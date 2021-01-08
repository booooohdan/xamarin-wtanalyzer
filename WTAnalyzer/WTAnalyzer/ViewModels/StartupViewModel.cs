using Akavache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WTAnalyzer.Cache;
using WTAnalyzer.ViewModels.BaseViewModels;
using WTAnalyzer.Views.ServicePages;
using WTAnalyzer.XmlHandler;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    public class StartupViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        VehicleDataDownloader dataDownloader;
        bool alertResult;
        public StartupViewModel(INavigation navigation)
        {
            Navigation = navigation;
            dataDownloader = new VehicleDataDownloader();
            CheckIfInternetConnected();
        }

        private async void CheckIfInternetConnected()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await Task.Run(dataDownloader.CheckIfDBCached);
                await Navigation.PushAsync(new TabMenuPage());
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    alertResult = await App.Current.MainPage.DisplayAlert("No internet", "Please make sure the Internet is available and restart the app", "Try again", "Quit");
                
                    if (alertResult)
                    {
                        CheckIfInternetConnected();
                    }
                    else
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                });
            }
        }
    }
}
