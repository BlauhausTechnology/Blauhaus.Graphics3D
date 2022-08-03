using System.Numerics;
using Blauhaus.Graphics3D.Maui.Skia.Controls;
using Blauhaus.Graphics3D.Maui.Skia.Extensions;
using Blauhaus.Graphics3D.Maui.Skia.Pages;
using Blauhaus.Graphics3D.TestApp.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace Blauhaus.Graphics3D.TestApp.Views;

public class NakedPyramidView : BaseGraphics3DPage<NakedPyramidViewModel, CameraCanvasControl>
{
    public NakedPyramidView(NakedPyramidViewModel viewModel) : base(viewModel)
    {

        CanvasControl.Camera.Position = viewModel.CameraPosition;
        CanvasControl.Camera.LookingAt = viewModel.CameraLookingAt;

        var shapePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 1,
            IsAntialias = true,
            Color = Color.FromRgb(200,0,0).ToSKColor()
        };

        var linePaint = new SKPaint
        {
            StrokeWidth = 5,
            IsAntialias = true,
            Color = Color.FromRgb(200,100,0).ToSKColor()
        };


        CanvasControl.DrawHandler = (canvas, screen) =>
        {
            var screenPoints = CanvasControl.Camera.GetScreenCoordinates(ViewModel.TriangleVertices);
            canvas.DrawIndexedTriangles(screenPoints, ViewModel.TriangleIndices, shapePaint);


            var worldOrigin = CanvasControl.Camera.GetScreenPosition(new Vector3(0, 0, 0));
            var cameraLookingAt = CanvasControl.Camera.GetScreenPosition(CanvasControl.Camera.LookingAt);
            canvas.DrawLine(worldOrigin.ToSKPoint(), cameraLookingAt.ToSKPoint(), linePaint);

            CanvasControl.DrawCameraInfo(canvas);
        };
            
        Content = CanvasControl;

    }

    protected override CameraCanvasControl ConstructCanvas()
    {
        return new CameraCanvasControl(ViewModel);
    }
}