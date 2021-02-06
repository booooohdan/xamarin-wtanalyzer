using WTAnalyzer.ViewModels;
using WTAnalyzer.ViewModels.ServiceViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WTAnalyzer.Views.ServicePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartupPage : ContentPage
    {
        public StartupPage()
        {
            InitializeComponent();
            BindingContext = new StartupViewModel();
        }
    }
}