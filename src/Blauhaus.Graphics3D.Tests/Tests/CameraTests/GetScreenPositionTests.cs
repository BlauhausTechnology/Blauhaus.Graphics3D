using System.Numerics;
using Blauhaus.Graphics3D.Tests.Tests.CameraTests.Base;
using NUnit.Framework;

namespace Blauhaus.Graphics3D.Tests.Tests.CameraTests
{
    [TestFixture]
    public class GetScreenPositionTests : BaseCameraTest
    {

        public class LookingAtOrigin : GetScreenPositionTests
        {
            public override void Setup()
            {
                base.Setup();

                LookingAt = Vector3.Zero;
            }

            [Test]
            public void WHEN_Vector_is_ZERO()
            {
                //Act
                var result = Sut.GetScreenPosition(Vector3.Zero);

                //Assert
                Assert.That(result.X, Is.EqualTo(MidX));
                Assert.That(result.Y, Is.EqualTo(MidX));
                VerifyOnScreen(result);
            }

            [Test]
            public void WHEN_Vector_is_right_of_origin()
            {
                //Act
                var result = Sut.GetScreenPosition(new Vector3(0, 1, 0));

                //Assert
                Assert.That(result.X, Is.GreaterThan(MidX));
                VerifyOnScreen(result);
            }

            [Test]
            public void WHEN_Vector_is_left_of_origin()
            {
                //Act
                var result = Sut.GetScreenPosition(new Vector3(0, -1, 0));

                //Assert
                Assert.That(result.X, Is.LessThan(MidX));
                VerifyOnScreen(result);
            }

            [Test]
            public void WHEN_Vector_is_above_origin()
            {
                //Act
                var result = Sut.GetScreenPosition(new Vector3(0, 0, 1));

                //Assert
                Assert.That(result.Y, Is.LessThan(MidY));
                VerifyOnScreen(result);
            }

            [Test]
            public void WHEN_Vector_is_below_origin()
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
                Assert.That(result.Y, Is.LessThan(MidY));
                VerifyOnScreen(result);
            }
            
            [Test]
            public void WHEN_Vector_is_ToLeft()
            {
                //Act
                var result = Sut.GetScreenPosition(new Vector3(0, -1, 1));

                //Assert
                Assert.That(result.X, Is.LessThan(MidX)); 
                Assert.That(result.Y, Is.LessThan(MidY));
                VerifyOnScreen(result);
            }

            [Test]
            public void WHEN_Vector_is_BottomLeft()
            {
                //Act
                var result = Sut.GetScreenPosition(new Vector3(0, -1, -1));

                //Assert
                Assert.That(result.X, Is.LessThan(MidX)); 
                Assert.That(result.Y, Is.GreaterThan(MidY));
                VerifyOnScreen(result);
            }
            
            [Test]
            public void WHEN_Vector_is_BottomRight()
            {
                //Act
                var result = Sut.GetScreenPosition(new Vector3(0, 1, -1));

                //Assert
                Assert.That(result.X, Is.GreaterThan(MidX)); 
                Assert.That(result.Y, Is.GreaterThan(MidY));
                VerifyOnScreen(result);
            }
        }
    }
}