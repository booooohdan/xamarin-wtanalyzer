using System.Diagnostics;
using System.Threading.Tasks;
using WTAnalyzer.Cache;
using WTAnalyzer.ViewModels.BaseViewModels;
using WTAnalyzer.Views.ServicePages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    public class StartupViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        VehicleDataDownloader dataDownloader;
        PlaneFilterDataSetter filterPlaneDataSetter;
        TankFilterDataSetter filterTankDataSetter;
        bool alertResult;
        public StartupViewModel(INavigation navigation)
        {
            Navigation = navigation;
            dataDownloader = new VehicleDataDownloader();
            filterPlaneDataSetter = new PlaneFilterDataSetter();
            filterTankDataSetter = new TankFilterDataSetter();

            CheckIfInternetConnected();
        }

        private async void CheckIfInternetConnected()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await Task.Run(dataDownloader.CheckIfDBCached);
                await Task.Run(filterPlaneDataSetter.InitAsync);
                await Task.Run(filterTankDataSetter.InitAsync);
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
