// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.Numerics;
using SkiaSharp;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Extensions
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
    }
}