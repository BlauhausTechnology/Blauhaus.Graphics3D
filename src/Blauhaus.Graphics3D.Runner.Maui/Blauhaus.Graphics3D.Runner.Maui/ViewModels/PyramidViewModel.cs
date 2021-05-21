using System;
using System.Numerics;
using Blauhaus.MVVM.Abstractions.ViewModels;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Blauhaus.Graphics3D.Runner.Maui.ViewModels
{
    public class PyramidViewModel : BaseViewModel
    {
        private Vector3[] _worldVertices;


        public PyramidViewModel()
        {
            _worldVertices = new Vector3[]
            {
                new( 0,  0,  3),        
                new( 2,  2, -2),
                new( 2, -2, -2),   
                new(-2,  2, -2),   
                new(-2, -2, -2),    
            };

            Indices = new[]
            {
                //front face
                0, 1, 2,
                
                //right face
                0, 3, 1,

                //back face
                0, 4, 3,

                //left face
                0, 2, 4
            };
            

            Camera = new Camera(1, 1, new Vector3(10, 0, 0), Vector3.Zero, Vector3.UnitZ);
            ScreenPoints = Array.Empty<Vector2>();
            
            LookAtX = 0;
            LookAtY = 0;
            LookAtZ = 0;

            PositionX = 10;
            PositionY = 0.1f;
            PositionZ = 0;
        }

        private Camera Camera { get; }
        
        public Vector2 ScreenDimensions
        {
            set
            {
                Camera.SetDimensions(value.X, value.Y);
                ScreenPoints = Camera.GetScreenCoordinates(_worldVertices);
            }
        }
        
        public Vector2[] ScreenPoints
        {
            get => GetProperty<Vector2[]>();
            private set => SetProperty(value);
        }

        public int[] Indices { get; }


        public float LookAtX
        {
            get => GetProperty<float>();
            set => SetProperty(value, UpdateLookAt);
        }
        
        public float LookAtY
        {
            get => GetProperty<float>();
            set => SetProperty(value, UpdateLookAt);
        }
        
        public float LookAtZ
        {
            get => GetProperty<float>();
            set => SetProperty(value, UpdateLookAt);
        }


        public float PositionX
        {
            get => GetProperty<float>();
            set => SetProperty(value, UpdatePosition);
        }
        
        public float PositionY
        {
            get => GetProperty<float>();
            set => SetProperty(value, UpdatePosition);
        }
        
        public float PositionZ
        {
            get => GetProperty<float>();
            set => SetProperty(value, UpdatePosition);
        }

        
        public float Yaw
        {
            get => GetProperty<float>();
            set => SetProperty(value, UpdateRotation);
        }
        public float Pitch
        {
            get => GetProperty<float>();
            set => SetProperty(value, UpdateRotation);
        }
        public float Roll
        {
            get => GetProperty<float>();
            set => SetProperty(value, UpdateRotation);
        }

        private void UpdateRotation()
        {
            Camera.Pitch = Pitch;
            Camera.Yaw = Yaw;
            Camera.Roll = Roll;
            
            ScreenPoints = Camera.GetScreenCoordinates(_worldVertices);
        }


        private void UpdateLookAt()
        {
            Camera.SetLookAt(new Vector3(LookAtX, LookAtY, LookAtZ));
            ScreenPoints = Camera.GetScreenCoordinates(_worldVertices);
        }

        private void UpdatePosition()
        {
            Camera.Position = new Vector3(PositionX, PositionY, PositionZ);
            ScreenPoints = Camera.GetScreenCoordinates(_worldVertices);
        }


    }
}