using System.Numerics;
using Blauhaus.Graphics3D.Tests.Tests.CameraTests.Base;
using NUnit.Framework;

namespace Blauhaus.Graphics3D.Tests.Tests.CameraTests
{
    [TestFixture]
    public class GetScreenPositionTests : BaseCameraTest
    {
        private const float MidX = 500;
        private const float MidY = 500;
        private const float LeftX = 0;
        private const float RightX = 1000;
        private const float TopY = 0;
        private const float BottomY = 1000;
        
        private static void VerifyOnScreen(Vector2 screenPosition)
        {
            Assert.That(screenPosition, Is.GreaterThan(LeftX));
            Assert.That(screenPosition, Is.GreaterThan(TopY));
            Assert.That(screenPosition, Is.LessThan(BottomY));
            Assert.That(screenPosition, Is.LessThan(RightX));

        }


        public class LookingAtOrigin : GetScreenPositionTests
        {
            public override void Setup()
            {
                base.Setup();

                LookAtVector = Vector3.Zero;
            }

            [Test]
            public void WHEN_Vector_is_ZERO_SHOULD_return_screen_midpoint()
            {
                //Act
                var result = Sut.GetScreenPosition(Vector3.Zero);

                //Assert
                Assert.That(result.X, Is.EqualTo(MidX));
                Assert.That(result.Y, Is.EqualTo(MidX));
                VerifyOnScreen(result);
            }

            [Test]
            public void WHEN_Vector_is_right_of_origin_SHOULD_return_X_to_the_right()
            {
                //Act
                var result = Sut.GetScreenPosition(new Vector3(0, 1, 0));

                //Assert
                Assert.That(result.X, Is.GreaterThan(MidX));
                Assert.That(result.X, Is.LessThan(RightX));
                Assert.That(result.Y, Is.EqualTo(MidY));
            }

            [Test]
            public void WHEN_Vector_is_left_of_origin_SHOULD_return_X_to_the_left()
            {
                //Act
                var result = Sut.GetScreenPosition(new Vector3(0, -1, 0));

                //Assert
                Assert.That(result.X, Is.LessThan(MidX));
                Assert.That(result.X, Is.GreaterThan(LeftX));
                Assert.That(result.Y, Is.EqualTo(MidY));
            }

            [Test]
            public void WHEN_Vector_is_above_origin_SHOULD_return_Y_above()
            {
                //Act
                var result = Sut.GetScreenPosition(new Vector3(0, 0, 1));

                //Assert
                Assert.That(result.X, Is.EqualTo(MidX));
                Assert.That(result.Y, Is.LessThan(MidY));
                Assert.That(result.Y, Is.GreaterThan(TopY));
            }

            [Test]
            public void WHEN_Vector_is_below_origin_SHOULD_return_Y_below_mid()
            {
                //Act
                var result = Sut.GetScreenPosition(new Vector3(0, 0, -1));

                //Assert
                Assert.That(result.X, Is.EqualTo(MidX));
                Assert.That(result.Y, Is.GreaterThan(MidY));
                Assert.That(result.Y, Is.LessThan(BottomY));
            }
            
            [Test]
            public void WHEN_Vector_is_TopRight()
            {
                //Act
                var result = Sut.GetScreenPosition(new Vector3(0, 1, 1));

                //Assert
                Assert.That(result.X, Is.GreaterThan(MidX));
                Assert.That(result.X, Is.LessThan(RightX));
                Assert.That(result.Y, Is.LessThan(MidY));
                Assert.That(result.Y, Is.LessThan(TopY));
            }
        }
    }
}