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
    public class TankViewModel : BaseVehiclesViewModel
    {
        #region Define variables
        public INavigation Navigation { get; set; }
        public ICommand OpenFilterModalPageCommand { get; }
        int adsCount = 0;
        int start_count = Preferences.Get("start_count", 0);
        public bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }

        #endregion

        public TankViewModel(INavigation navigation) : base("Tank")
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
            if (FirstRun)
            {
                AppTrackingShowAsync();
                FirstRun = false;
            }
        }

        private async Task AppTrackingShowAsync()
        {
            if (Device.RuntimePlatform == Device.iOS && DeviceInfo.Version.Major >= 14)
            {
                var status = await DependencyService.Get<IAppTracking>().IsAuthorized();
                CrossMTAdmob.Current.UserPersonalizedAds = await DependencyService.Get<IAppTracking>().RequestAuthorizationAsync();
            }
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

                await Navigation.PushModalAsync(new FilterPage("Tank"));
            }

            ShowReview();
            ShowIntersitialAds();
        }

        private void ShowReview() //Review request
        {
            start_count++;
            Preferences.Set("start_count", start_count);
            if (start_count == 5 || start_count == 15)
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        CrossStoreReview.Current.OpenStoreReviewPage("com.wtwave.wtinsider");
                        break;
                    case Device.iOS:
                        //CrossStoreReview.Current.OpenStoreReviewPage("1542964380");
                        CrossStoreReview.Current.OpenStoreReviewPage("");
                        break;
                }
            }
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


        public List<Tank> FilterVehicleDataDependingFilterPage(string[] filterNations, string[] filterRank, string[] filterRole, string[] filterGameType)
        {
            return arrayOfTanks.TanksListApi.ToList()
                .Where(x => filterNations.Contains(x.Nation)).ToList()
                .Where(x => filterRank.Contains(x.Rank)).ToList()
                .Where(x => filterRole.Contains(x.Class)).ToList()
                .Where(x => filterGameType.Contains(x.Type)).ToList();
        }

        public ObservableCollection<ChartsItem> GetFilteredDataForCharts(string nation, string task)
        {
            var filteredTankList = FilterVehicleDataDependingFilterPage(filterNations, filterRanks, filterRoles, filterGameTypes);
            var dataForCharts = new ObservableCollection<ChartsItem>();

            foreach (double number in BRArray.TanksBR())
            {
                double? tanksCount = null;
                if (task == AppResources.RepairCost) { tanksCount = filteredTankList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.RepairCost); }
                if (task == AppResources.SpeedAtTerrain) { tanksCount = filteredTankList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MaxSpeedAtTerrain); }
                if (task == AppResources.SpeedAtRoad) { tanksCount = filteredTankList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MaxSpeedAtRoad); }
                if (task == AppResources.ReverseSpeed) { tanksCount = filteredTankList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MaxReverseSpeed); }
                if (task == AppResources.EnginePower) { tanksCount = filteredTankList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.EnginePower); }
                if (task == AppResources.Weight) { tanksCount = filteredTankList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Weight); }
                if (task == AppResources.Penetration) { tanksCount = filteredTankList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Penetration); }
                if (task == AppResources.ReloadTime) { tanksCount = filteredTankList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.ReloadTime); }
                if (task == AppResources.TurretArmor) { tanksCount = filteredTankList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.ReducedArmorFrontTurret); }
                if (task == AppResources.UpperArmorPlate) { tanksCount = filteredTankList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.ReducedArmorTopSheet); }
                if (task == AppResources.LowerArmorPlate) { tanksCount = filteredTankList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.ReducedArmorBottomSheet); }
                if (task == AppResources.FirstRideYear) { tanksCount = filteredTankList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.FirstRideYear); }

                dataForCharts.Add(new ChartsItem(number, tanksCount));
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
            var filteredTankList = FilterVehicleDataDependingFilterPage(filterNations, filterRanks, filterRoles, filterGameTypes);
            var dataForListView = new ObservableCollection<ListViewItem>();

            foreach (var item in filteredTankList)
            {
                if (filterTask == AppResources.RepairCost) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.RepairCost, AppResources.SL, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.SpeedAtTerrain) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeedAtTerrain, AppResources.KmH, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.SpeedAtRoad) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeedAtRoad, AppResources.KmH, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.ReverseSpeed) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxReverseSpeed, AppResources.KmH, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.EnginePower) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.EnginePower, AppResources.HP, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.Weight) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Weight, AppResources.T, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.Penetration) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Penetration, AppResources.Mm, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.ReloadTime) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ReloadTime, AppResources.S, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.TurretArmor) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ReducedArmorFrontTurret, AppResources.Mm, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.UpperArmorPlate) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ReducedArmorTopSheet, AppResources.Mm, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.LowerArmorPlate) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ReducedArmorBottomSheet, AppResources.Mm, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.FirstRideYear) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.FirstRideYear, AppResources.Y, item.BR, item.VehicleId)); }
            }

            sortedDataForListView = filterOrder == AppResources.Ascending
                ? dataForListView.OrderBy(x => x.Value).ToObservableCollection()
                : dataForListView.OrderByDescending(x => x.Value).ToObservableCollection();

            ListViewVehicleProp = sortedDataForListView;
        }
    }
}
