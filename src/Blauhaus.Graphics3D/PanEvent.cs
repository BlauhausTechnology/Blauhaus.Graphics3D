using System.Numerics;

namespace Blauhaus.Graphics3D
{
    
    public readonly struct PanEvent
    { 
        public PanEvent(float x, float y, float newX, float newY)
        {
            Scale = new Vector2(x, y);
            NewCenter = new Vector2(newX, newY);
        }

        public Vector2 NewCenter { get; }

        public Vector2 Scale { get; }
    }
}