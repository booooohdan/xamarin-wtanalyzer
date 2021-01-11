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
        private ObservableCollection<string> orders;
        private ObservableCollection<string> tasks;

        private ObservableCollection<ChipsItem> selectedNations;
        private ObservableCollection<ChipsItem> selectedRanks;
        private ObservableCollection<ChipsItem> selectedRoles;
        private string selectedOrder;
        private string selectedTask;

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

        public FilterViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ResetCommand = new Command(ResetHandler);
            SubmitCommand = new Command(SubmitHandler);
            SetDefaultDataToChips();
            Task.Run(SetDataToChipsFromCache).Wait();
        }

        private void SetDefaultDataToChips()
        {
            Tasks = TasksCollection.PlaneTasks();
            Nations = NationsCollection.PlaneNations();
            Ranks = RanksCollection.PlaneRanks();
            Roles = RolesCollection.PlaneRoles();
            Orders = new ObservableCollection<string>()
            {
                 "Ascending",
                 "Descending",
            };
        }

        private async void ResetHandler(object obj)
        {
            var planeFilterDataSetter = new PlaneFilterDataSetter();
            await planeFilterDataSetter.InitAsync();
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
            var cacheTasks = await BlobCache.UserAccount.GetObject<string>("cachedSelectedTask");
            SelectedTask = Tasks[Tasks.IndexOf(cacheTasks)];

            var cacheRanks = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>("cachedSelectedRanks");
            SelectedRanks = new ObservableCollection<ChipsItem>();

            foreach (var rank in from cacheRank in cacheRanks
                                 from rank in Ranks
                                 where rank.CodeName == cacheRank.CodeName
                                 select rank)
            {
                selectedRanks.Add(Ranks[Ranks.IndexOf(rank)]);
            }

            var cacheNations = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>("cachedSelectedNations");
            SelectedNations = new ObservableCollection<ChipsItem>();

            foreach (var nation in from cacheNation in cacheNations
                                   from nation in Nations
                                   where nation.CodeName == cacheNation.CodeName
                                   select nation)
            {
                selectedNations.Add(Nations[Nations.IndexOf(nation)]);
            }

            var cacheRoles = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>("cachedSelectedRoles");
            SelectedRoles = new ObservableCollection<ChipsItem>();

            foreach (var role in from cacheRole in cacheRoles
                                 from role in Roles
                                 where role.CodeName == cacheRole.CodeName
                                 select role)
            {
                selectedRoles.Add(Roles[Roles.IndexOf(role)]);
            }

            var cacheOrders = await BlobCache.UserAccount.GetObject<string>("cachedSelectedOrder");
            SelectedOrder = Orders[Orders.IndexOf(cacheOrders)];
        }

        private async Task WriteFilterDataToCache()
        {
            await BlobCache.UserAccount.Invalidate("cachedSelectedTask");
            await BlobCache.UserAccount.InsertObject("cachedSelectedTask", SelectedTask, TimeSpan.FromDays(7));

            await BlobCache.UserAccount.Invalidate("cachedSelectedNations");
            await BlobCache.UserAccount.InsertObject("cachedSelectedNations", SelectedNations, TimeSpan.FromDays(7));

            await BlobCache.UserAccount.Invalidate("cachedSelectedRanks");
            await BlobCache.UserAccount.InsertObject("cachedSelectedRanks", SelectedRanks, TimeSpan.FromDays(7));

            await BlobCache.UserAccount.Invalidate("cachedSelectedRoles");
            await BlobCache.UserAccount.InsertObject("cachedSelectedRoles", SelectedRoles, TimeSpan.FromDays(7));

            await BlobCache.UserAccount.Invalidate("cachedSelectedOrder");
            await BlobCache.UserAccount.InsertObject("cachedSelectedOrder", SelectedOrder, TimeSpan.FromDays(7));
        }
    }
}
