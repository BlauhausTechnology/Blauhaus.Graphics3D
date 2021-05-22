using System;
using System.Numerics;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base.Base;
using Blauhaus.MVVM.Xamarin.Views.ContentViews;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base
{
    public abstract class BaseCanvasControl : BaseCanvasView
    {
        private readonly SKCanvasView _canvasView = new();
        private bool _isDrawing;

        protected BaseCanvasControl() 
        {
            _canvasView.PaintSurface += OnCanvasViewPaintSurface;
            _canvasView.EnableTouchEvents = true;
            _canvasView.Touch += HandleTouch;

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

            DrawHandler?.Invoke(canvas);
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