
using Akavache;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using WTAnalyzer.Models;
using WTAnalyzer.XmlHandler;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels.BaseViewModels
{
    public class BaseVehiclesViewModel : BaseViewModel
    {
        #region Define variables
        protected string filterTask { get; set; }
        protected string[] filterNations { get; set; }
        protected string[] filterRanks { get; set; }
        protected string[] filterRoles { get; set; }
        protected string[] filterGameTypes { get; set; }
        protected string filterOrder { get; set; }

        protected ArrayOfPlanes arrayOfPlanes { get; set; }
        protected ArrayOfTanks arrayOfTanks { get; set; }
        protected ArrayOfHelis arrayOfHelis { get; set; }
        protected ArrayOfShips arrayOfShips { get; set; }

        private string vmType;
        private string cachedTask;
        private string cachedNation;
        private string cachedRank;
        private string cachedRole;
        private string cachedGameType;
        private string cachedOrder;

        #endregion

        #region View Properties
        private ObservableCollection<ListViewItem> listViewVehicleProp { get; set; }
        private ObservableCollection<ChartsItem> lineUSA { get; set; }
        private ObservableCollection<ChartsItem> lineGermany { get; set; }
        private ObservableCollection<ChartsItem> lineUSSR { get; set; }
        private ObservableCollection<ChartsItem> lineBritain { get; set; }
        private ObservableCollection<ChartsItem> lineJapan { get; set; }
        private ObservableCollection<ChartsItem> lineItaly { get; set; }
        private ObservableCollection<ChartsItem> lineFrance { get; set; }
        private ObservableCollection<ChartsItem> lineChina { get; set; }
        private ObservableCollection<ChartsItem> lineSweden { get; set; }

        public string FilterTask
        {
            get => filterTask;
            set
            {
                filterTask = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ListViewItem> ListViewVehicleProp
        {
            get => listViewVehicleProp;
            set
            {
                listViewVehicleProp = value;
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

        public BaseVehiclesViewModel(string vmType)
        {
            this.vmType = vmType;
            SetParametersDependingCallingViewModel();
            Task.Run(GetVehicleDataFromCacheAsync).Wait();
            Task.Run(GetDataFromFilterPageAsync).Wait();
        }
        private void SetParametersDependingCallingViewModel()
        {
            if (vmType == "Avia")
            {
                cachedTask = "cachedAviaTask";
                cachedNation = "cachedAviaNations";
                cachedRank = "cachedAviaRanks";
                cachedRole = "cachedAviaRoles";
                cachedGameType = "cachedAviaGameTypes";
                cachedOrder = "cachedAviaOrder";
            }

            if (vmType == "Tank")
            {
                cachedTask = "cachedTankTask";
                cachedNation = "cachedTankNations";
                cachedRank = "cachedTankRanks";
                cachedRole = "cachedTankRoles";
                cachedGameType = "cachedTankGameTypes";
                cachedOrder = "cachedTankOrder";
            }

            if (vmType == "Heli")
            {
                cachedTask = "cachedHeliTask";
                cachedNation = "cachedHeliNations";
                cachedRank = "cachedHeliRanks";
                cachedRole = "cachedHeliRoles";
                cachedGameType = "cachedHeliGameTypes";
                cachedOrder = "cachedHeliOrder";
            }

            if (vmType == "Ship")
            {
                cachedTask = "cachedShipTask";
                cachedNation = "cachedShipNations";
                cachedRank = "cachedShipRanks";
                cachedRole = "cachedShipRoles";
                cachedGameType = "cachedShipGameTypes";
                cachedOrder = "cachedShipOrder";
            }
        }

        public async Task GetVehicleDataFromCacheAsync()
        {
            arrayOfPlanes = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
            arrayOfTanks = await BlobCache.UserAccount.GetObject<ArrayOfTanks>("cachedArrayOfTanks");
            arrayOfHelis = await BlobCache.UserAccount.GetObject<ArrayOfHelis>("cachedArrayOfHelis");
            arrayOfShips = await BlobCache.UserAccount.GetObject<ArrayOfShips>("cachedArrayOfShips");
        }

        protected async Task GetDataFromFilterPageAsync()
        {
            filterTask = await BlobCache.UserAccount.GetObject<string>(cachedTask);
            FilterTask = filterTask;

            var cacheNation = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>(cachedNation);
            filterNations = string.Join("|", cacheNation.Select(x => x.CodeName.ToString()).ToArray()).Split('|');

            var cacheRank = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>(cachedRank);
            filterRanks = string.Join("|", cacheRank.Select(x => x.CodeName.ToString()).ToArray()).Split('|');

            var cacheRole = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>(cachedRole);
            filterRoles = string.Join("|", cacheRole.Select(x => x.CodeName.ToString()).ToArray()).Split('|');

            var cacheGameType = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>(cachedGameType);
            filterGameTypes = string.Join("|", cacheGameType.Select(x => x.CodeName.ToString()).ToArray()).Split('|');

            filterOrder = await BlobCache.UserAccount.GetObject<string>(cachedOrder);

            MessagingCenter.Unsubscribe<FilterViewModel, string>(this, "filterClose");
        }
    }
}
