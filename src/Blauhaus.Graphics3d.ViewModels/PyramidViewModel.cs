using System;
using System.Collections.Generic;
using System.Numerics;
using Blauhaus.Graphics3D;
using Blauhaus.MVVM.Abstractions.ViewModels;
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Blauhaus.Graphics3d.ViewModels
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

            Camera = new Camera(1, 1, new Vector3(-10, 0, 0), Vector3.Zero, Vector3.UnitZ);
            ScreenPoints = Array.Empty<Vector2>();
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


        public Vector2[] ScreenPoints { get; private set; }
        public int[] Indices { get; }
    }
}