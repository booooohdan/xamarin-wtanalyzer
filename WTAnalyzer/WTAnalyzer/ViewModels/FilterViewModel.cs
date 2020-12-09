using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand SubmitCommand { get; }
        public FilterViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SubmitCommand = new Command(SubmitHandler);
        }

        private async void SubmitHandler(object obj)
        {
            await Navigation.PopModalAsync();
        }
    }
}
