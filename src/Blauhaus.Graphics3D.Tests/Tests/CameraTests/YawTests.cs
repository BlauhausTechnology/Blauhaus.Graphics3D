using System;
using System.Numerics;
using Blauhaus.Graphics3D.Tests.Tests.CameraTests.Base;
using NUnit.Framework;

namespace Blauhaus.Graphics3D.Tests.Tests.CameraTests
{
    public class YawTests : BaseCameraTest
    {

        [Test]
        public void Positive_Yaw_SHOULD_move_world_origin_to_the_left_of_screen()
        {
            //Arrange
            var initial = Sut.GetScreenPosition(Vector3.Zero);

            //Act
            Sut.Yaw = 0.1f;
            var result = Sut.GetScreenPosition(Vector3.Zero);

            //Assert
            Assert.That(result.X, Is.LessThan(MidX));
            Assert.That(result.Y, Is.EqualTo(MidY));
            VerifyOnScreen(result);
        }
		   
    }
}