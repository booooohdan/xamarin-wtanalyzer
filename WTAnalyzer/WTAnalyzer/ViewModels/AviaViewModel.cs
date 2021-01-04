using Akavache;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WTAnalyzer.DataCollections;
using WTAnalyzer.Helpers;
using WTAnalyzer.Models;
using WTAnalyzer.ViewModels.BaseViewModels;
using WTAnalyzer.Views.ServicePages;
using WTAnalyzer.XmlHandler;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    public class AviaViewModel : BaseViewModel
    {
        #region Define variables

        public INavigation Navigation { get; set; }
        public ICommand OpenFilterModalPageCommand { get; }
        ArrayOfPlanes arrayOfPlanes;
        string filterLabelDesc;
        string filterTask;
        string[] filterNations;
        string[] filterRank;
        string[] filterClass;
        string filterOrder;
        string filterClose;

        private ObservableCollection<ListViewItem> listViewPlaneProp { get; set; }
        private ObservableCollection<ChartsItem> lineUSA { get; set; }
        private ObservableCollection<ChartsItem> lineGermany { get; set; }
        private ObservableCollection<ChartsItem> lineUSSR { get; set; }
        private ObservableCollection<ChartsItem> lineBritain { get; set; }
        private ObservableCollection<ChartsItem> lineJapan { get; set; }
        private ObservableCollection<ChartsItem> lineItaly { get; set; }
        private ObservableCollection<ChartsItem> lineFrance { get; set; }
        private ObservableCollection<ChartsItem> lineChina { get; set; }
        private ObservableCollection<ChartsItem> lineSweden { get; set; }

        #endregion

        #region Ctor

        public AviaViewModel(INavigation navigation)
        {
            Debug.WriteLine("AviaViewModel constructor");
            Navigation = navigation;
            OpenFilterModalPageCommand = new Command(OpenFilterModalPageHandler);
            Task.Run(FillListFromCacheAsync).Wait(); //Load data from cache
        }

        #endregion

        #region Public propertys

        public string FilterTask
        {
            get => filterTask;
            set
            {
                filterTask = value;
                OnPropertyChanged();
            }
        }

        public string FilterLabelDesc
        {
            get => filterLabelDesc;
            set
            {
                filterLabelDesc = value;
                OnPropertyChanged();
            }
        }

        private string FilterClose
        {
            get => filterClose;
            set
            {
                filterClose = value;
                OnPropertyChanged();
                GetDataFromFilterPageAsync();
            }
        }

        public ObservableCollection<ListViewItem> ListViewPlaneProp
        {
            get => listViewPlaneProp;
            set
            {
                listViewPlaneProp = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> LineUSA
        {
            get => lineUSA;
            set
            {
                lineUSA = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChartsItem> LineGermany
        {
            get => lineGermany;
            set
            {
                lineGermany = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> LineUSSR
        {
            get => lineUSSR;
            set
            {
                lineUSSR = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChartsItem> LineBritain
        {
            get => lineBritain;
            set
            {
                lineBritain = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChartsItem> LineJapan
        {
            get => lineJapan;
            set
            {
                lineJapan = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChartsItem> LineItaly
        {
            get => lineItaly;
            set
            {
                lineItaly = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChartsItem> LineFrance
        {
            get => lineFrance;
            set
            {
                lineFrance = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChartsItem> LineChina
        {
            get => lineChina;
            set
            {
                lineChina = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChartsItem> LineSweden
        {
            get => lineSweden;
            set
            {
                lineSweden = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public async Task FillListFromCacheAsync() //Loading data from cache
        {
            Debug.WriteLine("FillListFromCacheAsync()");

            arrayOfPlanes = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
        }

        private void SetDataToFilterLabel()
        {
            Debug.WriteLine("SetDataToFilterLabel()");

            string nations = filterNations.Count() == 9 ? "All" : "Custom";
        }

        public void SetDataToListViewProperties()
        {
            Debug.WriteLine("SetDataToListViewProperties()");

            var sortedDataForListView = new ObservableCollection<ListViewItem>();
            var filteredPlaneList = GetFilteredList(filterNations, filterRank, filterClass);
            var dataForListView = new ObservableCollection<ListViewItem>();

            foreach (var item in filteredPlaneList)
            {
                //if (filterTask == "Count") { dataForListView.Add(new ListViewItem(item.Nation, item.Name, item.RepairCost)); }
                if (filterTask == "Repair Cost") { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.RepairCost, "s.l", item.BR)); }
                if (filterTask == "Max Speed") { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeedAt0, "km/h", item.BR)); }
                if (filterTask == "Max Speed at 5000 m") { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeedAt5000, "km/h", item.BR)); }
                if (filterTask == "Climb") { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Climb, "s", item.BR)); }
                if (filterTask == "Turn Time") { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.TurnAt0, "s", item.BR)); }
                if (filterTask == "Engine Power") { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.EnginePower, "", item.BR)); }
                if (filterTask == "Weight") { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Weight, "kg", item.BR)); }
                if (filterTask == "Flutter") { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Flutter, "km/h", item.BR)); }
                if (filterTask == "Optimal Alitude") { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.OptimalAlitude, "m", item.BR)); }
                if (filterTask == "Bomb Load") { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.BombLoad, "kg", item.BR)); }
                if (filterTask == "Burst Mass") { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.BurstMass, "kg/s", item.BR)); }
                if (filterTask == "First fly Year") { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.FirstFlyYear, "y", item.BR)); }
            }

            sortedDataForListView = filterOrder == "Ascending"
                ? dataForListView.OrderBy(x => x.Value).ToObservableCollection()
                : dataForListView.OrderByDescending(x => x.Value).ToObservableCollection();
           
            ListViewPlaneProp = sortedDataForListView;
        }

        public void SetDataToChartsProperties()
        {
            Debug.WriteLine("SetCollectionOfVehicleToDataPoint()");

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

        public List<Plane> GetFilteredList(string[] filterNations, string[] filterRank, string[] filterClass)
        {
            Debug.WriteLine("GetFilteredList()");

            return arrayOfPlanes.PlanesListApi.ToList()
                .Where(x => filterNations.Contains(x.Nation)).ToList()
                .Where(x => filterRank.Contains(x.Rank)).ToList()
                .Where(x => filterClass.Contains(x.Class)).ToList();

        }

        public ObservableCollection<ChartsItem> GetFilteredDataForCharts(string nation, string task)
        {
            Debug.WriteLine("GetLineDataPoint()");

            var filteredPlaneList = GetFilteredList(filterNations, filterRank, filterClass);
            var dataForCharts = new ObservableCollection<ChartsItem>();

            foreach (double number in BRArray.PlanesBR())
            {
                double? planesCount = null;
                if (task == "Count") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Count(); }
                if (task == "Repair Cost") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.RepairCost); }
                if (task == "Max Speed") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MaxSpeedAt0); }
                if (task == "Max Speed at 5000 m") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MaxSpeedAt5000); }
                if (task == "Climb") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Climb); }
                if (task == "Turn Time") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.TurnAt0); }
                if (task == "Engine Power") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.EnginePower); }
                if (task == "Weight") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Weight); }
                if (task == "Flutter") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Flutter); }
                if (task == "Optimal Alitude") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.OptimalAlitude); }
                if (task == "Bomb Load") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.BombLoad); }
                if (task == "Burst Mass") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.BurstMass); }
                if (task == "First fly Year") { planesCount = filteredPlaneList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.FirstFlyYear); }

                dataForCharts.Add(new ChartsItem(number, planesCount));
            }
            return dataForCharts;
        }

        private async void OpenFilterModalPageHandler(object obj) //Open filter page
        {
            Debug.WriteLine("OpenFilterModalPageHandler");

            if (Navigation.ModalStack.Count == 0)
            {
                //MessagingCenter.Subscribe<FilterViewModel, string>(this, "filterTask",(sender, arg) => { FilterTask = arg; });
                MessagingCenter.Subscribe<FilterViewModel, string>(this, "filterNations",
                     (sender, arg) => { filterNations = arg.Split('|'); });
                MessagingCenter.Subscribe<FilterViewModel, string>(this, "filterRank",
                     (sender, arg) => { filterRank = arg.Split('|'); });
                MessagingCenter.Subscribe<FilterViewModel, string>(this, "filterClass",
                     (sender, arg) => { filterClass = arg.Split('|'); });
                MessagingCenter.Subscribe<FilterViewModel, string>(this, "filterOrder",
                     (sender, arg) => { filterOrder = arg; });
                MessagingCenter.Subscribe<FilterViewModel, string>(this, "filterClose",
                     (sender, arg) => { FilterClose = arg; });

                await Navigation.PushModalAsync(new FilterPage());
            }
        }

        private async Task GetDataFromFilterPageAsync()
        {
            Debug.WriteLine("GetDataFromFilterPage()");

            FilterTask = await BlobCache.UserAccount.GetObject<string>("cachedSelectedTask");
            //MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterTask");
            MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterNations");
            MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterRank");
            MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterClass");
            MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterOrder");
            MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterClose");

            SetDataToChartsProperties();
            SetDataToListViewProperties();
            SetDataToFilterLabel();

            Debug.WriteLine(filterTask);
            Debug.WriteLine(filterNations);
            Debug.WriteLine(filterRank);
            Debug.WriteLine(filterClass);
            Debug.WriteLine(filterOrder);
            Debug.WriteLine(filterClose);
        }
    }
}
