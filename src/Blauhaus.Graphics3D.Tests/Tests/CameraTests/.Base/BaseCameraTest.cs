using System.Numerics;
using Blauhaus.Graphics3D.Tests.Tests.Base;

namespace Blauhaus.Graphics3D.Tests.Tests.CameraTests.Base
{
    public abstract class BaseCameraTest : BaseGraphics3DTest<Camera>
    {
        protected float Width;
        protected float Height;
        protected Vector3 Position;
        protected Vector3 LookAtVector;
        protected Vector3 UpVector;
        protected float NearClip;
        protected float FarClip;

        public override void Setup()
        {
            base.Setup();

            Width = 1200;
            Height = 600;
            Position = new Vector3(-10, 0, 0);
            LookAtVector = Vector3.Zero;
            UpVector = Vector3.UnitZ;
            NearClip = 0.01f;
            FarClip = 100f;
        }

        protected override Camera ConstructSut()
        {
            return new(Width, Height, Position, LookAtVector, UpVector, NearClip, FarClip);
        }
    }
}