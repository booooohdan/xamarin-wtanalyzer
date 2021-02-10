using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WTAnalyzer.Models;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels.BaseViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool busy;
        public bool Busy
        {
            get => busy;
            set
            {
                busy = value;
                OnPropertyChanged();
            }
        }

        private ListViewItem selectedVehicle;
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

        private async Task NavigateToOnePointBr()
        {
            await Shell.Current.GoToAsync($"//onepointbr?name={SelectedVehicle.Name}");
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
