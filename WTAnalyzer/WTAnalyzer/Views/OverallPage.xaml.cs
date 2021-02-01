using WTAnalyzer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WTAnalyzer.Views
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