using Akavache;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WTAnalyzer.Models;
using WTAnalyzer.ViewModels;
using WTAnalyzer.XmlHandler;
using Xamarin.Forms;

namespace WTAnalyzer.Helpers
{
    public class VehiclesSearchHandler : SearchHandler
    {
        ObservableCollection<ListViewItem> allVehicleList;
        protected ArrayOfPlanes arrayOfPlanes { get; set; }
        protected ArrayOfTanks arrayOfTanks { get; set; }
        protected ArrayOfHelis arrayOfHelis { get; set; }
        protected ArrayOfShips arrayOfShips { get; set; }

        public VehiclesSearchHandler()
        {
            Task.Run(GetVehicleDataFromCacheAsync).Wait();
        }

        public async Task GetVehicleDataFromCacheAsync()
        {
            arrayOfPlanes = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
            arrayOfTanks = await BlobCache.UserAccount.GetObject<ArrayOfTanks>("cachedArrayOfTanks");
            arrayOfHelis = await BlobCache.UserAccount.GetObject<ArrayOfHelis>("cachedArrayOfHelis");
            arrayOfShips = await BlobCache.UserAccount.GetObject<ArrayOfShips>("cachedArrayOfShips");
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);



            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                allVehicleList = new ObservableCollection<ListViewItem>();

                foreach (var item in arrayOfPlanes.PlanesListApi)
                {
                    allVehicleList.Add(new ListViewItem(item.Nation, item.Class, null, item.Name, null, null, item.BR, item.VehicleId));
                }
                foreach (var item in arrayOfTanks.TanksListApi)
                {
                    allVehicleList.Add(new ListViewItem(item.Nation, item.Class, null, item.Name, null, null, item.BR, item.VehicleId));
                }
                foreach (var item in arrayOfHelis.HelisListApi)
                {
                    allVehicleList.Add(new ListViewItem(item.Nation, item.Class, null, item.Name, null, null, item.BR, item.VehicleId));
                }
                foreach (var item in arrayOfShips.ShipsListApi)
                {
                    allVehicleList.Add(new ListViewItem(item.Nation, item.Class, null, item.Name, null, null, item.BR, item.VehicleId));
                }

                ItemsSource = allVehicleList.Where(x => x.Name.ToLower().Contains(newValue.ToLower())).ToList();
            }

        }

        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            var selectedId = ((ListViewItem)item).Id.ToString();
            MessagingCenter.Send(this, "searchResult", selectedId);
        }
    }
}
