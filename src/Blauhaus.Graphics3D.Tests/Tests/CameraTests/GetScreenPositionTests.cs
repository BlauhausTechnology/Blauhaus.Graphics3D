using System.Numerics;
using Blauhaus.Graphics3D.Tests.Tests.CameraTests.Base;
using Blauhaus.TestHelpers.Builders.Base;
using NUnit.Framework;

namespace Blauhaus.Graphics3D.Tests.Tests.CameraTests
{
    public class GetScreenPositionTests : BaseCameraTest
    {
        private const float MidX = 500;
        private const float MiddleY = 500;
        private const float LeftX = 0;
        private const float RightX = 1000;
        private const float TopY = 0;
        private const float BottomY = 1000;


        [Test]
        public void WHEN_Looking_at_Origin_and_Vector_is_ZERO_SHOULD_return_screen_midpoint()
        {
            //Act
            var result = Sut.GetScreenPosition(Vector3.Zero);

            //Assert
            Assert.That(result.X, Is.EqualTo(MidX));
            Assert.That(result.Y, Is.EqualTo(MidX));
        }

        [Test]
        public void WHEN_Looking_at_Origin_and_Vector_is_right_of_origin_SHOULD_return_X_to_the_right()
        {
            //Act
            var result = Sut.GetScreenPosition(new Vector3(0, 1, 0));

            //Assert
            Assert.That(result.X, Is.GreaterThan(MidX));
            Assert.That(result.X, Is.LessThan(RightX));
            Assert.That(result.Y, Is.EqualTo(MiddleY));
        }

        [Test]
        public void WHEN_Looking_at_Origin_and_Vector_is_left_of_origin_SHOULD_return_X_to_the_left()
        {
            //Act
            var result = Sut.GetScreenPosition(new Vector3(0, -1, 0));

            //Assert
            Assert.That(result.X, Is.LessThan(MidX));
            Assert.That(result.X, Is.GreaterThan(LeftX));
            Assert.That(result.Y, Is.EqualTo(MiddleY));
        }

        [Test]
        public void WHEN_Looking_at_Origin_and_Vector_is_above_origin_SHOULD_return_Y_above()
        {
            //Act
            var result = Sut.GetScreenPosition(new Vector3(0, 0, 1));

            //Assert
            Assert.That(result.X, Is.EqualTo(MidX));
            Assert.That(result.Y, Is.LessThan(MiddleY));
            Assert.That(result.Y, Is.GreaterThan(TopY));
        }
		
    }
}