using System.Numerics;

namespace Blauhaus.Graphics3D.ViewModels
{
    public interface ICameraViewModel
    {
        public Vector3 CameraPosition { get; set; }
        public Vector3 CameraLookingAt { get; set; }
        public Vector3 CameraLookDirection { get; set; }
    }
}