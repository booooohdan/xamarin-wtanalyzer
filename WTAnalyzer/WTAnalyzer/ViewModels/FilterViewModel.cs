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
using WTAnalyzer.ViewModels.BaseViewModels;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        #region Define variables

        public INavigation Navigation { get; set; }
        public ICommand SubmitCommand { get; }
        public ICommand ResetCommand { get; }
        private ObservableCollection<ChipsItem> nations;
        private ObservableCollection<ChipsItem> ranks;
        private ObservableCollection<ChipsItem> roles;
        private ObservableCollection<ChipsItem> gameTypes;
        private ObservableCollection<string> orders;
        private ObservableCollection<string> tasks;

        private ObservableCollection<ChipsItem> selectedNations;
        private ObservableCollection<ChipsItem> selectedRanks;
        private ObservableCollection<ChipsItem> selectedRoles;
        private ObservableCollection<ChipsItem> selectedGameTypes;
        private string selectedOrder;
        private string selectedTask;

        private string vmType;
        private string cachedTask;
        private string cachedNation;
        private string cachedRank;
        private string cachedRole;
        private string cachedGameType;
        private string cachedOrder;

        #endregion

        #region View propertys

        public ObservableCollection<ChipsItem> Nations
        {
            get => nations;
            set
            {
                nations = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChipsItem> SelectedNations
        {
            get => selectedNations;
            set
            {
                selectedNations = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChipsItem> Ranks
        {
            get => ranks;
            set
            {
                ranks = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChipsItem> SelectedRanks
        {
            get => selectedRanks;
            set
            {
                selectedRanks = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChipsItem> Roles
        {
            get => roles;
            set
            {
                roles = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChipsItem> SelectedRoles
        {
            get => selectedRoles;
            set
            {
                selectedRoles = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChipsItem> GameTypes
        {
            get => gameTypes;
            set
            {
                gameTypes = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChipsItem> SelectedGameTypes
        {
            get => selectedGameTypes;
            set
            {
                selectedGameTypes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Orders
        {
            get => orders;
            set
            {
                orders = value;
                OnPropertyChanged();
            }
        }

        public string SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> Tasks
        {
            get => tasks;
            set
            {
                tasks = value;
                OnPropertyChanged();
            }
        }
        public string SelectedTask
        {
            get => selectedTask;
            set
            {
                selectedTask = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public FilterViewModel(INavigation navigation, string vmType)
        {
            Navigation = navigation;
            ResetCommand = new Command(ResetHandler);
            SubmitCommand = new Command(SubmitHandler);
            this.vmType = vmType;
            SetParametersDependingCallingViewModel();

            SetDefaultDataToChips();
            Task.Run(SetDataToChipsFromCache).Wait();
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

        }

        private void SetDefaultDataToChips()
        {
            if (vmType == "Avia")
            {
                Tasks = TasksCollection.PlaneTasks();
                Nations = NationsCollection.PlaneNations();
                Ranks = RanksCollection.PlaneRanks();
                Roles = RolesCollection.PlaneRoles();
            }

            if (vmType == "Tank")
            {
                Tasks = TasksCollection.TankTasks();
                Nations = NationsCollection.TankNations();
                Ranks = RanksCollection.TankRanks();
                Roles = RolesCollection.TankRoles();
            }

            GameTypes = GameTypeCollection.GameTypes();
            Orders = OrderCollection.Order();
        }

        private async void ResetHandler(object obj)
        {
            if (vmType == "Avia")
            {
                await new PlaneFilterDataSetter().InitAsync();
            }

            if (vmType == "Tank")
            {
                await new TankFilterDataSetter().InitAsync();
            }
            await SetDataToChipsFromCache();
        }

        private async void SubmitHandler(object obj)
        {
            if (Navigation.ModalStack.Count != 0)
            {
                await WriteFilterDataToCache();
                MessagingCenter.Send(this, "filterClose", "");
                await Navigation.PopModalAsync();
            }
        }

        private async Task SetDataToChipsFromCache()
        {
            var cacheTasks = await BlobCache.UserAccount.GetObject<string>(cachedTask);
            SelectedTask = Tasks[Tasks.IndexOf(cacheTasks)];

            var cacheNations = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>(cachedNation);
            SelectedNations = new ObservableCollection<ChipsItem>();

            foreach (var nation in from cacheNation in cacheNations
                                   from nation in Nations
                                   where nation.CodeName == cacheNation.CodeName
                                   select nation)
            {
                selectedNations.Add(Nations[Nations.IndexOf(nation)]);
            }

            var cacheRanks = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>(cachedRank);
            SelectedRanks = new ObservableCollection<ChipsItem>();

            foreach (var rank in from cacheRank in cacheRanks
                                 from rank in Ranks
                                 where rank.CodeName == cacheRank.CodeName
                                 select rank)
            {
                selectedRanks.Add(Ranks[Ranks.IndexOf(rank)]);
            }

            var cacheRoles = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>(cachedRole);
            SelectedRoles = new ObservableCollection<ChipsItem>();

            foreach (var role in from cacheRole in cacheRoles
                                 from role in Roles
                                 where role.CodeName == cacheRole.CodeName
                                 select role)
            {
                selectedRoles.Add(Roles[Roles.IndexOf(role)]);
            }

            var cacheGameTypes = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>(cachedGameType);
            SelectedGameTypes = new ObservableCollection<ChipsItem>();

            foreach (var gameType in from cacheGameType in cacheGameTypes
                                 from gameType in GameTypes
                                 where gameType.CodeName == cacheGameType.CodeName
                                 select gameType)
            {
                selectedGameTypes.Add(GameTypes[GameTypes.IndexOf(gameType)]);
            }

            var cacheOrders = await BlobCache.UserAccount.GetObject<string>(cachedOrder);
            SelectedOrder = Orders[Orders.IndexOf(cacheOrders)];
        }

        private async Task WriteFilterDataToCache()
        {
            await BlobCache.UserAccount.Invalidate(cachedTask);
            await BlobCache.UserAccount.InsertObject(cachedTask, SelectedTask, TimeSpan.FromDays(7));

            await BlobCache.UserAccount.Invalidate(cachedNation);
            await BlobCache.UserAccount.InsertObject(cachedNation, SelectedNations, TimeSpan.FromDays(7));

            await BlobCache.UserAccount.Invalidate(cachedRank);
            await BlobCache.UserAccount.InsertObject(cachedRank, SelectedRanks, TimeSpan.FromDays(7));

            await BlobCache.UserAccount.Invalidate(cachedRole);
            await BlobCache.UserAccount.InsertObject(cachedRole, SelectedRoles, TimeSpan.FromDays(7));
            
            await BlobCache.UserAccount.Invalidate(cachedGameType);
            await BlobCache.UserAccount.InsertObject(cachedGameType, SelectedGameTypes, TimeSpan.FromDays(7));

            await BlobCache.UserAccount.Invalidate(cachedOrder);
            await BlobCache.UserAccount.InsertObject(cachedOrder, SelectedOrder, TimeSpan.FromDays(7));
        }
    }
}
