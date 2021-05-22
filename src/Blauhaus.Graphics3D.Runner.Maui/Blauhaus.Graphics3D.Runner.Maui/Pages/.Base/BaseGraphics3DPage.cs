using System;
using System.Numerics;
using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base.Base;
using Blauhaus.MVVM.Xamarin.Views.Content;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Runner.Maui.Pages.Base
{
    public abstract class BaseGraphics3DPage<TViewModel> : BasePage<TViewModel> 
    {
        protected BaseGraphics3DPage(TViewModel viewModel) : base(viewModel)
        {
        }

        protected abstract BaseCanvasView<TViewModel> GetCanvas();


        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetCanvas().HandleAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            GetCanvas().HandleDisappearing();
        }
    }
}