using System.Numerics;
using Blauhaus.MVVM.Abstractions.ViewModels;

namespace Blauhaus.Graphics3D.Runner.Maui.ViewModels
{
    public class NakedPyramidViewModel : BaseViewModel
    {
        public NakedPyramidViewModel()
        {
            
            CameraPosition = new Vector3(10, 0, 0);
            CameraLookingAt = Vector3.Zero;

            TriangleVertices = new Vector3[]
            {
                new( 0,  0,  3),        
                new( 2,  2, -2),
                new( 2, -2, -2),   
                new(-2,  2, -2),   
                new(-2, -2, -2),    
            };

            TriangleIndices = new[]
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

        public Vector3 CameraPosition { get; set; }
        public Vector3 CameraLookingAt { get; set; }

        public int[] TriangleIndices { get; private set; }
        public Vector3[] TriangleVertices
        {
            get => GetProperty<Vector3[]>();
            set => SetProperty(value);
        }
    }
}