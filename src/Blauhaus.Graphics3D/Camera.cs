using System;
using System.Collections.Generic;
using System.Numerics;

namespace Blauhaus.Graphics3D
{
    public class Camera
    {
        private float _width;
        private float _height;
        private readonly Vector3 _position;
        private readonly Vector3 _lookAtVector;
        private readonly Vector3 _upVector;

        private Matrix4x4 _worldMatrix;
        private Matrix4x4 _viewMatrix;
        private Matrix4x4 _projectionMatrix;
        private Matrix4x4? _screenMatrix;
        private Matrix4x4 ScreenMatrix
        {
            get
            {
                _screenMatrix ??= Matrix4x4.Multiply(Matrix4x4.Multiply(_worldMatrix, _viewMatrix), _projectionMatrix);
                return _screenMatrix.Value;
            }
        }

        public Camera(float width, float height, Vector3 position, Vector3 lookAtVector, Vector3 upVector)
        {
            _width = width;
            _height = height;
            _position = position;
            _lookAtVector = lookAtVector;
            _upVector = upVector;

            _worldMatrix = Matrix4x4.Identity;

            _viewMatrix = Matrix4x4.CreateLookAt(
                cameraPosition: position, 
                cameraTarget: _lookAtVector, 
                cameraUpVector: upVector);
            
            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                fieldOfView: (float) (Math.PI / 4f), 
                aspectRatio: width/height, 
                nearPlaneDistance: 0.01f, 
                farPlaneDistance: 10f);
        }

        public Camera SetDimensions(float width, float height)
        {
            _width = width;
            _height = height;
            
            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                fieldOfView: (float) (Math.PI / 4f), 
                aspectRatio: width/height, 
                nearPlaneDistance: 0.01f, 
                farPlaneDistance: 10f);

            _screenMatrix = null;
            return this;
        }


        public Vector2[] GetScreenCoordinates(IReadOnlyList<Vector3> worldPoints)
        {
            var canvasCoordinates = new Vector2[worldPoints.Count];
            for (var i = 0; i < worldPoints.Count; i++)
            {
                var vec4 = new Vector4(worldPoints[i], 1);
                var pointInCameraSpace = Vector4.Transform(vec4, ScreenMatrix); 
                var screenX = pointInCameraSpace.X / -pointInCameraSpace.Z * _width/2f + _width/2f;
                var screenY = pointInCameraSpace.Y / -pointInCameraSpace.Z * _height/2f + _height/2f;
                canvasCoordinates[i] = new Vector2(screenX, screenY);
            }

            return canvasCoordinates;
        }
         

    }
}