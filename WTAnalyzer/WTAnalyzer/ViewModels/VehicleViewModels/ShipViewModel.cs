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
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels.VehicleViewModels
{
    public class ShipViewModel : BaseVehiclesViewModel
    {
        #region Define variables
        public INavigation Navigation { get; set; }
        public ICommand OpenFilterModalPageCommand { get; }

        #endregion

        public ShipViewModel(INavigation navigation) : base("Ship")
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

                await Navigation.PushModalAsync(new FilterPage("Ship"));
            }
        }

        public List<Ship> FilterVehicleDataDependingFilterPage(string[] filterNations, string[] filterRank, string[] filterRole, string[] filterGameType)
        {
            return arrayOfShips.ShipsListApi.ToList()
                .Where(x => filterNations.Contains(x.Nation)).ToList()
                .Where(x => filterRank.Contains(x.Rank)).ToList()
                .Where(x => filterRole.Contains(x.Class)).ToList()
                .Where(x => filterGameType.Contains(x.Type)).ToList();
        }

        public ObservableCollection<ChartsItem> GetFilteredDataForCharts(string nation, string task)
        {
            var filteredShipList = FilterVehicleDataDependingFilterPage(filterNations, filterRanks, filterRoles, filterGameTypes);
            var dataForCharts = new ObservableCollection<ChartsItem>();

            foreach (double number in BRArray.ShipsBR())
            {
                double? shipsCount = null;
                if (task == AppResources.RepairCost) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.RepairCost); }
                if (task == AppResources.MainCaliberSize) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MainCaliberSize); }
                if (task == AppResources.MainCaliberReload) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MainCaliberReload); }
                if (task == AppResources.TorpedoCount) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.TorpedoItem); }
                if (task == AppResources.TorpedoSpeed) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.TorpedoMaxSpeed); }
                if (task == AppResources.TorpedoTNT) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.TorpedoTNT); }
                if (task == AppResources.MaxSpeedShip) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MaxSpeed); }
                if (task == AppResources.CirculationTime) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Turn360); }
                if (task == AppResources.TowerArmor) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.ArmorTower); }
                if (task == AppResources.HullArmor) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.ArmorHull); }
                if (task == AppResources.CrewCount) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.CrewCount); }
                if (task == AppResources.Displacement) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Displacement); }
                if (task == AppResources.LaunchedYear) { shipsCount = filteredShipList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.FirstLaunchYear); }

                dataForCharts.Add(new ChartsItem(number, shipsCount));
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
            var filteredShipList = FilterVehicleDataDependingFilterPage(filterNations, filterRanks, filterRoles, filterGameTypes);
            var dataForListView = new ObservableCollection<ListViewItem>();

            foreach (var item in filteredShipList)
            {
                if (filterTask == AppResources.RepairCost) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.RepairCost, AppResources.SL, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.MainCaliberSize) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MainCaliberSize, AppResources.Mm, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.MainCaliberReload) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MainCaliberReload, AppResources.S, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.TorpedoCount) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.TorpedoItem, AppResources.Item, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.TorpedoSpeed) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.TorpedoMaxSpeed, AppResources.KmH, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.TorpedoTNT) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.TorpedoTNT, AppResources.Kg, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.MaxSpeedShip) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeed, AppResources.KmH, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.CirculationTime) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Turn360, AppResources.S, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.TowerArmor) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ArmorTower, AppResources.Mm, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.HullArmor) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ArmorHull, AppResources.Mm, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.CrewCount) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.CrewCount, AppResources.Item, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.Displacement) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Displacement, AppResources.T, item.BR, item.VehicleId)); }
                if (filterTask == AppResources.LaunchedYear) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.FirstLaunchYear, AppResources.Y, item.BR, item.VehicleId)); }
            }

            sortedDataForListView = filterOrder == AppResources.Ascending
                ? dataForListView.OrderBy(x => x.Value).ToObservableCollection()
                : dataForListView.OrderByDescending(x => x.Value).ToObservableCollection();

            ListViewVehicleProp = sortedDataForListView;
        }
    }
}
