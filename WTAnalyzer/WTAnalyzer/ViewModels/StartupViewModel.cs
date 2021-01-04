using Akavache;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
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
        ArrayOfPlanes arrayOfPlanes;
        ArrayOfTanks arrayOfTanks;
        ArrayOfHelis arrayOfHelis;
        ArrayOfShips arrayOfShips;

        public StartupViewModel(INavigation navigation)
        {
            Navigation = navigation;
            CheckIfInternetConnected();
        }

        private async void CheckIfInternetConnected()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("No internet", "Please make sure the Internet is available and restart the app", "OK");
                });
                return;
            }
            await CheckIfDBCached();
        }

        //TODO: move to separate file
        #region refactor
        private async Task CheckIfDBCached()
        {
            try
            {
                Registrations.Start("WTAAkavacheCache");
                var arrayOfPlansCached = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
                var arrayOfTanksCached = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfTanks");
                var arrayOfHelisCached = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfHelis");
                var arrayOfShipsCached = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfShips");
            }
            catch (KeyNotFoundException)
            {
                await GetPlanesListFromApiAsync();
                await GetTanksListFromApiAsync();
                await GetHelisListFromApiAsync();
                await GetShipsListFromApiAsync();
            }
            finally
            {
                await Navigation.PushAsync(new TabMenuPage());
            }
        }

        private async Task GetPlanesListFromApiAsync()
        {
            string URL = "http://bo7145907-001-site2.ftempurl.com/wtvapi/vehicles/planes";
            ApiXmlReaderInitial initial = new ApiXmlReaderInitial();
            XmlReader xReader = initial.ApiXmlReader(URL);
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfPlanes));
            arrayOfPlanes = (ArrayOfPlanes)serializer.Deserialize(xReader);
            await BlobCache.UserAccount.InsertObject("cachedArrayOfPlanes", arrayOfPlanes, TimeSpan.FromDays(7));
        }

        private async Task GetTanksListFromApiAsync()
        {
            string URL = "http://bo7145907-001-site2.ftempurl.com/wtvapi/vehicles/tanks";
            ApiXmlReaderInitial initial = new ApiXmlReaderInitial();
            XmlReader xReader = initial.ApiXmlReader(URL);
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfTanks));
            arrayOfTanks = (ArrayOfTanks)serializer.Deserialize(xReader);
            await BlobCache.UserAccount.InsertObject("cachedArrayOfTanks", arrayOfTanks, TimeSpan.FromDays(7));
        }

        private async Task GetHelisListFromApiAsync()
        {
            string URL = "http://bo7145907-001-site2.ftempurl.com/wtvapi/vehicles/helis";
            ApiXmlReaderInitial initial = new ApiXmlReaderInitial();
            XmlReader xReader = initial.ApiXmlReader(URL);
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfHelis));
            arrayOfHelis = (ArrayOfHelis)serializer.Deserialize(xReader);
            await BlobCache.UserAccount.InsertObject("cachedArrayOfHelis", arrayOfHelis, TimeSpan.FromDays(7));
        }

        private async Task GetShipsListFromApiAsync()
        {
            string URL = "http://bo7145907-001-site2.ftempurl.com/wtvapi/vehicles/ships";
            ApiXmlReaderInitial initial = new ApiXmlReaderInitial();
            XmlReader xReader = initial.ApiXmlReader(URL);
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfShips));
            arrayOfShips = (ArrayOfShips)serializer.Deserialize(xReader);
            await BlobCache.UserAccount.InsertObject("cachedArrayOfShips", arrayOfShips, TimeSpan.FromDays(7));
        }
        #endregion
    }
}
