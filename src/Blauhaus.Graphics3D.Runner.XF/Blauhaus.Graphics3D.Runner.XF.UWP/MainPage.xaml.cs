using Blauhaus.DeviceServices.UWP.Ioc;
using Blauhaus.Graphics3D.Runner.Maui;

namespace Blauhaus.Graphics3D.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new MauiApp(new UwpServices()));
        }
    }
}
