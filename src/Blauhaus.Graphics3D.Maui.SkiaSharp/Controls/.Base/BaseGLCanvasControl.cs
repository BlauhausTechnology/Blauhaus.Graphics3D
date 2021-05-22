// ReSharper disable InconsistentNaming

using Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base.Base;

namespace Blauhaus.Graphics3D.Maui.SkiaSharp.Controls.Base
{
    public abstract class BaseGLCanvasControl<TViewModel> : BaseCanvasView<TViewModel>
    {
        protected BaseGLCanvasControl(TViewModel viewModel) : base(viewModel)
        {
        }
    }
}