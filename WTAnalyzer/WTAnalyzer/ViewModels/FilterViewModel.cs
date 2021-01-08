using Akavache;
using System;
using System.Collections.Generic;
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
        private ObservableCollection<ChipsItem> nations;
        private ObservableCollection<ChipsItem> ranks;
        private ObservableCollection<ChipsItem> types;
        private ObservableCollection<string> orders;
        private ObservableCollection<string> tasks;

        private ObservableCollection<ChipsItem> selectedNations;
        private ObservableCollection<ChipsItem> selectedRanks;
        private ObservableCollection<ChipsItem> selectedTypes;
        private string selectedOrder;
        private string selectedTask;

        #endregion

        #region Ctor

        public FilterViewModel(INavigation navigation)
        {
            Debug.WriteLine("FilterPageViewModel constructor");

            Navigation = navigation;
            SubmitCommand = new Command(SubmitHandler);

            Tasks = TasksCollection.PlaneTasks();
            Nations = NationsCollection.PlaneNations();
            Ranks = RanksCollection.PlaneRanks();
            Types = TypesCollection.PlaneTypes();
            Orders = new ObservableCollection<string>()
            {
                 "Ascending",
                 "Descending",
            };

            Task.Run(CheckIfSelectedItemsExistInCache).Wait();

        }

        private async Task CheckIfSelectedItemsExistInCache()
        {
            var cacheTasks = await BlobCache.UserAccount.GetObject<string>("cachedSelectedTask");
            SelectedTask = Tasks[Tasks.IndexOf(cacheTasks)];

            var cacheRanks = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>("cachedSelectedRanks");
            selectedRanks = new ObservableCollection<ChipsItem>();

            foreach (var rank in from cacheRank in cacheRanks
                                 from rank in Ranks
                                 where rank.CodeName == cacheRank.CodeName
                                 select rank)
            {
                selectedRanks.Add(Ranks[Ranks.IndexOf(rank)]);
            }

            var cacheNations = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>("cachedSelectedNations");
            selectedNations = new ObservableCollection<ChipsItem>();

            foreach (var nation in from cacheNation in cacheNations
                                   from nation in Nations
                                   where nation.CodeName == cacheNation.CodeName
                                   select nation)
            {
                selectedNations.Add(Nations[Nations.IndexOf(nation)]);
            }

            var cacheTypes = await BlobCache.UserAccount.GetObject<ObservableCollection<ChipsItem>>("cachedSelectedTypes");
            selectedTypes = new ObservableCollection<ChipsItem>();

            foreach (var type in from cacheType in cacheTypes
                                 from type in Types
                                 where type.CodeName == cacheType.CodeName
                                 select type)
            {
                selectedTypes.Add(Types[Types.IndexOf(type)]);
            }

            var cacheOrders = await BlobCache.UserAccount.GetObject<string>("cachedSelectedOrder");
            SelectedOrder = Orders[Orders.IndexOf(cacheOrders)];


        }

        #endregion

        #region Public propertys

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

        public ObservableCollection<ChipsItem> Types
        {
            get => types;
            set
            {
                types = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChipsItem> SelectedTypes
        {
            get => selectedTypes;
            set
            {
                selectedTypes = value;
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

        private async void SubmitHandler(object obj) //Close filter page
        {
            Debug.WriteLine("SubmitHandler()");

            string filterNations = string.Join("|", selectedNations.Select(x => x.CodeName.ToString()).ToArray());
            string filterRank = string.Join("|", selectedRanks.Select(x => x.CodeName.ToString()).ToArray());
            string filterClass = string.Join("|", selectedTypes.Select(x => x.CodeName.ToString()).ToArray());
            string filterOrder = selectedOrder;
            string filterClose = "filterClose";

            if (Navigation.ModalStack.Count != 0)
            {
                await InsertFilterDataToCache();


                // MessagingCenter.Send(this, "filterTask", filterTask);
                MessagingCenter.Send(this, "filterNations", filterNations);
                MessagingCenter.Send(this, "filterRank", filterRank);
                MessagingCenter.Send(this, "filterClass", filterClass);
                MessagingCenter.Send(this, "filterOrder", filterOrder);
                MessagingCenter.Send(this, "filterClose", filterClose);


                await Navigation.PopModalAsync();
            }
        }

        private async Task InsertFilterDataToCache()
        {
            await BlobCache.UserAccount.InsertObject("cachedSelectedTask", SelectedTask, TimeSpan.FromDays(7));

            await BlobCache.UserAccount.Invalidate("cachedSelectedNations");
            await BlobCache.UserAccount.InsertObject("cachedSelectedNations", selectedNations, TimeSpan.FromDays(7));
            await BlobCache.UserAccount.Invalidate("cachedSelectedRanks");
            await BlobCache.UserAccount.InsertObject("cachedSelectedRanks", selectedRanks, TimeSpan.FromDays(7));
            await BlobCache.UserAccount.Invalidate("cachedSelectedTypes");
            await BlobCache.UserAccount.InsertObject("cachedSelectedTypes", selectedTypes, TimeSpan.FromDays(7));

            await BlobCache.UserAccount.InsertObject("cachedSelectedOrder", selectedOrder, TimeSpan.FromDays(7));
        }
    }
}
