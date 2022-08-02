using Blauhaus.Graphics3D.TestApp.ViewModels;
using Blauhaus.MVVM.Maui.Views;

namespace Blauhaus.Graphics3D.TestApp.Views;

public class AppContainerView : BaseMauiShell<AppContainerViewModel>
{
    public AppContainerView(AppContainerViewModel viewModel) : base(viewModel)
    {
        //Items.Add(new ShellContent
        //{
        //    Title = "Home",
        //    Content = new ContentPage
        //    {
        //        BackgroundColor = Color.FromRgb(0,0,100)
        //    }
        //});

        Items.Add(new ShellContent
        {
            Title = "Naked Pyramid",
            ContentTemplate = new DataTemplate(typeof(NakedPyramidView))
        });
    }
}