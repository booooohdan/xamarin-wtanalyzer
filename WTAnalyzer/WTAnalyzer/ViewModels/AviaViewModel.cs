using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using WTAnalyzer.Models;
using WTAnalyzer.Views.ServicePages;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    public class AviaViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand ChooseCommand { get; }
        public ObservableCollection<DataPoint> LineData1 { get; set; }
        public ObservableCollection<DataPoint> LineData2 { get; set; }

        public AviaViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ChooseCommand = new Command(ChooseHandler);

            LineData1 = new ObservableCollection<DataPoint>
            {
                new DataPoint("2005", 21),
                new DataPoint("2006", 24),
                new DataPoint("2007", 36),
                new DataPoint("2008", 38),
                new DataPoint("2009", 54),
                new DataPoint("2010", 57),
                new DataPoint("2011", 70)
            };

            LineData2 = new ObservableCollection<DataPoint>
            {
                new DataPoint("2005", 28),
                new DataPoint("2006", 44),
                new DataPoint("2007", 48),
                new DataPoint("2008", 50),
                new DataPoint("2009", 66),
                new DataPoint("2010", 78),
                new DataPoint("2011", 84)
            };

        }

        private async void ChooseHandler(object obj)
        {
             await Navigation.PushModalAsync(new FilterPage());
        }
    }
}
