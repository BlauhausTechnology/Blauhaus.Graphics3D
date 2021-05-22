using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base.Base;
using Blauhaus.MVVM.Xamarin.Views.Content;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Pages
{
    public abstract class BaseGraphics3DPage<TViewModel, TCanvas> : BasePage<TViewModel> where TCanvas : BaseCanvasView, new()
    {
        protected TCanvas Canvas { get; }

        protected BaseGraphics3DPage(TViewModel viewModel, TCanvas canvas) : base(viewModel)
        {
            Canvas = canvas;
        }

        protected BaseGraphics3DPage(TViewModel viewModel) : base(viewModel)
        {
            Canvas = new TCanvas();
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