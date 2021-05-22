using System;
using System.Numerics;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base.Base;
using Blauhaus.MVVM.Xamarin.Views.ContentViews;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base
{
    public abstract class BaseCanvasControl<TViewModel> : BaseCanvasView<TViewModel>
    {
        private readonly SKCanvasView _canvasView = new();

        protected BaseCanvasControl(TViewModel viewModel) : base(viewModel)
        {
            _canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = _canvasView;
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            if (ScreenDimensions.X != info.Width || ScreenDimensions.Y != info.Height)
            {
                ScreenDimensions = new Vector2(info.Width, info.Height);
                DimensionsChanged?.Invoke(ScreenDimensions);
            }

            Draw?.Invoke(canvas);
        }

        
        public Action<Vector2>? DimensionsChanged { get; set; }
        public Action<SKCanvas>? Draw { get; set; }

        protected void Redraw() => _canvasView.InvalidateSurface();

        public void HandleZoom(Action<ZoomEvent> pinchHandler)
        {
            PinchGestureRecognizer = new PinchGestureRecognizer();
            PinchGestureRecognizer.PinchUpdated += (_, args) =>
            {
                if (args.Status == GestureStatus.Running)
                {
                    pinchHandler?.Invoke(new ZoomEvent(args.ScaleOrigin.X, args.ScaleOrigin.Y, args.Scale));
                }
            };
            GestureRecognizers.Add(PinchGestureRecognizer);

            if (Device.Idiom == TargetIdiom.Desktop)
            {
                if (_canvasView.EnableTouchEvents == false)
                {
                    _canvasView.EnableTouchEvents = true;
                    _canvasView.Touch += (_, args) =>
                    {
                        if (args.ActionType == SKTouchAction.WheelChanged)
                        {
                            if (args.WheelDelta != 0)
                            {
                                var scale = (ScreenDimensions.Y + args.WheelDelta) / ScreenDimensions.Y;
                                var x = args.Location.X;
                                var y = args.Location.Y;

                                pinchHandler?.Invoke(new ZoomEvent(x, y, scale));
                            }
                        }
                    };
                }
            }
        }
    }
}