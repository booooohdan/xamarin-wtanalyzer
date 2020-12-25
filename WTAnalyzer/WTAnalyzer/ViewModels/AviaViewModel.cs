using Akavache;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WTAnalyzer.Helpers;
using WTAnalyzer.Models;
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
        string filterTask;
        string[] filterNations;
        string[] filterRank;
        string[] filterClass;
        string filterOrder;
        string filterClose;

        private ObservableCollection<DataPoint> lineUSA { get; set; }
        private ObservableCollection<DataPoint> lineGermany { get; set; }
        private ObservableCollection<DataPoint> lineUSSR { get; set; }
        private ObservableCollection<DataPoint> lineBritain { get; set; }
        private ObservableCollection<DataPoint> lineJapan { get; set; }
        private ObservableCollection<DataPoint> lineItaly { get; set; }
        private ObservableCollection<DataPoint> lineFrance { get; set; }
        private ObservableCollection<DataPoint> lineChina { get; set; }
        private ObservableCollection<DataPoint> lineSweden { get; set; }

        #endregion

        #region Ctor

        public AviaViewModel(INavigation navigation)
        {
            Navigation = navigation;
            OpenFilterModalPageCommand = new Command(OpenFilterModalPageHandler);
            Task.Run(FillListFromCacheAsync).Wait(); //Load data from cache

            #region Default data from constructor

            //TODO: Implement default data

            #endregion

            Debug.WriteLine("AviaViewModel constructor");
        }

        #endregion

        #region Public propertys

        private string FilterClose
        {
            get => filterClose;
            set
            {
                filterClose = value;
                OnPropertyChanged();
                GetDataFromFilterPage();
            }
        }
        public ObservableCollection<DataPoint> LineUSA
        {
            get => lineUSA;
            set
            {
                lineUSA = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DataPoint> LineGermany
        {
            get => lineGermany;
            set
            {
                lineGermany = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DataPoint> LineUSSR
        {
            get => lineUSSR;
            set
            {
                lineUSSR = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DataPoint> LineBritain
        {
            get => lineBritain;
            set
            {
                lineBritain = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DataPoint> LineJapan
        {
            get => lineJapan;
            set
            {
                lineJapan = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DataPoint> LineItaly
        {
            get => lineItaly;
            set
            {
                lineItaly = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DataPoint> LineFrance
        {
            get => lineFrance;
            set
            {
                lineFrance = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DataPoint> LineChina
        {
            get => lineChina;
            set
            {
                lineChina = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DataPoint> LineSweden
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

        public ObservableCollection<DataPoint> GetLineDataPoint(string nation, string task)
        {
            Debug.WriteLine("GetLineDataPoint()");

            var planeList = GetFilteredList(filterNations, filterRank, filterClass);
            var datas = new ObservableCollection<DataPoint>();

            foreach (double number in BRArray.PlanesBR())
            {
                double? planesCount = null;
                if (task == "Count") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Count(); }
                if (task == "Repair Cost") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.RepairCost); }
                if (task == "Max Speed") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MaxSpeedAt0); }
                if (task == "Max Speed at 5000 m") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.MaxSpeedAt5000); }
                if (task == "Climb") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Climb); }
                if (task == "Turn Time") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.TurnAt0); }
                if (task == "Engine Power") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.EnginePower); }
                if (task == "Weight") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Weight); }
                if (task == "Flutter") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.Flutter); }
                if (task == "Optimal Alitude") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.OptimalAlitude); }
                if (task == "Bomb Load") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.BombLoad); }
                if (task == "Burst Mass") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.BurstMass); }
                if (task == "First fly Year") { planesCount = planeList.Where(x => x.Nation == nation & x.BR == number).Max(x => x.FirstFlyYear); }

                datas.Add(new DataPoint(number, planesCount));
            }
            return datas;
        }

        public List<Plane> GetFilteredList(string[] filterNations, string[] filterRank, string[] filterClass)
        {
            Debug.WriteLine("GetFilteredList()");

            var planesAll = arrayOfPlanes.PlanesListApi.ToList();
            var planesByNation = planesAll.Where(x => filterNations.Contains(x.Nation)).ToList();
            var planesByRank = planesByNation.Where(x => filterRank.Contains(x.Rank)).ToList();
            var planesByType = planesByRank.Where(x => filterClass.Contains(x.Class)).ToList();

            return planesByType;
        }

        public void SetCollectionOfVehicleToDataPoint()
        {
            Debug.WriteLine("SetCollectionOfVehicleToDataPoint()");

            LineUSA = filterNations.Contains("USA") ? GetLineDataPoint("USA", filterTask) : null;
            LineGermany = filterNations.Contains("Germany") ? GetLineDataPoint("Germany", filterTask) : null;
            LineUSSR = filterNations.Contains("USSR") ? GetLineDataPoint("USSR", filterTask) : null;
            LineBritain = filterNations.Contains("Britain") ? GetLineDataPoint("Britain", filterTask) : null;
            LineJapan = filterNations.Contains("Japan") ? GetLineDataPoint("Japan", filterTask) : null;
            LineItaly = filterNations.Contains("Italy") ? GetLineDataPoint("Italy", filterTask) : null;
            LineFrance = filterNations.Contains("France") ? GetLineDataPoint("France", filterTask) : null;
            LineChina = filterNations.Contains("China") ? GetLineDataPoint("China", filterTask) : null;
            LineSweden = filterNations.Contains("Sweden") ? GetLineDataPoint("Sweden", filterTask) : null;

        }

        private async void OpenFilterModalPageHandler(object obj) //Open filter page
        {
            Debug.WriteLine("OpenFilterModalPageHandler");

            if (Navigation.ModalStack.Count == 0)
            {

                #region Message center subscribe

                MessagingCenter.Subscribe<FilterViewModel, string>(this, "filterTask",
                     (sender, arg) => { filterTask = arg; });
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
                #endregion

                await Navigation.PushModalAsync(new FilterPage());
            }
        }

        private void GetDataFromFilterPage()
        {
            Debug.WriteLine("GetDataFromFilterPage()");

            MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterTask");
            MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterNations");
            MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterRank");
            MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterClass");
            MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterOrder");
            MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterClose");

            SetCollectionOfVehicleToDataPoint();

            Debug.WriteLine(filterTask);
            Debug.WriteLine(filterNations);
            Debug.WriteLine(filterRank);
            Debug.WriteLine(filterClass);
            Debug.WriteLine(filterOrder);
            Debug.WriteLine(filterClose);
        }
    }
}
