using System.Numerics;
using SkiaSharp;
// ReSharper disable InconsistentNaming

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Extensions
{
    public static class Vector2Extensions
    {
        public static SKPoint ToSKPoint(this Vector2 vector2)
        {
            return new SKPoint(vector2.X, vector2.Y);
        }
    }
}