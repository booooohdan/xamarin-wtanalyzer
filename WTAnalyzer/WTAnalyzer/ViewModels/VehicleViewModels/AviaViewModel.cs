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
    public class AviaViewModel : BaseVehiclesViewModel
    {
        #region Define variables
        public INavigation Navigation { get; set; }
        public ICommand OpenFilterModalPageCommand { get; }

        int adsCount = 0;

        #endregion

        public AviaViewModel(INavigation navigation) : base("Avia")
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
                     async (sender, arg) =>
                     {
                         await GetDataFromFilterPageAsync();
                         SetDataToChartsView();
                         SetDataToListView();
                     });

                await Navigation.PushModalAsync(new FilterPage("Avia"));
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

        public List<Plane> FilterVehicleDataDependingFilterPage(string[] filterNations, string[] filterRank, string[] filterRole, string[] filterGameType)
        {
            return arrayOfPlanes.PlanesListApi.ToList()
                .Where(x => filterNations.Contains(x.Nation)).ToList()
                .Where(x => filterRank.Contains(x.Rank)).ToList()
                .Where(x => filterRole.Contains(x.Class)).ToList()
                .Where(x => filterGameType.Contains(x.Type)).ToList();
        }

        public ObservableCollection<ChartsItem> GetFilteredDataForCharts(string nation, string task)
        {
            var filteredPlaneList = FilterVehicleDataDependingFilterPage(filterNations, filterRanks, filterRoles, filterGameTypes);
            var dataForCharts = new ObservableCollection<ChartsItem>();

            foreach (double number in BRArray.PlanesBR())
            {
                double? planesCount = null;
                if (task == AppResources.RepairCost) { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.RepairCost); }
                if (task == AppResources.MaxSpeed) { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MaxSpeedAt0); }
                if (task == AppResources.MaxSpeedAt5000M) { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MaxSpeedAt5000); }
                if (task == AppResources.Climb) { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Climb); }
                if (task == AppResources.TurnTime) { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.TurnAt0); }
                if (task == AppResources.EnginePower) { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.EnginePower); }
                if (task == AppResources.Weight) { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Weight); }
                if (task == AppResources.Flutter) { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Flutter); }
                if (task == AppResources.OptimalAlitude) { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.OptimalAlitude); }
                if (task == AppResources.BombLoad) { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.BombLoad); }
                if (task == AppResources.BurstMass) { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.BurstMass); }
                if (task == AppResources.FirstFlyYear) { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.FirstFlyYear); }

                dataForCharts.Add(new ChartsItem(number, planesCount));
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
            var filteredPlaneList = FilterVehicleDataDependingFilterPage(filterNations, filterRanks, filterRoles, filterGameTypes);
            var dataForListView = new ObservableCollection<ListViewItem>();

            foreach (var item in filteredPlaneList)
            {
                if (filterTask == AppResources.RepairCost) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.RepairCost, AppResources.SL, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.MaxSpeed) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeedAt0, AppResources.KmH, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.MaxSpeedAt5000M) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeedAt5000, AppResources.KmH, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.Climb) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Climb, AppResources.S, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.TurnTime) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.TurnAt0, AppResources.S, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.EnginePower) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.EnginePower, "", item.BR, item.VehicleId)); }
                if (filterTask == AppResources.Weight) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Weight, AppResources.Kg, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.Flutter) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Flutter, AppResources.KmH, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.OptimalAlitude) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.OptimalAlitude, AppResources.M, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.BombLoad) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.BombLoad, AppResources.Kg, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.BurstMass) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.BurstMass, AppResources.KgS, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.FirstFlyYear) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.FirstFlyYear, AppResources.Y, item.BR, item.VehicleId)); }
            }

            sortedDataForListView = filterOrder == AppResources.Ascending
                ? dataForListView.OrderBy(x => x.Value).ToObservableCollection()
                : dataForListView.OrderByDescending(x => x.Value).ToObservableCollection();

            ListViewVehicleProp = sortedDataForListView;
        }
    }
}
