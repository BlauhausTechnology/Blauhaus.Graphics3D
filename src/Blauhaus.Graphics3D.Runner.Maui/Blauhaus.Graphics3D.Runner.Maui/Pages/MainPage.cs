using Blauhaus.Graphics3D.Runner.Maui.ViewModels;
using Blauhaus.MVVM.Xamarin.Views.Content;
using Xamarin.Forms;

namespace Blauhaus.Graphics3D.Runner.Maui.Pages
{
    public class MainPage : BasePage<MainViewModel>
    {
        public MainPage(MainViewModel viewModel) : base(viewModel)
        {
        }
    }
}
