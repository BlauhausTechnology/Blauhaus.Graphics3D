using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base;
using Blauhaus.MVVM.Abstractions.ViewModels;
using Blauhaus.MVVM.Xamarin.Converters;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
// ReSharper disable StaticMemberInGenericType

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Controls
{
    public class ScreenPointsCanvasControl<TViewModel> : BaseGLCanvasControl<TViewModel>
    {
        public ScreenPointsCanvasControl(TViewModel viewModel) : base(viewModel)
        {
            SetBinding(ScreenPointsProperty, new Binding("ScreenPoints", BindingMode.OneWay, new ActionConverter(Redraw)));
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