﻿using System;
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
        private Vector3 _lookDirection;
        private Vector3 _upVector;
        private float _nearclip;
        private float _farClip;
        private Quaternion _cameraRotation;

        private Matrix4x4 _worldMatrix;
        private Matrix4x4 _viewMatrix;
        private Matrix4x4 _projectionMatrix;
        private Matrix4x4 _screenMatrix;
        private float _yaw;
        private float _pitch;
        private float _roll;

        public Camera(
            float width, float height, 
            Vector3 position, Vector3 lookDirection, Vector3 upVector, 
            float nearclip = 0.01f, float farClip = 100f)
        {
            _width = width;
            _height = height;
            _position = position;
            _lookDirection = lookDirection;
            _upVector = upVector;
            _nearclip = nearclip;
            _farClip = farClip;
            _cameraRotation = Quaternion.Identity;
            
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

        public void SetLookAt(Vector3 lookAtVector)
        {
            _lookDirection = lookAtVector;
            UpdateMatrices();
        }
        
        public Vector3 Position { get => _position; set { _position = value; UpdateMatrices(); } }
         
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

            _cameraRotation = Quaternion.CreateFromYawPitchRoll(Yaw, Pitch, Roll);
            //var lookDirection = Vector3.Transform(_lookDirection, lookRotation);
            _lookDirection = Vector3.Transform(_lookDirection, Matrix4x4.CreateFromQuaternion(_cameraRotation));

            _viewMatrix = Matrix4x4.CreateLookAt(
                cameraPosition: _position, 
                cameraTarget: /*_position*/ _lookDirection, 
                cameraUpVector: _upVector);
            
            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                fieldOfView: (float) (Math.PI / 4f), 
                aspectRatio: _width/_height, 
                nearPlaneDistance:_nearclip, 
                farPlaneDistance: _farClip);
            
            var worldViewMatrix = Matrix4x4.Multiply(_worldMatrix , _viewMatrix);

            _screenMatrix = Matrix4x4.Multiply(worldViewMatrix, _projectionMatrix);

        }

         

    }
}