using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base;
using Blauhaus.Graphics3D.ViewModels;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Controls
{
    public class CameraCanvasControl : BaseCameraCanvasControl
    {
        public CameraCanvasControl(ICameraViewModel cameraViewModel) : base(cameraViewModel)
        {
        }
    }
}