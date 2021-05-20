using System;
using System.Collections.Generic;
using System.Numerics;

namespace Blauhaus.Graphics3D
{
    public class Camera
    {

        private Matrix4x4? _screenMatrix;

        public Camera(float width, float height, Vector3 position, Vector3 up)
        {
            Width = width;
            Height = height;

            WorldMatrix = Matrix4x4.Identity;

            ViewMatrix = Matrix4x4.CreateLookAt(
                cameraPosition: position, 
                cameraTarget: Vector3.Zero, 
                cameraUpVector: up);
            
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

        private Matrix4x4 ScreenMatrix
        {
            get
            {
                _screenMatrix ??= Matrix4x4.Multiply(Matrix4x4.Multiply(WorldMatrix, ViewMatrix), ProjectionMatrix);
                return _screenMatrix.Value;
            }
        }
        public Vector2[] GetScreenCoordinates(IReadOnlyList<Vector3> worldPoints)
        {
            var canvasCoordinates = new Vector2[worldPoints.Count];
            for (var i = 0; i < worldPoints.Count; i++)
            {
                var vec4 = new Vector4(worldPoints[i], 1);
                var pointInCameraSpace = Vector4.Transform(vec4, ScreenMatrix); 
                var screenX = pointInCameraSpace.X / -pointInCameraSpace.Z * Width/2f + Width/2f;
                var screenY = pointInCameraSpace.Y / -pointInCameraSpace.Z * Height/2f + Height/2f;
                canvasCoordinates[i] = new Vector2(screenX, screenY);
            }

            return canvasCoordinates;
        }

        public IReadOnlyList<Vector2> GetScreenCoordinates(IReadOnlyList<Vector4> worldPoints)
        {
            var canvasCoordinates = new Vector2[worldPoints.Count];
            for (var i = 0; i < worldPoints.Count; i++)
            {
                var pointInCameraSpace = Vector4.Transform(worldPoints[i], ScreenMatrix); 
                var screenX = pointInCameraSpace.X / -pointInCameraSpace.Z * Width/2f + Width/2f;
                var screenY = pointInCameraSpace.Y / -pointInCameraSpace.Z * Height/2f + Height/2f;
                canvasCoordinates[i] = new Vector2(screenX, screenY);
            }

            return canvasCoordinates;
        }

    }
}