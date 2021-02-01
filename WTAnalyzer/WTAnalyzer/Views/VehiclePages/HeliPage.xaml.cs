using WTAnalyzer.ViewModels;
using WTAnalyzer.ViewModels.VehicleViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WTAnalyzer.Views.VehiclePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeliPage : ContentPage
    {
        public HeliPage()
        {
            InitializeComponent();
            BindingContext = new HeliViewModel(Navigation);
        }
    }
}