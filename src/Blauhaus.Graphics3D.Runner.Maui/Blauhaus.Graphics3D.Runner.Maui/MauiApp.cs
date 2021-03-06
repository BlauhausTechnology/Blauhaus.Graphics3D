using Blauhaus.Analytics.Console.Ioc;
using Blauhaus.Common.ValueObjects.BuildConfigs;
using Blauhaus.DeviceServices.Common.Ioc;
using Blauhaus.Graphics3D.Runner.Maui.Pages;
using Blauhaus.Graphics3D.Runner.Maui.ViewModels;
using Blauhaus.MVVM.Services;
using Blauhaus.MVVM.Xamarin.App;
using Blauhaus.MVVM.Xamarin.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace Blauhaus.Graphics3D.Runner.Maui
{
    public class MauiApp : BaseFormsApp
    { 
        
        public MauiApp(IServiceCollection platformServices) : base(platformServices)
        {
        }
        
        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvvmServices()
                .AddDeviceServices()
                .RegisterConsoleLoggerClientService();
                
            services
                .AddPage<MainPage, MainViewModel>()
                .AddPage<PryamidPage, PyramidViewModel>()
                .AddPage<NakedPyramidPage, NakedPyramidViewModel>()
                .AddPage<FourDotsPage, FourDotsViewModel>();
        }
        
        protected override void HandleAppStarting()
        {
            base.HandleAppStarting();
            //MainPage = AppServiceLocator.Resolve<FourDotsPage>();
            //MainPage = AppServiceLocator.Resolve<PryamidPage>();
            MainPage = AppServiceLocator.Resolve<NakedPyramidPage>();
        }

        protected override IBuildConfig GetBuildConfig()
        {
            return BuildConfig.Debug;
        }

    }
}
