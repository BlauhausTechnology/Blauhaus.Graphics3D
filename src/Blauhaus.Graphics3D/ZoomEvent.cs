using System.Numerics;

namespace Blauhaus.Graphics3D
{
    
    public readonly struct ZoomEvent
    {
        public ZoomEvent(double x, double y, double scale)
        {
            Center = new Vector2((float) x, (float) y);
            Scale = scale;
        }

        public ZoomEvent(float x, float y, double scale)
        {
            Center = new Vector2(x, y);
            Scale = scale;
        }

        public Vector2 Center { get; }
        public double Scale { get; }
    }
}