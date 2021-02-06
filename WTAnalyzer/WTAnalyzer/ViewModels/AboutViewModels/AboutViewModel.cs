using Akavache;
using Plugin.StoreReview;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;
using WTAnalyzer.Resx;
using WTAnalyzer.ViewModels.BaseViewModels;
using WTAnalyzer.Views.ServicePages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels.AboutViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ICommand RateCommand { get; set; }
        public ICommand PatreonCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public ICommand DBUpdateCommand { get; set; }
        public ICommand HintsCommand { get; set; }

        public AboutViewModel()
        {
            RateCommand = new Command(RateHandler);
            PatreonCommand = new Command(PatreonHandler);
            ShareCommand = new Command(ShareHandler);
            DBUpdateCommand = new Command(DBUpdateHandler);
            HintsCommand = new Command(HintsHandler);
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

        private async void PatreonHandler(object obj)
        {
            var uri = "https://www.patreon.com/wave_app";
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        private async void ShareHandler(object obj)
        {
            string appLink = string.Empty;

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    appLink = "https://play.google.com/store/apps/details?id=com.wtwave.wtinsider";
                        break;
                case Device.iOS:
                    appLink = "Thunder Analyzer";
                    break;
            }

            await Share.RequestAsync(new ShareTextRequest
            {
                Title = AppResources.ShareWithFriends,
                Text = AppResources.CheckThisCoolApp,                
                Uri = appLink,
                
            });
        }

        private async void DBUpdateHandler(object obj)
        {
            await BlobCache.UserAccount.Invalidate("cachedArrayOfPlanes");
            await BlobCache.UserAccount.Invalidate("cachedArrayOfTanks");
            await BlobCache.UserAccount.Invalidate("cachedArrayOfHelis");
            await BlobCache.UserAccount.Invalidate("cachedArrayOfShips");
            Process.GetCurrentProcess().Kill();
        }

        private void HintsHandler(object obj)
        {
            
        }
    }
}
