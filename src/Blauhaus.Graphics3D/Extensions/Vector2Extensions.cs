using System.Numerics;

namespace Blauhaus.Graphics3D.Extensions
{
    public static class Vector2Extensions
    {
        public static bool IsOnScreen(this Vector2 point, float screenWidth, float screenHeight)
        {
            if (point.X < 0 || point.Y < 0) return false;
            if (point.X > screenWidth || point.Y > screenHeight) return false;
            return true;
        }
    }
}