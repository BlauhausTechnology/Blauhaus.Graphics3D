using System.Collections.Generic;
using System.Numerics;

namespace Blauhaus.Graphics3D.Extensions
{
    public static class Vector2CollectionExtensions
    {
        public static bool IsAnyOnPointScreen(this IEnumerable<Vector2> points, Vector2 screenDimensions)
        {
            foreach (var vector2 in points)
            {
                if (vector2.IsOnScreen(screenDimensions.X, screenDimensions.Y))
                    return true;
            }

            return false;
        }
    }
}