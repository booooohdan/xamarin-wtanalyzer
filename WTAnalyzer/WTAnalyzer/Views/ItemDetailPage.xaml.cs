using System.ComponentModel;
using WTAnalyzer.ViewModels;
using Xamarin.Forms;

namespace WTAnalyzer.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}