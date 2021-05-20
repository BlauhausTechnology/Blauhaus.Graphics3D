using System;
using System.Collections.Generic;
using System.Numerics;
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Blauhaus.Graphics3D
{
    public class Camera
    {
        private float _width;
        private float _height;
        private Vector3 _position;
        private Vector3 _lookAtVector;
        private Vector3 _upVector;
        private float _nearclip;
        private float _farClip;

        private Matrix4x4 _worldMatrix;
        private Matrix4x4 _viewMatrix;
        private Matrix4x4 _projectionMatrix;
        private Matrix4x4 _screenMatrix; 

        public Camera(
            float width, float height, 
            Vector3 position, Vector3 lookAtVector, Vector3 upVector, 
            float nearclip = 0.01f, float farClip = 100f)
        {
            _width = width;
            _height = height;
            _position = position;
            _lookAtVector = lookAtVector;
            _upVector = upVector;
            _nearclip = nearclip;
            _farClip = farClip;

            GenerateMatrices();
        }

        private void GenerateMatrices()
        {
            _worldMatrix = Matrix4x4.Identity;

            _viewMatrix = Matrix4x4.CreateLookAt(
                cameraPosition: _position, 
                cameraTarget: _lookAtVector, 
                cameraUpVector: _upVector);
            
            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                fieldOfView: (float) (Math.PI / 4f), 
                aspectRatio: _width/_height, 
                nearPlaneDistance:_nearclip, 
                farPlaneDistance: _farClip);

            var worldViewMatrix = Matrix4x4.Multiply(_worldMatrix, _viewMatrix);

            _screenMatrix = Matrix4x4.Multiply(worldViewMatrix, _projectionMatrix);
        }

        public Vector2 GetScreenPosition(Vector3 worldPosition)
        {
            var pointInCameraSpace = Vector4.Transform(new Vector4(worldPosition, 1), _screenMatrix);  
            return new Vector2(
                pointInCameraSpace.X / -pointInCameraSpace.Z * _width/2f + _width/2f, 
                pointInCameraSpace.Y / -pointInCameraSpace.Z * _height/2f + _height/2f);
        }

        public Vector2[] GetScreenCoordinates(IReadOnlyList<Vector3> worldPoints)
        {
            
            var canvasCoordinates = new Vector2[worldPoints.Count];
            for (var i = 0; i < worldPoints.Count; i++)
            {
                canvasCoordinates[i] = GetScreenPosition(worldPoints[i]);
            }

            return canvasCoordinates;
        }
         

    }
}