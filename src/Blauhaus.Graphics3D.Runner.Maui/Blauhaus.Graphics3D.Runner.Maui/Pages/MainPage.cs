using Blauhaus.Graphics3D.Runner.Maui.Pages.Base;
using Blauhaus.Graphics3D.Runner.Maui.ViewModels;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Runner.Maui.Pages
{
    public class MainPage : BaseGraphics3DPage<MainViewModel>
    {
        public MainPage(MainViewModel viewModel) : base(viewModel)
        {
        }
    }
}
