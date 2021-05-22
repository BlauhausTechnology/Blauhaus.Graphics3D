using System.Linq;
using System.Numerics;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Extensions;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Controls
{
    public class IndexedTriangleCanvasControl : BaseCameraCanvasControl
    {

        public IndexedTriangleCanvasControl()
        {
            DrawHandler = canvas =>
            {
                var screenPoints = Camera.GetScreenCoordinates(Vertices);

                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke, 
                    StrokeWidth = 1, 
                    IsAntialias = true, 
                    Color = Color.Red.ToSKColor()
                };

                canvas.DrawIndexedTriangles(screenPoints, Indices, paint); 
            };
        }


        public Vector3[] Vertices { get; set; } = new Vector3[0];
        public int[] Indices { get; set; } = new int[0];

    }
}