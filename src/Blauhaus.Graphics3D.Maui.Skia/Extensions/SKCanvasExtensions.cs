// ReSharper disable InconsistentNaming

using System.Numerics;
using SkiaSharp;

namespace Blauhaus.Graphics3D.Maui.Skia.Extensions
{
    public static class SKCanvasExtensions
    {
        public static void DrawIndexedTriangles(this SKCanvas canvas, IReadOnlyList<Vector2> screenPoints, IReadOnlyList<int> indices, SKPaint paint)
        {
            for (var i = 0; i < indices.Count / 3; i++)
            {
                using var trianglePath = new SKPath();
                trianglePath.MoveTo(screenPoints[indices[i * 3 + 0]].X, screenPoints[indices[i * 3 + 0]].Y);
                trianglePath.LineTo(screenPoints[indices[i * 3 + 1]].X, screenPoints[indices[i * 3 + 1]].Y);
                trianglePath.LineTo(screenPoints[indices[i * 3 + 2]].X, screenPoints[indices[i * 3 + 2]].Y);
                trianglePath.Close();
                canvas.DrawPath(trianglePath, paint);
            }
        }

        public static void DrawTriangle(this SKCanvas canvas, IReadOnlyList<Vector2> triangleVertices2D, SKPaint paint)
        {
            using var trianglePath = new SKPath();
            trianglePath.MoveTo(triangleVertices2D[0].ToSKPoint());
            trianglePath.LineTo(triangleVertices2D[1].ToSKPoint());
            trianglePath.LineTo(triangleVertices2D[2].ToSKPoint());
            trianglePath.Close();
            canvas.DrawPath(trianglePath, paint);
        }
         
    }
}