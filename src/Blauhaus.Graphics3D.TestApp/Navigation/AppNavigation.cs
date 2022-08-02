using Blauhaus.MVVM.Abstractions.TargetNavigation;

namespace Blauhaus.Graphics3D.TestApp.Navigation;


public static class AppNavigation
{

    internal static class Views
    {
        public static ViewIdentifier AppContainerView = ViewIdentifier.Create();

        public static ViewIdentifier NakedPyramidView = ViewIdentifier.Create();
    }

    public static NavigationTarget AppContainer = NavigationTarget.CreateContainer(Views.AppContainerView);
    public static NavigationTarget NakedPyramid = NavigationTarget.CreateView(Views.NakedPyramidView);
}