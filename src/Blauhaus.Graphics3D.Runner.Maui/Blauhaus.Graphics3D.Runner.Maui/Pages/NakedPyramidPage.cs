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

            Canvas.Camera.Position = viewModel.CameraPosition;
            Canvas.Camera.LookingAt = viewModel.CameraLookingAt;

            Canvas.DrawHandler = canvas =>
            {
                var screenPoints = Canvas.Camera.GetScreenCoordinates(ViewModel.TriangleVertices);

                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 1,
                    IsAntialias = true,
                    Color = Color.Red.ToSKColor()
                };

                canvas.DrawIndexedTriangles(screenPoints, ViewModel.TriangleIndices, paint);
            };
            
            Content = Canvas;

        }
         
    }
}