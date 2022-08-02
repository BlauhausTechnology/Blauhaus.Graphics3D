using Blauhaus.Analytics.Serilog.Ioc;
using Blauhaus.Graphics3D.TestApp.Navigation;
using Blauhaus.Graphics3D.TestApp.ViewModels;
using Blauhaus.Graphics3D.TestApp.Views;
using Blauhaus.Ioc.Abstractions;
using Blauhaus.MVVM.Abstractions.TargetNavigation;
using Blauhaus.MVVM.Ioc;
using Blauhaus.MVVM.Maui.Ioc;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Blauhaus.Graphics3D.TestApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<Test3DApp>()
                .UseSkiaSharp();

            builder.Services
                .AddMauiApplication()
                .AddExecutingCommands()
                .AddSerilogAnalytics("3D", config => {});


            builder.Services
                .AddMauiView<AppContainerView, AppContainerViewModel>(AppNavigation.Views.AppContainerView)
                .AddMauiView<NakedPyramidView, NakedPyramidViewModel>(AppNavigation.Views.NakedPyramidView);
            
            return builder.Build();
        }
    }
}