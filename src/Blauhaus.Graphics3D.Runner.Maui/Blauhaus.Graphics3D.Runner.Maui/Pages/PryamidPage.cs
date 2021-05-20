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
            canvas.Clear();
             
            var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                IsAntialias = true,
                Color = Color.Green.ToSKColor()
            };

            var camera = new Camera(info.Width, info.Height, new Vector3(-10, 0, 0), Vector3.UnitZ);
            var vectors = camera.GetScreenCoordinates(ViewModel.Vertices);
             
            for (var i = 0; i < ViewModel.Indices.Length; i++)
            {
                using var trianglePath = new SKPath();
                trianglePath.MoveTo(vectors[ViewModel.Indices[i * 3 + 0]].X, vectors[ViewModel.Indices[i * 3 + 0]].Y);
                trianglePath.LineTo(vectors[ViewModel.Indices[i * 3 + 1]].X, vectors[ViewModel.Indices[i * 3 + 1]].Y);
                trianglePath.LineTo(vectors[ViewModel.Indices[i * 3 + 2]].X, vectors[ViewModel.Indices[i * 3 + 2]].Y);
                trianglePath.Close();
                canvas.DrawPath(trianglePath, paint);
            }

        }

         
    }
}