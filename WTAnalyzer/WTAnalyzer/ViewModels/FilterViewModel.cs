using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using WTAnalyzer.Models;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        #region Define variables

        public INavigation Navigation { get; set; }
        public ICommand SubmitCommand { get; }
        private ObservableCollection<ChipsItem> nations;
        private ObservableCollection<ChipsItem> selectedNations;
        private ObservableCollection<ChipsItem> ranks;
        private ObservableCollection<ChipsItem> selectedRanks;
        private ObservableCollection<ChipsItem> types;
        private ObservableCollection<ChipsItem> selectedTypes;
        private ObservableCollection<string> orders;
        private string selectedOrder;
        private ObservableCollection<string> tasks;
        private string selectedTask;

        #endregion

        #region Ctor

        public FilterViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SubmitCommand = new Command(SubmitHandler);

            Nations = new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = "USA", CodeName = "USA" },
                new ChipsItem() { Name = "Germany", CodeName = "Germany" },
                new ChipsItem() { Name = "USSR", CodeName = "USSR" },
                new ChipsItem() { Name = "Britain", CodeName = "Britain" },
                new ChipsItem() { Name = "Japan", CodeName = "Japan" },
                new ChipsItem() { Name = "Italy", CodeName = "Italy" },
                new ChipsItem() { Name = "France", CodeName = "France" },
                new ChipsItem() { Name = "China", CodeName = "China" },
                new ChipsItem() { Name = "Sweden", CodeName = "Sweden" }
            };

            selectedNations = new ObservableCollection<ChipsItem>()
            {
                Nations[0],
                Nations[1],
                Nations[2],
                Nations[3],
                Nations[4],
                Nations[5],
                Nations[6],
                Nations[7],
                Nations[8],
            };

            Ranks = new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = "I", CodeName = "I" },
                new ChipsItem() { Name = "II", CodeName = "II" },
                new ChipsItem() { Name = "III", CodeName = "III" },
                new ChipsItem() { Name = "IV", CodeName = "IV" },
                new ChipsItem() { Name = "V", CodeName = "V" },
                new ChipsItem() { Name = "VI", CodeName = "VI" },
                new ChipsItem() { Name = "VII", CodeName = "VII" },
            };

            selectedRanks = new ObservableCollection<ChipsItem>()
            {
                Ranks[0],
                Ranks[1],
                Ranks[2],
                Ranks[3],
                Ranks[4],
                Ranks[5],
                Ranks[6],
            };

            Types = new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = "Fighter", CodeName = "Fighter" },
                new ChipsItem() { Name = "Attacker", CodeName = "Attacker" },
                new ChipsItem() { Name = "Bomber", CodeName = "Bomber" },
            };

            selectedTypes = new ObservableCollection<ChipsItem>()
            {
                Types[0],
                Types[1],
                Types[2],
            };

            Orders = new ObservableCollection<string>()
            {
                 "Ascending",
                 "Descending",
            };

            SelectedOrder = "Ascending";

            Tasks = new ObservableCollection<string>()
            {
                 "Count",
                 "Repair Cost",
                 "Max Speed",
                 "Max Speed at 5000 m",
                 "Climb",
                 "Turn Time",
                 "Engine Power",
                 "Weight",
                 "Flutter",
                 "Optimal Alitude",
                 "Bomb Load",
                 "Burst Mass",
                 "First fly Year",
            };

            SelectedTask = "Repair Cost";

            Debug.WriteLine("FilterPageViewModel constructor");
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

            string filterTask = selectedTask;
            string filterNations = string.Join("|", selectedNations.Select(x => x.CodeName.ToString()).ToArray());
            string filterRank = string.Join("|", selectedRanks.Select(x => x.CodeName.ToString()).ToArray());
            string filterClass = string.Join("|", selectedTypes.Select(x => x.CodeName.ToString()).ToArray());
            string filterOrder = selectedOrder;
            string filterClose = "filterClose";

            if (Navigation.ModalStack.Count != 0)
            {
                MessagingCenter.Send(this, "filterTask", filterTask);
                MessagingCenter.Send(this, "filterNations", filterNations);
                MessagingCenter.Send(this, "filterRank", filterRank);
                MessagingCenter.Send(this, "filterClass", filterClass);
                MessagingCenter.Send(this, "filterOrder", filterOrder);
                MessagingCenter.Send(this, "filterClose", filterClose);
                await Navigation.PopModalAsync();
            }
        }
    }
}
