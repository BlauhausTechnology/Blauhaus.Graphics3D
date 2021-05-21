using System;
using System.Numerics;
using Blauhaus.MVVM.Xamarin.Views.Content;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Runner.Maui.Pages.Base
{
    public abstract class BaseGraphics3DPage<TViewModel> : BasePage<TViewModel>
    {
        protected BaseGraphics3DPage(TViewModel viewModel) : base(viewModel)
        {
        }

       
    }
}