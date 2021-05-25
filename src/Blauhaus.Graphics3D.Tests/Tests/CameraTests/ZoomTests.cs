using System.Numerics;
using Blauhaus.Graphics3D.Tests.Tests.CameraTests.Base;
using NUnit.Framework;

namespace Blauhaus.Graphics3D.Tests.Tests.CameraTests
{
    public class ZoomTests : BaseCameraTest
    {

        [Test]
        public void WHEN_zoom_in_by_10_percentage_SHOULD_move_camera_from_10_to_9()
        {
            //Act
            Sut.Zoom(new ZoomEvent(0, 0, 1.1d));

            //Assert
            Assert.That(Sut.Position.X, Is.EqualTo(9f));
        }

        [Test]
        public void WHEN_zoom_out_by_20_percent_SHOULD_move_camera_from_10_to_12()
        {
            //Act
            Sut.Zoom(new ZoomEvent(0, 0, 0.8d));

            //Assert
            Assert.That(Sut.Position.X, Is.EqualTo(12f));
        }

        [Test]
        public void WHEN_zooming_in_SHOULD_only_zoom_distance_between_Camera_Position_and_Lookat()
        {
            //Arrange
            LookingAt = new Vector3(5, 0, 0);

            //Act
            Sut.Zoom(new ZoomEvent(0, 0, 0.8d));

            //Assert
            Assert.That(Sut.Position.X, Is.EqualTo(11f));
        }


        [Test]
        public void WHEN_zooming_out_SHOULD_only_zoom_distance_between_Camera_Position_and_Lookat()
        {
            //Arrange
            LookingAt = new Vector3(5, 0, 0);

            //Act
            Sut.Zoom(new ZoomEvent(0, 0, 1.2d));

            //Assert
            Assert.That(Sut.Position.X, Is.EqualTo(9f));
        }

        [Test]
        public void WHEN_zooming_in_SHOULD_not_zoom_further_than_lookat()
        {
            //Arrange
            LookingAt = new Vector3(5, 0, 0);

            //Act
            Sut.Zoom(new ZoomEvent(0, 0, 4d));

            //Assert
            Assert.That(Sut.Position.X, Is.EqualTo(float.Epsilon));
        }
		
    }
}