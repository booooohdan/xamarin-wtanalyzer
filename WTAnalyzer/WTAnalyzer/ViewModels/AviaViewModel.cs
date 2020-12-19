using Akavache;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        IEnumerable<Plane> resultByNation;
        string filterLabel;

        private ObservableCollection<DataPoint> lineUsa { get; set; }
        private ObservableCollection<DataPoint> lineGermany { get; set; }
        private ObservableCollection<DataPoint> lineUssr { get; set; }
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

            #region Message ceter subscribe

            MessagingCenter.Subscribe<FilterViewModel, string>(this, "filterNations", (sender, arg) =>
            {
                GetPlaneWithFilter(arg);
                FilterLabel = arg;
            });

            #endregion
        }

        #endregion

        #region Public propertys

        public string FilterLabel
        {
            get => filterLabel;
            set
            {
                filterLabel = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DataPoint> LineUsa
        {
            get => lineUsa;
            set
            {
                lineUsa = value;
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

        public ObservableCollection<DataPoint> LineUssr { get; set; }
        public ObservableCollection<DataPoint> LineBritain { get; set; }
        public ObservableCollection<DataPoint> LineJapan { get; set; }
        public ObservableCollection<DataPoint> LineItaly { get; set; }
        public ObservableCollection<DataPoint> LineFrance { get; set; }
        public ObservableCollection<DataPoint> LineChina { get; set; }
        public ObservableCollection<DataPoint> LineSweden { get; set; }

        #endregion

        public async Task FillListFromCacheAsync() //Loading data from cache
        {
            arrayOfPlanes = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
        }

        public void GetPlaneWithFilter(string filter)
        {
            LineUsa = filter.Contains("USA") ? GetLineDataPoint("USA", "count") : null;
            LineGermany = filter.Contains("Germany") ? GetLineDataPoint("Germany", "count") : null;
        }

        public ObservableCollection<DataPoint> GetLineDataPoint(string nation, string task)
        {
            var planesAll = arrayOfPlanes.PlanesListApi.Where(x => x.Nation == nation).ToList();
            var datas = new ObservableCollection<DataPoint>();

            foreach (double number in BRArray.PlanesBR())
            {
                double? planesCount = null;
                if (task == "count") { planesCount = planesAll.Where(x => x.BR == number).Count(); }
                if (task == "repaircost") { planesCount = planesAll.Where(x => x.BR == number).Max(x => x.RepairCost); }
                if (task == "maxspeed") { planesCount = planesAll.Where(x => x.BR == number).Max(x => x.MaxSpeedAt0); }
                if (task == "maxspeedat5000m") { planesCount = planesAll.Where(x => x.BR == number).Max(x => x.MaxSpeedAt5000); }
                if (task == "climb") { planesCount = planesAll.Where(x => x.BR == number).Max(x => x.Climb); }
                if (task == "turntime") { planesCount = planesAll.Where(x => x.BR == number).Max(x => x.TurnAt0); }
                if (task == "enginepower") { planesCount = planesAll.Where(x => x.BR == number).Max(x => x.EnginePower); }
                if (task == "weight") { planesCount = planesAll.Where(x => x.BR == number).Max(x => x.Weight); }
                if (task == "flutter") { planesCount = planesAll.Where(x => x.BR == number).Max(x => x.Flutter); }
                if (task == "optimalalitude") { planesCount = planesAll.Where(x => x.BR == number).Max(x => x.OptimalAlitude); }
                if (task == "bombload") { planesCount = planesAll.Where(x => x.BR == number).Max(x => x.BombLoad); }
                if (task == "burstmass") { planesCount = planesAll.Where(x => x.BR == number).Max(x => x.BurstMass); }
                if (task == "firstflyyear") { planesCount = planesAll.Where(x => x.BR == number).Max(x => x.FirstFlyYear); }

                datas.Add(new DataPoint(number, planesCount));
            }
            return datas;
        }

        private async void OpenFilterModalPageHandler(object obj) //Open filter page
        {
            if (Navigation.ModalStack.Count == 0)
            {
                await Navigation.PushModalAsync(new FilterPage());
            }
        }
    }
}
