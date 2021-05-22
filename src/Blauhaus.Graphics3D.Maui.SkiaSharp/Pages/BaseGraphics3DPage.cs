using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base.Base;
using Blauhaus.MVVM.Xamarin.Views.Content;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Pages
{
    public abstract class BaseGraphics3DPage<TViewModel, TCanvas> : BasePage<TViewModel> where TCanvas : BaseCanvasView
    {
        protected abstract TCanvas Canvas { get; }

        protected BaseGraphics3DPage(TViewModel viewModel) : base(viewModel)
        {
        }
         
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Canvas.HandleAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Canvas.HandleDisappearing();
        }
    }
}