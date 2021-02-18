using Akavache;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WTAnalyzer.Models;
using WTAnalyzer.XmlHandler;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels.BaseViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Define variables
        protected ArrayOfPlanes arrayOfPlanes { get; set; }
        protected ArrayOfTanks arrayOfTanks { get; set; }
        protected ArrayOfHelis arrayOfHelis { get; set; }
        protected ArrayOfShips arrayOfShips { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private bool busy;
        private ListViewItem selectedVehicle;
        ObservableCollection<ListViewItem> leftListView;
        ObservableCollection<ListViewItem> rightListView;
        #endregion

        #region View Properties
        public bool Busy
        {
            get => busy;
            set
            {
                busy = value;
                OnPropertyChanged();
            }
        }

        public ListViewItem SelectedVehicle
        {
            get => selectedVehicle;
            set
            {
                if (value != null)
                {
                    selectedVehicle = value;
                    OnPropertyChanged();
                    NavigateToOnePointBr();
                }
            }
        }

        public ObservableCollection<ListViewItem> LeftListView
        {
            get => leftListView;
            set
            {
                leftListView = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ListViewItem> RightListView
        {
            get => rightListView;
            set
            {
                rightListView = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public BaseViewModel()
        {
            Task.Run(GetVehicleDataFromCacheAsync).Wait();
        }

        public async Task GetVehicleDataFromCacheAsync()
        {
            arrayOfPlanes = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
            arrayOfTanks = await BlobCache.UserAccount.GetObject<ArrayOfTanks>("cachedArrayOfTanks");
            arrayOfHelis = await BlobCache.UserAccount.GetObject<ArrayOfHelis>("cachedArrayOfHelis");
            arrayOfShips = await BlobCache.UserAccount.GetObject<ArrayOfShips>("cachedArrayOfShips");
        }

        private async Task NavigateToOnePointBr()
        {
            await Shell.Current.GoToAsync($"//onepointbr?id={SelectedVehicle.Id}");
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
