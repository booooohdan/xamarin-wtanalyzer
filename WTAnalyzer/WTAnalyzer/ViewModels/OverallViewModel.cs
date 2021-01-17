using Akavache;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WTAnalyzer.Cache;
using WTAnalyzer.DataCollections;
using WTAnalyzer.Models;
using WTAnalyzer.Resx;
using WTAnalyzer.ViewModels.BaseViewModels;
using WTAnalyzer.XmlHandler;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    public class OverallViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        protected ArrayOfPlanes arrayOfPlanes { get; set; }
        protected ArrayOfTanks arrayOfTanks { get; set; }
        protected ArrayOfHelis arrayOfHelis { get; set; }
        protected ArrayOfShips arrayOfShips { get; set; }
        public ObservableCollection<ChartsItem> DoughnutSeriesData { get; set; }

        public OverallViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(GetVehicleDataFromCacheAsync).Wait();
            DoughnutSeriesData = VehicleCountPerNation();
        }

        public async Task GetVehicleDataFromCacheAsync()
        {
            arrayOfPlanes = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
            arrayOfTanks = await BlobCache.UserAccount.GetObject<ArrayOfTanks>("cachedArrayOfTanks");
            arrayOfHelis = await BlobCache.UserAccount.GetObject<ArrayOfHelis>("cachedArrayOfHelis");
            arrayOfShips = await BlobCache.UserAccount.GetObject<ArrayOfShips>("cachedArrayOfShips");
        }

        public ObservableCollection<ChartsItem> VehicleCountPerNation()
        {
            var planes = arrayOfPlanes.PlanesListApi.ToList();
            var tanks = arrayOfTanks.TanksListApi.ToList();
            var helis = arrayOfHelis.HelisListApi.ToList();
            var ships = arrayOfShips.ShipsListApi.ToList();
                    
            var result = planes.Select(p => p.Nation)
                .Concat(tanks.Select(t => t.Nation))
                .Concat(helis.Select(h => h.Nation))
                .Concat(ships.Select(s => s.Nation));

            var usa = result.Where(x => x == "USA").Count();
            var germ = result.Where(x => x == "Germany").Count();
            var ussr = result.Where(x => x == "USSR").Count();
            var gb = result.Where(x => x == "Britain").Count();
            var jpn = result.Where(x => x == "Japan").Count();
            var it = result.Where(x => x == "Italy").Count();
            var fr = result.Where(x => x == "France").Count();
            var ch = result.Where(x => x == "China").Count();
            var sw = result.Where(x => x == "Sweden").Count();

            return new ObservableCollection<ChartsItem>
            {
                new ChartsItem(AppResources.USA, usa),
                new ChartsItem(AppResources.Germany, germ),
                new ChartsItem(AppResources.USSR, ussr),
                new ChartsItem(AppResources.Britain, gb),
                new ChartsItem(AppResources.Japan, jpn),
                new ChartsItem(AppResources.Italy, it),
                new ChartsItem(AppResources.France, fr),
                new ChartsItem(AppResources.China, ch),
                new ChartsItem(AppResources.Sweden, sw)
            };
        }

    }
}
