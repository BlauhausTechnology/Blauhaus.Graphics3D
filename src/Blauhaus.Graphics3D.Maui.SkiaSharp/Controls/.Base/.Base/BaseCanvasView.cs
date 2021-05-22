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
        protected PinchGestureRecognizer PinchGestureRecognizer;

        public Action<Vector2>? DimensionsChangedHandler;
        public Action<ZoomEvent>? ZoomHandler;
        public Action<SKCanvas>? DrawHandler;

        protected BaseCanvasView(TViewModel viewModel) : base(viewModel)
        {
            PinchGestureRecognizer = new PinchGestureRecognizer();
            GestureRecognizers.Add(PinchGestureRecognizer);
        }

        public virtual void HandleAppearing()
        {
            PinchGestureRecognizer.PinchUpdated += HandlePinch;
        }

        public virtual void HandleDisappearing()
        {
            PinchGestureRecognizer.PinchUpdated -= HandlePinch;
        }

        public abstract void Redraw();

        private void HandlePinch(object sender, PinchGestureUpdatedEventArgs args)
        {
            if (args.Status == GestureStatus.Running)
            {
                ZoomHandler?.Invoke(new ZoomEvent(args.ScaleOrigin.X, args.ScaleOrigin.Y, args.Scale));
            }
        }

        protected void HandleTouch(object sender, SKTouchEventArgs args)
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