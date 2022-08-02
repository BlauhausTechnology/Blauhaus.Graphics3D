using Blauhaus.Analytics.Abstractions;
using Blauhaus.Graphics3D.TestApp.Navigation;
using Blauhaus.Graphics3D.TestApp.Views;
using Blauhaus.Ioc.Abstractions;
using Blauhaus.MVVM.Abstractions.TargetNavigation;
using Blauhaus.MVVM.Maui.Applications;

namespace Blauhaus.Graphics3D.TestApp;

public class Test3DApp : BaseMauiApplication
{
    public Test3DApp(
        IServiceLocator serviceLocator, 
        IAnalyticsLogger<Test3DApp> logger, 
        INavigator navigator) 
            : base(serviceLocator, logger, navigator)
    {
    }

    protected override async Task HandleStartingAsync()
    {
        try
        {
            var v = ServiceLocator.Resolve<NakedPyramidView>();
            await Navigator.NavigateAsync(AppNavigation.NakedPyramid);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}