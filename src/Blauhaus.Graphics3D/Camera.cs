using System;
using System.Numerics;

namespace Blauhaus.Graphics3D
{
    public class Camera
    {

        private Matrix4x4? _screenMatrix;

        public Camera(float width, float height)
        {
            Width = width;
            Height = height;

            WorldMatrix = Matrix4x4.Identity;

            ViewMatrix = Matrix4x4.CreateLookAt(
                cameraPosition: new Vector3(0, 0, -5), 
                cameraTarget: Vector3.Zero, 
                cameraUpVector: Vector3.UnitY);
            
            ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                fieldOfView: (float) (Math.PI / 4f), 
                aspectRatio: width/height, 
                nearPlaneDistance: 0.01f, 
                farPlaneDistance: 10f);
        }

        public float Width { get; }
        public float Height { get; }

        public Matrix4x4 WorldMatrix { get; }
        public Matrix4x4 ViewMatrix { get; }
        public Matrix4x4 ProjectionMatrix { get; }

        public Matrix4x4 ScreenMatrix
        {
            get
            {
                if (_screenMatrix == null)
                {
                    _screenMatrix = Matrix4x4.Multiply(Matrix4x4.Multiply(WorldMatrix, ViewMatrix), ProjectionMatrix);
                }
                return _screenMatrix.Value;
            }
        }

    }
}