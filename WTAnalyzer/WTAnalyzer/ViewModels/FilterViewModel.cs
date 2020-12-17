using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using WTAnalyzer.Models;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand SubmitCommand { get; }
        private ObservableCollection<ChipsItem> employees;
        private ObservableCollection<ChipsItem> selectedItems;



        public FilterViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SubmitCommand = new Command(SubmitHandler);

            Employees = new ObservableCollection<ChipsItem>()
        {
            new ChipsItem() { Name = "USA", ChipColor = Color.DarkCyan },
            new ChipsItem() { Name = "Germany", ChipColor = Color.Black },
            new ChipsItem() { Name = "USSR", ChipColor = Color.DarkBlue },
            new ChipsItem() { Name = "Britain", ChipColor = Color.DeepPink },
            new ChipsItem() { Name = "Japan", ChipColor = Color.DeepPink },
            new ChipsItem() { Name = "Italy", ChipColor = Color.DeepPink },
            new ChipsItem() { Name = "France", ChipColor = Color.DeepPink },
            new ChipsItem() { Name = "China", ChipColor = Color.DeepPink },
            new ChipsItem() { Name = "Sweden", ChipColor = Color.DeepPink }
        };

            selectedItems = new ObservableCollection<ChipsItem>()
            {
                Employees[0],
                Employees[1],
                Employees[2],
                Employees[3],
                Employees[4],
                Employees[5],
                Employees[6],
                Employees[7],
                Employees[8],
            };

        }


        public ObservableCollection<ChipsItem> Employees
        {
            get => employees;
            set
            {
                employees = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChipsItem> SelectedItems
        {
            get => selectedItems;
            set
            {
                selectedItems = value;
                OnPropertyChanged();
            }
        }

        private async void SubmitHandler(object obj)
        {
            string filter = string.Join("|", selectedItems.Select(x => x.Name.ToString()).ToArray());

            if (Navigation.ModalStack.Count != 0)
            {
                MessagingCenter.Send(this, "Message", filter);
                await Navigation.PopModalAsync();
            }
        }
    }
}
