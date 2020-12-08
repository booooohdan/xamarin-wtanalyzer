using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    public class AviaViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public AviaViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        private string b = "BindingTextAvia";
        public string B
        {
            get => b;
            set
            {
                b = value;
                OnPropertyChanged();
            }
        }
    }
}
