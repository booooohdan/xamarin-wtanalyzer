using Akavache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WTAnalyzer.Cache;
using WTAnalyzer.Resx;
using WTAnalyzer.ViewModels.BaseViewModels;
using WTAnalyzer.XmlHandler;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels.ServiceViewModels
{
    public class StartupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ArrayOfPlanes arrayOfPlanes;
        ArrayOfTanks arrayOfTanks;
        ArrayOfHelis arrayOfHelis;
        ArrayOfShips arrayOfShips;
        PlaneFilterDataSetter filterPlaneDataSetter;
        TankFilterDataSetter filterTankDataSetter;
        HeliFilterDataSetter filterHeliDataSetter;
        ShipFilterDataSetter filterShipDataSetter;
        bool alertResult;

        #region View Property

        private bool internetActivity;
        private bool internetIcon;
        private bool planeActivity;
        private bool planeIcon;
        private bool tanksActivity;
        private bool tanksIcon;
        private bool heliActivity;
        private bool heliIcon;
        private bool shipActivity;
        private bool shipIcon;

        public bool InternetActivity
        {
            get => internetActivity;
            set
            {
                internetActivity = value;
                OnPropertyChanged();
            }
        }
        public bool InternetIcon
        {
            get => internetIcon;
            set
            {
                internetIcon = value;
                OnPropertyChanged();
            }
        }
        public bool PlaneActivity
        {
            get => planeActivity;
            set
            {
                planeActivity = value;
                OnPropertyChanged();
            }
        }
        public bool PlaneIcon
        {
            get => planeIcon;
            set
            {
                planeIcon = value;
                OnPropertyChanged();
            }
        }
        public bool TankActivity
        {
            get => tanksActivity;
            set
            {
                tanksActivity = value;
                OnPropertyChanged();
            }
        }
        public bool TankIcon
        {
            get => tanksIcon;
            set
            {
                tanksIcon = value;
                OnPropertyChanged();
            }
        }
        public bool HeliActivity
        {
            get => heliActivity;
            set
            {
                heliActivity = value;
                OnPropertyChanged();
            }
        }
        public bool HeliIcon
        {
            get => heliIcon;
            set
            {
                heliIcon = value;
                OnPropertyChanged();
            }
        }
        public bool ShipActivity
        {
            get => shipActivity;
            set
            {
                shipActivity = value;
                OnPropertyChanged();
            }
        }
        public bool ShipIcon
        {
            get => shipIcon;
            set
            {
                shipIcon = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public StartupViewModel()
        {
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
                InternetActivity = false;
                InternetIcon = true;
                await Task.Run(CheckIfDBCached);

                await Task.Run(filterPlaneDataSetter.InitAsync);
                await Task.Run(filterTankDataSetter.InitAsync);
                await Task.Run(filterHeliDataSetter.InitAsync);
                await Task.Run(filterShipDataSetter.InitAsync);
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                InternetActivity = true;
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

        public async Task<Task> CheckIfDBCached()
        {
            try
            {
                Registrations.Start("WTAAkavacheCache");
                var arrayOfPlansCached = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
                var arrayOfTanksCached = await BlobCache.UserAccount.GetObject<ArrayOfTanks>("cachedArrayOfTanks");
                var arrayOfHelisCached = await BlobCache.UserAccount.GetObject<ArrayOfHelis>("cachedArrayOfHelis");
                var arrayOfShipsCached = await BlobCache.UserAccount.GetObject<ArrayOfShips>("cachedArrayOfShips");
            }
            catch (KeyNotFoundException)
            {
                await GetPlanesListFromApiAsync();
                await GetTanksListFromApiAsync();
                await GetHelisListFromApiAsync();
                await GetShipsListFromApiAsync();
            }
            return Task.CompletedTask;
        }

        private async Task GetPlanesListFromApiAsync()
        {
            PlaneActivity = true;
            string URL = "http://bo7145907-001-site2.ftempurl.com/wtvapi/vehicles/planes";
            ApiXmlReaderInitial initial = new ApiXmlReaderInitial();
            XmlReader xReader = initial.ApiXmlReader(URL);
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfPlanes));
            arrayOfPlanes = (ArrayOfPlanes)serializer.Deserialize(xReader);
            await BlobCache.UserAccount.InsertObject("cachedArrayOfPlanes", arrayOfPlanes, TimeSpan.FromDays(7));
            PlaneActivity = false;
            PlaneIcon = true;
        }

        private async Task GetTanksListFromApiAsync()
        {
            TankActivity = true;
            string URL = "http://bo7145907-001-site2.ftempurl.com/wtvapi/vehicles/tanks";
            ApiXmlReaderInitial initial = new ApiXmlReaderInitial();
            XmlReader xReader = initial.ApiXmlReader(URL);
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfTanks));
            arrayOfTanks = (ArrayOfTanks)serializer.Deserialize(xReader);
            await BlobCache.UserAccount.InsertObject("cachedArrayOfTanks", arrayOfTanks, TimeSpan.FromDays(7));
            TankActivity = false;
            TankIcon = true;
        }

        private async Task GetHelisListFromApiAsync()
        {
            HeliActivity = true;
            string URL = "http://bo7145907-001-site2.ftempurl.com/wtvapi/vehicles/helis";
            ApiXmlReaderInitial initial = new ApiXmlReaderInitial();
            XmlReader xReader = initial.ApiXmlReader(URL);
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfHelis));
            arrayOfHelis = (ArrayOfHelis)serializer.Deserialize(xReader);
            await BlobCache.UserAccount.InsertObject("cachedArrayOfHelis", arrayOfHelis, TimeSpan.FromDays(7));
            HeliActivity = false;
            HeliIcon = true;
        }

        private async Task GetShipsListFromApiAsync()
        {
            ShipActivity = true;
            string URL = "http://bo7145907-001-site2.ftempurl.com/wtvapi/vehicles/ships";
            ApiXmlReaderInitial initial = new ApiXmlReaderInitial();
            XmlReader xReader = initial.ApiXmlReader(URL);
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfShips));
            arrayOfShips = (ArrayOfShips)serializer.Deserialize(xReader);
            await BlobCache.UserAccount.InsertObject("cachedArrayOfShips", arrayOfShips, TimeSpan.FromDays(7));
            ShipActivity = false;
            ShipIcon = true;
        }


        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
