using Akavache;
using Plugin.StoreReview;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand ShareCommand { get; set; }
        public ICommand DBUpdateCommand { get; set; }
        public ICommand HintsCommand { get; set; }
        private string currentAppVersion;
        private string dbDateUpdate;

        public string CurrentAppVersion
        {
            get => currentAppVersion;
            set 
            { 
                currentAppVersion = value;
                OnPropertyChanged();
            }
        }
        public string DbDateUpdate
        {
            get => dbDateUpdate; 
            set 
            { 
                dbDateUpdate = value;
                OnPropertyChanged();
            }
        }

        public AboutViewModel()
        {
            RateCommand = new Command(RateHandler);
            ShareCommand = new Command(ShareHandler);
            DBUpdateCommand = new Command(DBUpdateHandler);
            HintsCommand = new Command(HintsHandler);

            CurrentAppVersion = AppResources.Version + AppInfo.VersionString;
            var dbDate = BlobCache.UserAccount.GetCreatedAt("cachedArrayOfPlanes").Wait().ToString();
            dbDate = dbDate.Split()[0];
            DbDateUpdate = AppResources.DBUpdate + dbDate;
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
                    //TODO: Add Appstore ID
                    break;
            }
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
                    appLink = "";
                    //TODO: Add link to appstore
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
            //TODO: Implement hints logic
        }
    }
}
