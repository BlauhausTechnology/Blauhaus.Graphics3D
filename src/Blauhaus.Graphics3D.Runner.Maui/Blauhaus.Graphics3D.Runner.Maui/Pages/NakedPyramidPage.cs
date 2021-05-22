using System;
using System.Numerics;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Pages;
using Blauhaus.Graphics3D.Runner.Maui.ViewModels;
using Blauhaus.MVVM.Xamarin.Converters;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Runner.Maui.Pages
{
    public class NakedPyramidPage : BaseGraphics3DPage<NakedPyramidViewModel, IndexedTriangleCanvasControl>
    {
        public NakedPyramidPage(NakedPyramidViewModel viewModel) : base(viewModel)
        {

            Canvas.CameraPosition = viewModel.CameraPosition;
            Canvas.CameraLookingAt = viewModel.CameraLookingAt;

            SetBinding(TriangleVerticesProperty, new Binding(nameof(ViewModel.TriangleVertices), BindingMode.OneWay, new ActionConverter(() =>
            {
                Canvas.Vertices = ViewModel.TriangleVertices;
                Canvas.Indices = viewModel.TriangleIndices;
                Canvas.Redraw();
            })));
            
            Content = Canvas;

        }

 
        public static readonly BindableProperty TriangleVerticesProperty = BindableProperty.Create(
            propertyName: nameof(TriangleVertices),
            returnType: typeof(Vector3[]),
            declaringType: typeof(ContentPage),
            defaultValue: Array.Empty<Vector3>());

        public object TriangleVertices
        {
            get => GetValue(TriangleVerticesProperty);
            set => SetValue(TriangleVerticesProperty, value);
        }
    }
}