using System;
using System.Collections.Generic;
using System.Text;
using WTAnalyzer.ViewModels.BaseViewModels;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    [QueryProperty("Name", "name")]
    public class OnePointBrViewModel : BaseViewModel
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = Uri.UnescapeDataString(value);
                OnPropertyChanged();
            }
        }

        public OnePointBrViewModel()
        {

        }
    }
}
