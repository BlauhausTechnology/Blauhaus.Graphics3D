using System.Collections.Generic;
using System.Numerics;
using Blauhaus.MVVM.Abstractions.ViewModels;

namespace Blauhaus.Graphics3d.ViewModels
{
    public class PyramidViewModel : BaseViewModel
    {

        public PyramidViewModel()
        {
            Vertices = new[]
            {
                new Vector3(0, 0, 3),        
                new Vector3(2, 2, -2),
                new Vector3(2, -2, -2),   
                new Vector3(-2, 2, -2),   
                new Vector3(-2, -2, -2),    
            };

            Indices = new ushort[]
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
        }

        public Vector3[] Vertices { get; }
        public ushort[] Indices { get; }
    }
}