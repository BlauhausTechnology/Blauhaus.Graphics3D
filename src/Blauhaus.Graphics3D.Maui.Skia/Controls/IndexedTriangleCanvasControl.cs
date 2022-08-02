using System.Numerics;
using Blauhaus.Graphics3D.Maui.Skia.Copy.Controls.Base;
using Blauhaus.Graphics3D.Maui.Skia.Copy.Extensions;
using Blauhaus.Graphics3D.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace Blauhaus.Graphics3D.Maui.Skia.Copy.Controls
{
    public class IndexedTriangleCanvasControl : BaseCameraCanvasControl
    {

        public IndexedTriangleCanvasControl(ICameraViewModel viewModel): base(viewModel)
        {
            DrawHandler = (canvas, screen) =>
            {
                var screenPoints = Camera.GetScreenCoordinates(Vertices);

                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke, 
                    StrokeWidth = 1, 
                    IsAntialias = true, 
                    Color = Color.FromRgb(50,200,0).ToSKColor()
                };

                canvas.DrawIndexedTriangles(screenPoints, Indices, paint); 
            };
        }


        public Vector3[] Vertices { get; set; } = Array.Empty<Vector3>();
        public int[] Indices { get; set; } = Array.Empty<int>();

    }
}