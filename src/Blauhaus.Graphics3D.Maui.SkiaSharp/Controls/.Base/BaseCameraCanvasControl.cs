using System.Numerics;
using Blauhaus.Graphics3D.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base
{
    public abstract class BaseCameraCanvasControl : BaseGLCanvasControl
    {
        private SKPaint? _debugPaint;

        protected BaseCameraCanvasControl(ICameraViewModel cameraViewModel)
        {
            Camera = new Camera(0, 0, Vector3.One, Vector3.Zero, Vector3.UnitZ);

            DimensionsChangedHandler = dimensions => Camera.SetDimensions(dimensions.X, dimensions.Y);

            ZoomHandler = zoom =>
            {
                Camera.Zoom(zoom);

                cameraViewModel.CameraLookingAt = Camera.LookingAt;
                cameraViewModel.CameraPosition = Camera.Position;
                cameraViewModel.CameraLookDirection = Camera.LookDirection;

                Redraw();
            };

            PanHandler = pan =>
            {
                Camera.Pan(pan);
                Redraw();
            };
        }

        public Camera Camera { get; }

        public void DrawCameraInfo(SKCanvas canvas)
        {
            _debugPaint ??= new SKPaint
            {
                Color = Color.White.ToSKColor()
            };
            canvas.DrawText($"Position: {Camera.Position}", 10, 10, new SKFont(SKTypeface.Default), _debugPaint);
            canvas.DrawText($"LookingAt: {Camera.LookingAt}", 10, 30, new SKFont(SKTypeface.Default), _debugPaint);
            canvas.DrawText($"Distance: {Camera.Position - Camera.LookingAt}", 10, 50, new SKFont(SKTypeface.Default), _debugPaint);
            canvas.DrawText($"Look Direction: {Camera.LookDirection}", 10, 70, new SKFont(SKTypeface.Default), _debugPaint);
        }

        public void DrawInfo(SKCanvas canvas, string info, int y)
        {
            _debugPaint ??= new SKPaint
            {
                Color = Color.White.ToSKColor()
            };
            canvas.DrawText(info, 10, y, new SKFont(SKTypeface.Default), _debugPaint); 
        }

        
         
    }
}