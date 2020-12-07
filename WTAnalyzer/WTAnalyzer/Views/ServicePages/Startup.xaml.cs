using WTAnalyzer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WTAnalyzer.Views.ServicePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Startup : ContentPage
    {
        public Startup()
        {
            InitializeComponent();
            BindingContext = new StartupViewModel(Navigation);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
        }       
    }
}