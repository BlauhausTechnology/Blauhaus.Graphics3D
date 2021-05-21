using System.Numerics;
using Blauhaus.Graphics3D.Tests.Tests.Base;
using NUnit.Framework;

namespace Blauhaus.Graphics3D.Tests.Tests.CameraTests.Base
{
    public abstract class BaseCameraTest : BaseGraphics3DTest<Camera>
    {
        protected float Width;
        protected float Height;
        protected Vector3 Position;
        protected Vector3 LookingAt;
        protected Vector3 UpVector;
        protected float NearClip;
        protected float FarClip;


        protected float MidX => Width / 2f;
        protected float MidY => Height / 2f;
        protected float LeftX => 0;
        protected float RightX => Width;
        protected float TopY => 0;
        protected float BottomY => Height;
        protected Vector2 Mid => new(MidX, MidY);

        public override void Setup()
        {
            base.Setup();

            Width = 1000;
            Height = 1000;
            Position = new Vector3(10, 0, 0);
            LookingAt = Vector3.Zero;
            UpVector = Vector3.UnitZ;
            NearClip = 0.01f;
            FarClip = 100f;
        }

        protected override Camera ConstructSut()
        {
            return new(Width, Height, Position, LookingAt, UpVector, NearClip, FarClip);
        }

        protected void VerifyOnScreen(Vector2 screenPosition)
        {
            Assert.That(screenPosition.X, Is.GreaterThan(LeftX));
            Assert.That(screenPosition.X, Is.LessThan(RightX));
            
            Assert.That(screenPosition.Y, Is.GreaterThan(TopY));
            Assert.That(screenPosition.Y, Is.LessThan(BottomY));
        }

        protected void VerifyLeftOfScreen(Vector2 screenPoint)
        {
            Assert.That(screenPoint.X, Is.LessThan(MidX));
            VerifyOnScreen(screenPoint);
        }

        protected void VerifyRightOfScreen(Vector2 screenPoint)
        {
            Assert.That(screenPoint.X, Is.GreaterThan(MidX));
            VerifyOnScreen(screenPoint);
        }

        protected void VerifyTopHalfOfScreen(Vector2 screenPoint)
        {
            Assert.That(screenPoint.Y, Is.LessThan(MidY));
            VerifyOnScreen(screenPoint);
        }
        
        protected void VerifyBottomHalfOfScreen(Vector2 screenPoint)
        {
            Assert.That(screenPoint.Y, Is.GreaterThan(MidY));
            VerifyOnScreen(screenPoint);
        }
    }
}