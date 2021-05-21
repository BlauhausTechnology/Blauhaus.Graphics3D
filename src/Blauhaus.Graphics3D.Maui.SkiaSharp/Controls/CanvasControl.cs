using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using Blauhaus.MVVM.Abstractions.ViewModels;
using Blauhaus.MVVM.Xamarin.Converters;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Controls
{
    public class CanvasControl : ContentView
    {
        private readonly SKCanvasView _canvasView;
        private Vector2 _screenDimensions;

        private PinchGestureRecognizer? _pinchGestureRecognizer;


        public CanvasControl(object viewModel)
        {
            BindingContext = viewModel;

            _canvasView = new SKCanvasView();
            _canvasView.PaintSurface += OnCanvasViewPaintSurface;

            SetBinding(ScreenPointsProperty, new Binding("ScreenPoints", BindingMode.OneWay, new ActionConverter(() =>
            {
                _canvasView.InvalidateSurface();    
            })));

            Content = _canvasView;
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear();

            var dimensionsHaveChanged = _screenDimensions.X != info.Width || _screenDimensions.Y != info.Height;
            _screenDimensions = new Vector2(info.Width, info.Height);

            if (dimensionsHaveChanged && DimensionsChanged != null)
            {
                DimensionsChanged.Invoke(_screenDimensions);
            }

            Draw?.Invoke(canvas);

        }

        public Action<SKCanvas>? Draw { get; set; }
        public Action<Vector2>? DimensionsChanged { get; set; }

        public void HandleZoom(Action<ZoomEvent> pinchHandler)
        {
            _pinchGestureRecognizer = new PinchGestureRecognizer();
            _pinchGestureRecognizer.PinchUpdated += (_, args) =>
            {
                if (args.Status == GestureStatus.Running)
                {
                    pinchHandler?.Invoke(new ZoomEvent(args.ScaleOrigin.X, args.ScaleOrigin.Y, args.Scale));
                }
            };
            GestureRecognizers.Add(_pinchGestureRecognizer);

            if (Device.Idiom == TargetIdiom.Desktop)
            {
                if (_canvasView.EnableTouchEvents == false)
                {
                    _canvasView.EnableTouchEvents = true;
                    _canvasView.Touch += (_, args) =>
                    {
                        if (args.ActionType == SKTouchAction.WheelChanged)
                        {
                            if (args.WheelDelta != 0)
                            {
                                var scale = (_screenDimensions.Y + args.WheelDelta) / _screenDimensions.Y;
                                var x = args.Location.X;
                                var y = args.Location.Y;

                                pinchHandler?.Invoke(new ZoomEvent(x, y, scale));
                            }
                        }
                        
                    };
                }
            }
            
        }
          

        
        public static readonly BindableProperty ScreenPointsProperty = BindableProperty.Create(
            propertyName: nameof(ScreenPoints),
            returnType: typeof(Vector2[]),
            declaringType: typeof(ContentPage),
            defaultValue: Array.Empty<Vector2>());



        public object ScreenPoints
        {
            get => GetValue(ScreenPointsProperty);
            set => SetValue(ScreenPointsProperty, value);
        }
    }


}