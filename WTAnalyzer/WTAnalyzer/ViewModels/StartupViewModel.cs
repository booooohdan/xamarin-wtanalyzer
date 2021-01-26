using System.Diagnostics;
using System.Threading.Tasks;
using WTAnalyzer.Cache;
using WTAnalyzer.Resx;
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
        HeliFilterDataSetter filterHeliDataSetter;
        ShipFilterDataSetter filterShipDataSetter;
        bool alertResult;
        private string startupLabel;
        public string StartupLabel
        {
            get => startupLabel;
            set
            {
                startupLabel = value;
                OnPropertyChanged();
            }
        }
        public StartupViewModel(INavigation navigation)
        {
            Navigation = navigation;
            dataDownloader = new VehicleDataDownloader();
            filterPlaneDataSetter = new PlaneFilterDataSetter();
            filterTankDataSetter = new TankFilterDataSetter();
            filterHeliDataSetter = new HeliFilterDataSetter();
            filterShipDataSetter = new ShipFilterDataSetter();

            CheckIfInternetConnected();
        }

        private async void CheckIfInternetConnected()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                StartupLabel = AppResources.LoadingVehicleData;
                await Task.Run(dataDownloader.CheckIfDBCached);
                StartupLabel = string.Empty;
                await Task.Run(filterPlaneDataSetter.InitAsync);
                await Task.Run(filterTankDataSetter.InitAsync);
                await Task.Run(filterHeliDataSetter.InitAsync);
                await Task.Run(filterShipDataSetter.InitAsync);
                await Navigation.PushAsync(new TabMenuPage());
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    alertResult = await App.Current.MainPage.DisplayAlert(AppResources.NoInternet, AppResources.PleaseMakeSure, AppResources.TryAgain, AppResources.Quit);

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
