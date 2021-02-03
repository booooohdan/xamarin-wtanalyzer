using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
