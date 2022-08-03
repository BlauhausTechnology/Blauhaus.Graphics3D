using Blauhaus.Graphics3D.Maui.Skia.Controls.Base.Base;
using Blauhaus.MVVM.Abstractions.ViewModels;
using Blauhaus.MVVM.Maui.Views;

namespace Blauhaus.Graphics3D.Maui.Skia.Pages
{
    public abstract class BaseGraphics3DPage<TViewModel, TCanvas> : BaseUpdateContentPage<TViewModel> where TCanvas : BaseCanvasView where TViewModel : IViewModel
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