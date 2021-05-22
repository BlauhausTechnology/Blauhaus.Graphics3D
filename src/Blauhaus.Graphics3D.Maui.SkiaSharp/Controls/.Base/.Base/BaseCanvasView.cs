using System;
using System.Numerics;
using Blauhaus.MVVM.Xamarin.Views.ContentViews;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base.Base
{
    public abstract class BaseCanvasView <TViewModel> : BaseContentView<TViewModel>
    {
        protected Vector2 ScreenDimensions;
        protected PinchGestureRecognizer? PinchGestureRecognizer;

        public Action<Vector2>? DimensionsChangedHandler;
        public Action<ZoomEvent>? ZoomHandler;
        public Action<SKCanvas>? DrawHandler;

        protected BaseCanvasView(TViewModel viewModel) : base(viewModel)
        {
            PinchGestureRecognizer = new PinchGestureRecognizer();
            PinchGestureRecognizer.PinchUpdated += (_, args) =>
            {
                if (args.Status == GestureStatus.Running)
                {
                    ZoomHandler?.Invoke(new ZoomEvent(args.ScaleOrigin.X, args.ScaleOrigin.Y, args.Scale));
                }
            };

            GestureRecognizers.Add(PinchGestureRecognizer);

        }

        protected void HandleTouch(SKTouchEventArgs args)
        {
            if (args.ActionType == SKTouchAction.WheelChanged)
            {
                if (args.WheelDelta != 0)
                {
                    var scale = (ScreenDimensions.Y + args.WheelDelta) / ScreenDimensions.Y;
                    var x = args.Location.X;
                    var y = args.Location.Y;

                    ZoomHandler?.Invoke(new ZoomEvent(x, y, scale));
                }
            }
        }

    }
}