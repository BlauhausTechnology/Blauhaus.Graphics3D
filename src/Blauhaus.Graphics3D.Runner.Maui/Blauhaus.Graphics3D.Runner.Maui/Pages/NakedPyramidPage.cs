using System;
using System.Numerics;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Extensions;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Pages;
using Blauhaus.Graphics3D.Runner.Maui.ViewModels;
using Blauhaus.MVVM.Xamarin.Converters;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Runner.Maui.Pages
{
    public class NakedPyramidPage : BaseGraphics3DPage<NakedPyramidViewModel, CameraCanvasControl>
    {
        public NakedPyramidPage(NakedPyramidViewModel viewModel) : base(viewModel)
        {

            CanvasControl.Camera.Position = viewModel.CameraPosition;
            CanvasControl.Camera.LookingAt = viewModel.CameraLookingAt;

            CanvasControl.DrawHandler = canvas =>
            {
                var screenPoints = CanvasControl.Camera.GetScreenCoordinates(ViewModel.TriangleVertices);

                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 1,
                    IsAntialias = true,
                    Color = Color.Red.ToSKColor()
                };

                canvas.DrawIndexedTriangles(screenPoints, ViewModel.TriangleIndices, paint);
            };
            
            Content = CanvasControl;

        }
         
    }
}