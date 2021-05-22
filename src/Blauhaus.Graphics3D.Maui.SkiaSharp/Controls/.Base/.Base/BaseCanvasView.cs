using System.Numerics;
using Blauhaus.MVVM.Xamarin.Views.ContentViews;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base.Base
{
    public abstract class BaseCanvasView <TViewModel> : BaseContentView<TViewModel>
    {
        protected Vector2 ScreenDimensions;
        
        protected PinchGestureRecognizer? PinchGestureRecognizer;

        protected BaseCanvasView(TViewModel viewModel) : base(viewModel)
        {
        }
    }
}