using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Blauhaus.Graphics3D.Runner.Maui.Pages.Base;
using Blauhaus.Graphics3d.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Runner.Maui.Pages
{
    public class FourDotsPage : BaseGraphics3DPage<FourDotsViewModel>
    {
        public FourDotsPage(FourDotsViewModel viewModel) : base(viewModel)
        {
            BackgroundColor = Color.LightSlateGray;
            
            var canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }

        private static void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {

            var info = args.Info;
            var surface = args.Surface;
            var canvas = surface.Canvas;
            canvas.Clear();

            var pointsToShow = new[]
            {
                new Vector4(0, 0, 0, 1),   //middle of screen, middle distance
                new Vector4(2, 0, 0, 1),   //right of screen, middle distance
                new Vector4(-1, 1, 2, 1),  //top left of screen, far distance
                new Vector4(1, -1, -2, 1)   //botth right of screen, near distance
            };

            var worldCoordinateSystem = Matrix4x4.Identity;
            var viewCoordinateSystem = Matrix4x4.CreateLookAt(new Vector3(0, 0, -5), Vector3.Zero, Vector3.UnitY);
            var projectionCoordinateSystem = Matrix4x4.CreatePerspectiveFieldOfView((float) (Math.PI / 4f), info.Width / (float)info.Height, 0.01f, 10f);

            var camera = new Camera(info.Width, info.Height);

            var worldToView = Matrix4x4.Multiply(worldCoordinateSystem, viewCoordinateSystem);
            var viewToProjection = Matrix4x4.Multiply(worldToView, projectionCoordinateSystem);
            var viewToProjectionCanvas = ConvertToScreen(pointsToShow, camera.ScreenMatrix, info.Width, info.Height);

            var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 25
            };

            for (var i = 0; i < viewToProjectionCanvas.Count(); i++)
            {

                paint.Color = Colors[i];
                canvas.DrawCircle(viewToProjectionCanvas[i].X, viewToProjectionCanvas[i].Y, 10, paint);
            } 
        }

        private static readonly SKColor[] Colors = new[]
        {
            Color.Red.ToSKColor(),
            Color.Green.ToSKColor(),
            Color.Blue.ToSKColor(),
            Color.Yellow.ToSKColor(),
        };

        private static IReadOnlyList<Vector2> ConvertToScreen(IReadOnlyList<Vector4> points, Matrix4x4 matrix, float width, float height)
        {
            var canvasCoordinates = new Vector2[points.Count];
            for (var i = 0; i < points.Count; i++)
            {

                var pointInCameraSpace = Vector4.Transform(points[i], matrix); 
                var screenX = pointInCameraSpace.X / -pointInCameraSpace.Z * width/2f + width/2f;
                var screenY = pointInCameraSpace.Y / -pointInCameraSpace.Z * height/2f + height/2f;
                canvasCoordinates[i] = new Vector2(screenX, screenY);
            }

            return canvasCoordinates;
        }
    }
}