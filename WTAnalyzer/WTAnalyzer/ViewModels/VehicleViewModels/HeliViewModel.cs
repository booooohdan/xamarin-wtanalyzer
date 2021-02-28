using MarcTron.Plugin;
using Plugin.StoreReview;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WTAnalyzer.DataCollections;
using WTAnalyzer.Helpers;
using WTAnalyzer.Models;
using WTAnalyzer.Resx;
using WTAnalyzer.ViewModels.BaseViewModels;
using WTAnalyzer.ViewModels.ServiceViewModels;
using WTAnalyzer.Views.ServicePages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels.VehicleViewModels
{
    public class HeliViewModel : BaseVehiclesViewModel
    {
        #region Define variables
        public INavigation Navigation { get; set; }
        public ICommand OpenFilterModalPageCommand { get; }
        int adsCount = 0;

        #endregion

        public HeliViewModel(INavigation navigation) : base("Heli")
        {
            Navigation = navigation;
            OpenFilterModalPageCommand = new Command(OpenFilterModalPageHandler);
            Busy = true;
            Task.Run(() =>
            {
                SetDataToChartsView();
                SetDataToListView();
                Busy = false;
            });
        }

        private async void OpenFilterModalPageHandler(object obj)
        {
            if (Navigation.ModalStack.Count == 0)
            {
                MessagingCenter.Subscribe<FilterViewModel, string>(this, "filterClose",
                     async (sender, arg) => {
                         await GetDataFromFilterPageAsync();
                         SetDataToChartsView();
                         SetDataToListView();
                     });

                await Navigation.PushModalAsync(new FilterPage("Heli"));
            }

            ShowIntersitialAds();
        }

        private void ShowIntersitialAds()
        {
            adsCount++;
            AdmobIntersitials.LoadIntersitialExplorer();

            if (adsCount == 2 | adsCount == 5)
            {
                CrossMTAdmob.Current.ShowInterstitial();
                AdmobIntersitials.LoadIntersitialExplorer();
            }
            if (adsCount > 7)
            {
                CrossMTAdmob.Current.ShowInterstitial();
                AdmobIntersitials.LoadIntersitialExplorer();
            }
        }

        public List<Heli> FilterVehicleDataDependingFilterPage(string[] filterNations, string[] filterRank, string[] filterRole, string[] filterGameType)
        {
            return arrayOfHelis.HelisListApi.ToList()
                .Where(x => filterNations.Contains(x.Nation)).ToList()
                .Where(x => filterRank.Contains(x.Rank)).ToList()
                .Where(x => filterRole.Contains(x.Class)).ToList()
                .Where(x => filterGameType.Contains(x.Type)).ToList();
        }

        public ObservableCollection<ChartsItem> GetFilteredDataForCharts(string nation, string task)
        {
            var filteredHeliList = FilterVehicleDataDependingFilterPage(filterNations, filterRanks, filterRoles, filterGameTypes);
            var dataForCharts = new ObservableCollection<ChartsItem>();

            foreach (double number in BRArray.HelisBR())
            {
                double? helisCount = null;
                if (task == AppResources.RepairCost) { helisCount = filteredHeliList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.RepairCost); }
                if (task == AppResources.SpeedAtSeaLevel) { helisCount = filteredHeliList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MaxSpeed); }
                if (task == AppResources.ClimbTime01000M) { helisCount = filteredHeliList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.ClimbTo1000); }
                if (task == AppResources.Weight) { helisCount = filteredHeliList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Weight); }
                if (task == AppResources.ATGMCount) { helisCount = filteredHeliList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.AGMCount); }
                if (task == AppResources.ATGMRange) { helisCount = filteredHeliList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.AGMRange); }
                if (task == AppResources.UnguidedMissile) { helisCount = filteredHeliList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.ASMCount); }
                if (task == AppResources.FirstRideYear) { helisCount = filteredHeliList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.FirstFlyYear); }

                dataForCharts.Add(new ChartsItem(number, helisCount));
            }
            return dataForCharts;
        }

        protected void SetDataToChartsView()
        {
            LineUSA = !filterNations.Contains("USA") ? null : GetFilteredDataForCharts("USA", filterTask);
            LineGermany = !filterNations.Contains("Germany") ? null : GetFilteredDataForCharts("Germany", filterTask);
            LineUSSR = !filterNations.Contains("USSR") ? null : GetFilteredDataForCharts("USSR", filterTask);
            LineBritain = !filterNations.Contains("Britain") ? null : GetFilteredDataForCharts("Britain", filterTask);
            LineJapan = !filterNations.Contains("Japan") ? null : GetFilteredDataForCharts("Japan", filterTask);
            LineItaly = !filterNations.Contains("Italy") ? null : GetFilteredDataForCharts("Italy", filterTask);
            LineFrance = !filterNations.Contains("France") ? null : GetFilteredDataForCharts("France", filterTask);
            LineChina = !filterNations.Contains("China") ? null : GetFilteredDataForCharts("China", filterTask);
            LineSweden = !filterNations.Contains("Sweden") ? null : GetFilteredDataForCharts("Sweden", filterTask);

        }

        public void SetDataToListView()
        {
            var sortedDataForListView = new ObservableCollection<ListViewItem>();
            var filteredHeliList = FilterVehicleDataDependingFilterPage(filterNations, filterRanks, filterRoles, filterGameTypes);
            var dataForListView = new ObservableCollection<ListViewItem>();

            foreach (var item in filteredHeliList)
            {
                if (filterTask == AppResources.RepairCost) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.RepairCost, AppResources.SL, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.SpeedAtSeaLevel) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeed, AppResources.KmH, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.ClimbTime01000M) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ClimbTo1000, AppResources.S, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.Weight) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Weight, AppResources.Kg, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.ATGMCount) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.AGMCount, AppResources.Item, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.ATGMRange) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.AGMRange, AppResources.M, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.UnguidedMissile) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ASMCount, AppResources.Item, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.FirstRideYear) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.FirstFlyYear, AppResources.Y, item.BR, item.VehicleId)); }
            }

            sortedDataForListView = filterOrder == AppResources.Ascending
                ? dataForListView.OrderBy(x => x.Value).ToObservableCollection()
                : dataForListView.OrderByDescending(x => x.Value).ToObservableCollection();

            ListViewVehicleProp = sortedDataForListView;
        }
    }
}
