using System;
using System.Numerics;
using Blauhaus.Graphics3D.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base
{
    public enum PanAction{RotateWorld, RotateCameraAroundTarget}
    
    public abstract class BaseCameraCanvasControl : BaseGLCanvasControl
    {
        private readonly ICameraViewModel _cameraViewModel;
        private SKPaint? _debugPaint;
        public PanAction PanAction;

        protected BaseCameraCanvasControl(ICameraViewModel cameraViewModel)
        {
            _cameraViewModel = cameraViewModel;
            Camera = new Camera(0, 0, Vector3.One, Vector3.Zero, Vector3.UnitZ);

            DimensionsChangedHandler = dimensions => Camera.SetDimensions(dimensions.X, dimensions.Y);

            ZoomHandler = zoom =>
            {
                Camera.Zoom(zoom);
                UpdateViewModel();
                Redraw();
            };

            PanHandler = pan =>
            {
                switch (PanAction)
                {
                    case PanAction.RotateWorld:
                        Camera.RotateWorld(pan);
                        break;
                    case PanAction.RotateCameraAroundTarget:
                        Camera.RotateAboutCameraLookingAt(pan);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                UpdateViewModel();
                Redraw();
            };
        }

        private void UpdateViewModel()
        {
            _cameraViewModel.CameraLookingAt = Camera.LookingAt;
            _cameraViewModel.CameraPosition = Camera.Position;
            _cameraViewModel.CameraLookDirection = Camera.LookDirection;
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