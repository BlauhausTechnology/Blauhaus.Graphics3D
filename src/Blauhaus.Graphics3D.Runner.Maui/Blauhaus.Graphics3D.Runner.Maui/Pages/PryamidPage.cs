using System;
using System.Linq;
using System.Numerics;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls;
using Blauhaus.Graphics3D.Runner.Maui.Pages.Base;
using Blauhaus.Graphics3D.Runner.Maui.ViewModels;
using Blauhaus.MVVM.Xamarin.Converters;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using static Xamarin.CommunityToolkit.Markup.GridRowsColumns;

namespace Blauhaus.Graphics3D.Runner.Maui.Pages
{
    public class PryamidPage : BaseGraphics3DPage<PyramidViewModel>
    {

        private enum MainRows { Canvas, Controls }

        public PryamidPage(PyramidViewModel viewModel) : base(viewModel)
        {
            BackgroundColor = Color.Black;

            var canvasView = new ScreenPointsCanvasControl<PyramidViewModel>(viewModel)
            {
                DimensionsChangedHandler = dimensions => ViewModel.ScreenDimensions = dimensions,
                DrawHandler = canvas =>
                {
                    canvas.Clear();

                    var paint = new SKPaint {Style = SKPaintStyle.Stroke, StrokeWidth = 1, IsAntialias = true, Color = Color.Red.ToSKColor()};

                    for (var i = 0; i < ViewModel.Indices.Length / 3; i++)
                    {
                        using var trianglePath = new SKPath();
                        trianglePath.MoveTo(ViewModel.ScreenPoints[ViewModel.Indices[i * 3 + 0]].X, ViewModel.ScreenPoints[ViewModel.Indices[i * 3 + 0]].Y);
                        trianglePath.LineTo(ViewModel.ScreenPoints[ViewModel.Indices[i * 3 + 1]].X, ViewModel.ScreenPoints[ViewModel.Indices[i * 3 + 1]].Y);
                        trianglePath.LineTo(ViewModel.ScreenPoints[ViewModel.Indices[i * 3 + 2]].X, ViewModel.ScreenPoints[ViewModel.Indices[i * 3 + 2]].Y);
                        trianglePath.Close();
                        canvas.DrawPath(trianglePath, paint);
                    }
                },
                ZoomHandler = ViewModel.Zoom
            };


            var controls = new CanvasControls();

            Content = new Grid
            {
                RowDefinitions =  Rows.Define(
                    (MainRows.Canvas, Stars(1)),
                    (MainRows.Controls, 400)),

                Children =
                {
                    canvasView.Row(MainRows.Canvas),
                    controls.Row(MainRows.Controls)
                }
            };
        }
         
        private class CanvasControls : Grid
        {
            private enum ControlColumns { LookAt, Position, Rotation }

            public CanvasControls()
            {

                ColumnDefinitions = Columns.Define(
                    (ControlColumns.LookAt, Stars(1)),
                    (ControlColumns.Position, Stars(1)),
                    (ControlColumns.Rotation, Stars(1)));

                Children.Add(new StackLayout
                {
                    Children =
                    {
                        new Label{Text = "Look At X"},
                        new Slider {Minimum = -10, Maximum = 10}.Bind(nameof(PyramidViewModel.LookAtX)),
                        new Entry().Bind(nameof(PyramidViewModel.LookAtX)),
                        
                        new Label{Text = "Look At Y"},
                        new Slider {Minimum = -10, Maximum = 10}.Bind(nameof(PyramidViewModel.LookAtY)),
                        new Entry().Bind(nameof(PyramidViewModel.LookAtY)),
                        
                        new Label{Text = "Look At Z"},
                        new Slider {Minimum = -10, Maximum = 10}.Bind(nameof(PyramidViewModel.LookAtZ)),
                        new Entry().Bind(nameof(PyramidViewModel.LookAtZ)),
                    }
                }.Column(ControlColumns.LookAt));

                Children.Add(new StackLayout
                {
                    Children =
                    {
                        new Label{Text = "Position X"},
                        new Slider {Minimum = -50, Maximum = 50}.Bind(nameof(PyramidViewModel.PositionX)),
                        new Entry().Bind(nameof(PyramidViewModel.PositionX)),
                        
                        new Label{Text = "Position Y"},
                        new Slider {Minimum = -50, Maximum = 50}.Bind(nameof(PyramidViewModel.PositionY)),
                        new Entry().Bind(nameof(PyramidViewModel.PositionY)),
                        
                        new Label{Text = "Position Z"},
                        new Slider {Minimum = -50, Maximum = 50}.Bind(nameof(PyramidViewModel.PositionZ)),
                        new Entry().Bind(nameof(PyramidViewModel.PositionZ)),
                    }
                }.Column(ControlColumns.Position));

                Children.Add(new StackLayout
                {
                    Children =
                    {
                        new Label{Text = "Yaw"},
                        new Slider {Minimum = -3f, Maximum = 3f}.Bind(nameof(PyramidViewModel.Yaw)),
                        new Entry().Bind(nameof(PyramidViewModel.Yaw)),
                        
                        new Label{Text = "Pitch"},
                        new Slider {Minimum = -3f, Maximum = 3f}.Bind(nameof(PyramidViewModel.Pitch)),
                        new Entry().Bind(nameof(PyramidViewModel.Pitch)),

                        new Label{Text = "Roll"},
                        new Slider {Minimum = -3f, Maximum = 3f}.Bind(nameof(PyramidViewModel.Roll)),
                        new Entry().Bind(nameof(PyramidViewModel.Roll)),
                    }
                }.Column(ControlColumns.Rotation));
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