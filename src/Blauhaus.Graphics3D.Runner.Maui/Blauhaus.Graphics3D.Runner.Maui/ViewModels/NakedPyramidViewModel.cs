using System.Numerics;
using Blauhaus.Graphics3D.ViewModels;
using Blauhaus.MVVM.Abstractions.ViewModels;

namespace Blauhaus.Graphics3D.Runner.Maui.ViewModels
{
    public class NakedPyramidViewModel : BaseViewModel, ICameraViewModel
    {
        public NakedPyramidViewModel()
        {
            
            CameraPosition = new Vector3(10, 0, 0);
            CameraLookingAt = new Vector3(5, 0, 0);

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
        public Vector3 CameraLookDirection { get; set; }

        public int[] TriangleIndices { get; private set; }
        public Vector3[] TriangleVertices
        {
            get => GetProperty<Vector3[]>();
            set => SetProperty(value);
        }
    }
}