using WTAnalyzer.ViewModels;
using WTAnalyzer.ViewModels.VehicleViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WTAnalyzer.Views.VehiclePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AviaPage : ContentPage
    {
        public AviaPage()
        {
            InitializeComponent();
            BindingContext = new AviaViewModel(Navigation);
        }
    }
}