using Plugin.StoreReview;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WTAnalyzer.ViewModels.BaseViewModels;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels.AboutViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ICommand RateCommand { get; set; }

        public AboutViewModel()
        {
            RateCommand = new Command(RateHandler);
        }

        private void RateHandler(object obj)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    CrossStoreReview.Current.OpenStoreReviewPage("com.wtwave.wtinsider");
                    break;
                case Device.iOS:
                    CrossStoreReview.Current.OpenStoreReviewPage(""/*"1542964380"*/);
                    break;
            }
        }
    }
}
