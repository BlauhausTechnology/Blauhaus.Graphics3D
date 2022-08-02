using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base.Base;
using Blauhaus.MVVM.Xamarin.Views.Content;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Pages
{
    public abstract class BaseGraphics3DPage<TViewModel, TCanvas> : BasePage<TViewModel> where TCanvas : BaseCanvasView 
    {
        protected TCanvas CanvasControl { get; }

        protected BaseGraphics3DPage(TViewModel viewModel, TCanvas canvasControl) : base(viewModel)
        {
            CanvasControl = canvasControl;
        }

        protected BaseGraphics3DPage(TViewModel viewModel) : base(viewModel)
        {
            CanvasControl = ConstructCanvas();
        }

        protected abstract TCanvas ConstructCanvas();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CanvasControl.HandleAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CanvasControl.HandleDisappearing();
        }
    }
}