using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WTAnalyzer.ViewModels.BaseViewModels;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels.AboutViewModels
{
    public class FeedbackViewModel : BaseViewModel
    {
        public ICommand RateCommand { get; set; }

        public FeedbackViewModel()
        {
            RateCommand = new Command(RateHandler);
        }

        private void RateHandler(object obj)
        {

        }
    }
}
