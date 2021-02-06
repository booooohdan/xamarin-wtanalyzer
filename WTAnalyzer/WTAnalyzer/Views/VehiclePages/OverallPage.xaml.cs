using WTAnalyzer.ViewModels;
using WTAnalyzer.ViewModels.VehicleViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WTAnalyzer.Views.VehiclePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverallPage : ContentPage
    {
        public OverallPage()
        {
            InitializeComponent();
            BindingContext = new OverallViewModel(Navigation);
        }
    }
}