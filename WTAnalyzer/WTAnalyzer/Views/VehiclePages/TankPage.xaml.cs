using WTAnalyzer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WTAnalyzer.Views.VehiclePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TankPage : ContentPage
    {
        public TankPage()
        {
            InitializeComponent();
            BindingContext = new TankViewModel(Navigation);
        }
    }
}