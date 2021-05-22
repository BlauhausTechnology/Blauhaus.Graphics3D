// ReSharper disable InconsistentNaming

using System;
using System.Numerics;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base.Base;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base
{
    public abstract class BaseGLCanvasControl<TViewModel> : BaseCanvasView<TViewModel>
    {
        private readonly SKGLView _canvasView = new();

        protected BaseGLCanvasControl(TViewModel viewModel) : base(viewModel)
        {
            _canvasView.PaintSurface += OnCanvasViewPaintSurface;
            _canvasView.EnableTouchEvents = true;
            _canvasView.Touch += (_, args) => HandleTouch(args);

            Content = _canvasView;
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;
            var width = e.BackendRenderTarget.Width;
            var height = e.BackendRenderTarget.Height;

            if (ScreenDimensions.X != width || ScreenDimensions.Y != height)
            {
                ScreenDimensions = new Vector2(width, height);
                DimensionsChangedHandler?.Invoke(ScreenDimensions);
            }

            DrawHandler?.Invoke(canvas);
        }

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
                        HandleTouch(args);
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