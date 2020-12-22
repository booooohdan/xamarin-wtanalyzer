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
                new ChipsItem() { Name = "USA", ChipColor = Color.Gray },
                new ChipsItem() { Name = "Germany", ChipColor = Color.Gray },
                new ChipsItem() { Name = "USSR", ChipColor = Color.Gray },
                new ChipsItem() { Name = "Britain", ChipColor = Color.Gray },
                new ChipsItem() { Name = "Japan", ChipColor = Color.Gray },
                new ChipsItem() { Name = "Italy", ChipColor = Color.Gray },
                new ChipsItem() { Name = "France", ChipColor = Color.Gray },
                new ChipsItem() { Name = "China", ChipColor = Color.Gray },
                new ChipsItem() { Name = "Sweden", ChipColor = Color.Gray }
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
                new ChipsItem() { Name = "I", ChipColor = Color.Gray },
                new ChipsItem() { Name = "II", ChipColor = Color.Gray },
                new ChipsItem() { Name = "III", ChipColor = Color.Gray },
                new ChipsItem() { Name = "IV", ChipColor = Color.Gray },
                new ChipsItem() { Name = "V", ChipColor = Color.Gray },
                new ChipsItem() { Name = "VI", ChipColor = Color.Gray },
                new ChipsItem() { Name = "VII", ChipColor = Color.Gray },
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
                new ChipsItem() { Name = "Fighter", ChipColor = Color.Gray },
                new ChipsItem() { Name = "Attacker", ChipColor = Color.Gray },
                new ChipsItem() { Name = "Bomber", ChipColor = Color.Gray },
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

            SelectedTask = "Count";

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
            string filterNations = string.Join("|", selectedNations.Select(x => x.Name.ToString()).ToArray());
            string filterRank = string.Join("|", selectedRanks.Select(x => x.Name.ToString()).ToArray());
            string filterTypes = string.Join("|", selectedTypes.Select(x => x.Name.ToString()).ToArray());
            string filterOrder = selectedOrder;
            string filterClose = "filterClose";

            if (Navigation.ModalStack.Count != 0)
            {
                MessagingCenter.Send(this, "filterTask", filterTask);
                MessagingCenter.Send(this, "filterNations", filterNations);
                MessagingCenter.Send(this, "filterRank", filterRank);
                MessagingCenter.Send(this, "filterTypes", filterTypes);
                MessagingCenter.Send(this, "filterOrder", filterOrder);
                MessagingCenter.Send(this, "filterClose", filterClose);
                await Navigation.PopModalAsync();
            }
        }
    }
}
