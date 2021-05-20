using System.Linq;
using System.Numerics;
using Blauhaus.Graphics3D.Runner.Maui.Pages.Base;
using Blauhaus.Graphics3d.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Runner.Maui.Pages
{
    public class PryamidPage : BaseGraphics3DPage<PyramidViewModel>
    {
        public PryamidPage(PyramidViewModel viewModel) : base(viewModel)
        {
            BackgroundColor = Color.Black;
            
            var canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {

            var info = args.Info;
            var surface = args.Surface;
            var canvas = surface.Canvas;
            ViewModel.ScreenDimensions = new Vector2(info.Width, info.Height);

            canvas.Clear();
             
            var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                IsAntialias = true,
                Color = Color.Green.ToSKColor()
            };
             
            for (var i = 0; i < ViewModel.Indices.Length/3; i++)
            {
                using var trianglePath = new SKPath();
                trianglePath.MoveTo(ViewModel.ScreenPoints[ViewModel.Indices[i * 3 + 0]].X, ViewModel.ScreenPoints[ViewModel.Indices[i * 3 + 0]].Y);
                trianglePath.LineTo(ViewModel.ScreenPoints[ViewModel.Indices[i * 3 + 1]].X, ViewModel.ScreenPoints[ViewModel.Indices[i * 3 + 1]].Y);
                trianglePath.LineTo(ViewModel.ScreenPoints[ViewModel.Indices[i * 3 + 2]].X, ViewModel.ScreenPoints[ViewModel.Indices[i * 3 + 2]].Y);
                trianglePath.Close();
                canvas.DrawPath(trianglePath, paint);
            }

        }

         
    }
}