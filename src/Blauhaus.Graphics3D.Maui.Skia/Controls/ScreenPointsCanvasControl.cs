﻿using Blauhaus.Graphics3D.Maui.Skia.Controls.Base;

// ReSharper disable StaticMemberInGenericType

namespace Blauhaus.Graphics3D.Maui.Skia.Controls
{
    public class ScreenPointsCanvasControl : BaseCanvasControl
    {
        public ScreenPointsCanvasControl()
        {
        }

        //public ScreenPointsCanvasControl(IViewModel viewModel)
        //{
        //    BindingContext = viewModel;
        //    SetBinding(ScreenPointsProperty, new Binding("ScreenPoints", BindingMode.OneWay, new ActionConverter(Redraw)));
        //}
 
        //public static readonly BindableProperty ScreenPointsProperty = BindableProperty.Create(
        //    propertyName: nameof(ScreenPoints),
        //    returnType: typeof(Vector2[]),
        //    declaringType: typeof(ContentPage),
        //    defaultValue: Array.Empty<Vector2>());

        //public object ScreenPoints
        //{
        //    get => GetValue(ScreenPointsProperty);
        //    set => SetValue(ScreenPointsProperty, value);
        //}
    }


}