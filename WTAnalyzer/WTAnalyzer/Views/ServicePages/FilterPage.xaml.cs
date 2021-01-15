using WTAnalyzer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WTAnalyzer.Views.ServicePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : ContentPage
    {
        public FilterPage(string vmType)
        {
            InitializeComponent();
            BindingContext = new FilterViewModel(Navigation, vmType);
        }
    }
}