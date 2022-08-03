using System.Numerics;
using Blauhaus.Graphics3D.Maui.Skia.Controls.Base.Base;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace Blauhaus.Graphics3D.Maui.Skia.Controls.Base
{
    public abstract class BaseCanvasControl : BaseCanvasView
    {
        private readonly SKCanvasView _canvasView = new SKCanvasView();
        private bool _isDrawing;

        protected BaseCanvasControl() 
        { 
            Content = _canvasView;
        }
        
        public override void Redraw() => 
            _canvasView.InvalidateSurface();

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (_isDrawing)
            {
                return;
            }

            _isDrawing = true;

            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear();
            
            if (ScreenDimensions.X != info.Width || ScreenDimensions.Y != info.Height)
            {
                ScreenDimensions = new Vector2(info.Width, info.Height);
                DimensionsChangedHandler?.Invoke(ScreenDimensions);
            }

            DrawHandler?.Invoke(canvas, ScreenDimensions);
            _isDrawing = false;
        }
        
        public override void HandleAppearing()
        {
            base.HandleAppearing();
            
            _canvasView.PaintSurface += OnCanvasViewPaintSurface;
            _canvasView.EnableTouchEvents = true;
            _canvasView.Touch += HandleTouch;
        }

        public override void HandleDisappearing()
        {
            base.HandleDisappearing();
            
            _canvasView.PaintSurface -= OnCanvasViewPaintSurface;
            _canvasView.EnableTouchEvents = false;
            _canvasView.Touch -= HandleTouch;
        }
 
    }
}