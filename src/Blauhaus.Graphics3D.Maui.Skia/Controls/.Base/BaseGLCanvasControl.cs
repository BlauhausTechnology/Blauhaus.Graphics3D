// ReSharper disable InconsistentNaming

using System.Numerics;
using Blauhaus.Graphics3D.Maui.Skia.Controls.Base.Base;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace Blauhaus.Graphics3D.Maui.Skia.Controls.Base
{
    public abstract class BaseGLCanvasControl : BaseCanvasView
    {
        private readonly SKGLView _canvasView = new SKGLView();

        protected BaseGLCanvasControl()
        {
            Content = _canvasView;
        }
        
        public override void Redraw() => _canvasView.InvalidateSurface();

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

            DrawHandler?.Invoke(canvas, ScreenDimensions);
        }

        
    }
}