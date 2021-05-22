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
        private Vector3 _lookingAt;
        private Vector3 _upVector;
        private float _nearclip;
        private float _farClip;

        private Matrix4x4 _worldMatrix;
        private Matrix4x4 _viewMatrix;
        private Matrix4x4 _projectionMatrix;
        private Matrix4x4 _screenMatrix;
        private float _yaw;
        private float _pitch;
        private float _roll;

        public Camera(
            float width, float height, 
            Vector3 position, Vector3 lookingAt, Vector3 upVector, 
            float nearclip = 0.01f, float farClip = 100f)
        {
            _width = width;
            _height = height;
            _position = position;
            _lookingAt = lookingAt;
            _upVector = upVector;
            _nearclip = nearclip;
            _farClip = farClip;
            
            //non-traditional Matrix because of different coordinate system
            _worldMatrix = new Matrix4x4(
                1, 0, 0, 0,
                0, -1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

            UpdateMatrices();
        }

        public void SetDimensions(float width, float height)
        {
            _width = width;
            _height = height;
            UpdateMatrices();
        }
         
        
        public Vector3 LookingAt { get => _lookingAt; set { _lookingAt = value; UpdateMatrices(); } }
        public Vector3 Position { get => _position; set { _position = value; UpdateMatrices(); } }

        public void Zoom(ZoomEvent zoomEvent)
        {
            var currentCameraDistance = _position.Length() - _lookingAt.Length();
            var changeInDistance = (float) (1f - zoomEvent.Scale) * currentCameraDistance;
            var newCameraDistance = _position.Length() + changeInDistance;
            var currentCameraVector = Vector3.Normalize(_position);
            var newCameraPosition = newCameraDistance * currentCameraVector;
            Position = newCameraPosition;
        }
        

        //hmmmm
        public float Yaw { get => _yaw; set { _yaw = value; UpdateMatrices(); } }
        public float Pitch { get => _pitch; set { _pitch = value; UpdateMatrices(); } }
        public float Roll { get => _roll; set { _roll = value; UpdateMatrices(); } }
         

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
                var worklPoint = worldPoints[i];

                canvasCoordinates[i] = GetScreenPosition(worklPoint);
            }

            return canvasCoordinates;
        }

        //todo if (Vector3.Dot(_lookAtVector, triangleNormal) > 0) => cull


        private void UpdateMatrices()
        { 
            _viewMatrix = Matrix4x4.CreateLookAt(
                cameraPosition: new Vector3(_position.X, -_position.Y, _position.Z),
                cameraTarget: new Vector3(_lookingAt.X, -_lookingAt.Y, _lookingAt.Z),
                cameraUpVector: _upVector);

            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                fieldOfView: (float) (Math.PI / 4f), 
                aspectRatio: _width /_height, 
                nearPlaneDistance:_nearclip, 
                farPlaneDistance: _farClip);
            
            var worldViewMatrix = Matrix4x4.Multiply(_worldMatrix , _viewMatrix);

            _screenMatrix = Matrix4x4.Multiply(worldViewMatrix, _projectionMatrix);

        }

        private Vector3 GetLookDirection()
        {
            
            var lookDirection = Vector3.Normalize(_lookingAt - _position);
            
            var leftDirection = Vector3.Cross(_upVector, lookDirection); //maybe use for panning

            var cameraYaw = Quaternion.CreateFromAxisAngle(-Vector3.UnitZ, _yaw);
            lookDirection = Vector3.Transform(lookDirection, cameraYaw);
            
            var cameraPitchRotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, _pitch);
            lookDirection = Vector3.Transform(lookDirection, cameraPitchRotation);
            //_upVector = Vector3.Transform(_upVector, cameraPitchRotation);

            var cameraRoll = Quaternion.CreateFromAxisAngle(Vector3.UnitX, _roll);
            lookDirection = Vector3.Transform(lookDirection, cameraRoll);

            return lookDirection;
        }
         
    }
}