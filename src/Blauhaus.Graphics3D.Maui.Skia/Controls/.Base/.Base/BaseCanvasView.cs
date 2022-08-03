using System.Numerics;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace Blauhaus.Graphics3D.Maui.Skia.Controls.Base.Base
{
    public abstract class BaseCanvasView: ContentView
    {
        protected Vector2 ScreenDimensions;
        private readonly PinchGestureRecognizer _pinchGestureRecognizer;
        private readonly PanGestureRecognizer _panGestureRecognizer;

        public Action<Vector2>? DimensionsChangedHandler;
        public Action<SKCanvas, Vector2>? DrawHandler;
        public Action<ZoomEvent>? ZoomHandler;
        public Action<PanEvent>? PanHandler;

        protected BaseCanvasView()
        {
            _pinchGestureRecognizer = new PinchGestureRecognizer();
            GestureRecognizers.Add(_pinchGestureRecognizer);

            _panGestureRecognizer = new PanGestureRecognizer();
            GestureRecognizers.Add(_panGestureRecognizer);
        }

        public virtual void HandleAppearing()
        {
            _pinchGestureRecognizer.PinchUpdated += HandlePinch;
            _panGestureRecognizer.PanUpdated += HandlePan;
        }

        public virtual void HandleDisappearing()
        {
            _pinchGestureRecognizer.PinchUpdated -= HandlePinch;
            _panGestureRecognizer.PanUpdated -= HandlePan;
        }

        public abstract void Redraw();

        private void HandlePinch(object sender, PinchGestureUpdatedEventArgs args)
        {
            if (args.Status == GestureStatus.Running)
            {
                ZoomHandler?.Invoke(new ZoomEvent((float) args.ScaleOrigin.X, (float) args.ScaleOrigin.Y, args.Scale));
            }
        }
        
        private void HandlePan(object sender, PanUpdatedEventArgs args)
        {
            if (args.StatusType == GestureStatus.Running)
            {
                var newCenterX = (float)(ScreenDimensions.X / 2f + args.TotalX);
                var newCenterY = (float)(ScreenDimensions.Y / 2f + args.TotalY);
                var scaleX = (float)args.TotalX / ScreenDimensions.X;
                var scaleY = (float)(args.TotalY / ScreenDimensions.Y);
                PanHandler?.Invoke(new PanEvent(scaleX, scaleY, newCenterX, newCenterY));
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