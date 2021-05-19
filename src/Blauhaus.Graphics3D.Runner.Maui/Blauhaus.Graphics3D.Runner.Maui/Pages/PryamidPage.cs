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
            BackgroundColor = Color.LightSlateGray;
            
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
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 1
            };

            var camera = new Camera(info.Width, info.Height, new Vector3(-10, 0, 0), Vector3.UnitZ);
            var vectors = camera.GetScreenCoordinates(ViewModel.Vertices);
            
            var skVectors = new SKPoint[vectors.Length];
            for (var i = 0; i < vectors.Length; i++)
            {
                skVectors[i] = new SKPoint(vectors[i].X, vectors[i].Y);
            }

            canvas.DrawLine(skVectors[ViewModel.Indices[0]], skVectors[ViewModel.Indices[1]], paint);
            canvas.DrawLine(skVectors[ViewModel.Indices[1]], skVectors[ViewModel.Indices[2]], paint);
            canvas.DrawLine(skVectors[ViewModel.Indices[2]], skVectors[ViewModel.Indices[0]], paint);
            
            canvas.DrawLine(skVectors[ViewModel.Indices[3]], skVectors[ViewModel.Indices[4]], paint);
            canvas.DrawLine(skVectors[ViewModel.Indices[4]], skVectors[ViewModel.Indices[5]], paint);
            canvas.DrawLine(skVectors[ViewModel.Indices[5]], skVectors[ViewModel.Indices[3]], paint);
            
            canvas.DrawLine(skVectors[ViewModel.Indices[6]], skVectors[ViewModel.Indices[7]], paint);
            canvas.DrawLine(skVectors[ViewModel.Indices[7]], skVectors[ViewModel.Indices[8]], paint);
            canvas.DrawLine(skVectors[ViewModel.Indices[8]], skVectors[ViewModel.Indices[6]], paint);

            canvas.DrawLine(skVectors[ViewModel.Indices[9]], skVectors[ViewModel.Indices[10]], paint);
            canvas.DrawLine(skVectors[ViewModel.Indices[10]], skVectors[ViewModel.Indices[11]], paint);
            canvas.DrawLine(skVectors[ViewModel.Indices[11]], skVectors[ViewModel.Indices[9]], paint);
        }

         
    }
}