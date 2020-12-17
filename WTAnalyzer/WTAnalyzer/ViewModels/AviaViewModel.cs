using Akavache;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WTAnalyzer.Helpers;
using WTAnalyzer.Models;
using WTAnalyzer.Views.ServicePages;
using WTAnalyzer.XmlHandler;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    public class AviaViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand OpenFilterModalPageCommand { get; }
        ArrayOfPlanes arrayOfPlanes;
        IEnumerable<Plane> result;

        private ObservableCollection<DataPoint> lineUsa { get; set; }
        private ObservableCollection<DataPoint> lineGermany { get; set; }
        private ObservableCollection<DataPoint> lineUssr { get; set; }
        private ObservableCollection<DataPoint> lineBritain { get; set; }
        private ObservableCollection<DataPoint> lineJapan { get; set; }
        private ObservableCollection<DataPoint> lineItaly { get; set; }
        private ObservableCollection<DataPoint> lineFrance { get; set; }
        private ObservableCollection<DataPoint> lineChina { get; set; }
        private ObservableCollection<DataPoint> lineSweden { get; set; }
        string filter;
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
                OnPropertyChanged();
            }
             
        }
        public ObservableCollection<DataPoint> LineUsa
        {
            get => lineUsa;
            set
            {
                lineUsa = value;
                OnPropertyChanged(); 
            }
        }
        public ObservableCollection<DataPoint> LineGermany
        {
            get => lineGermany;
            set
            {
                lineGermany = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DataPoint> LineUssr { get; set; }
        public ObservableCollection<DataPoint> LineBritain { get; set; }
        public ObservableCollection<DataPoint> LineJapan { get; set; }
        public ObservableCollection<DataPoint> LineItaly { get; set; }
        public ObservableCollection<DataPoint> LineFrance { get; set; }
        public ObservableCollection<DataPoint> LineChina { get; set; }
        public ObservableCollection<DataPoint> LineSweden { get; set; }

        public AviaViewModel(INavigation navigation)
        {
            Navigation = navigation;
            OpenFilterModalPageCommand = new Command(OpenFilterModalPageHandler);
            Task.Run(FillListFromCacheAsync).Wait();
            Filter = "Ctor";

            result =new List<Plane>();
            var a = arrayOfPlanes.PlanesListApi.Where(x => x.Nation == "USA");
            result = result.Union(a);

            /*LineData1 = new ObservableCollection<DataPoint>
            {
                new DataPoint(1.0, 21),
                new DataPoint(1.3, 3),
                new DataPoint(1.7, 12),
                new DataPoint(2.0, 31),
                new DataPoint(2.3, 31),
                new DataPoint(2.7, 21),
                new DataPoint(10.7, 70)
            };*/

            /*LineUsa = new ObservableCollection<DataPoint>();
            foreach (double number in BRArray.PlanesBR())
            {
                var planesCount = result.Where(x => x.BR == number).Count();
                LineUsa.Add(new DataPoint(number, planesCount));
            }*/

            /*
            LineData2 = new ObservableCollection<DataPoint>
            {
                new DataPoint("2005", 28),
                new DataPoint("2006", 44),
                new DataPoint("2007", 48),
                new DataPoint("2008", 50),
                new DataPoint("2009", 66),
                new DataPoint("2010", 78),
                new DataPoint("2011", 84)

                //GetUsaWithFilter("usa");
            };*/
            MessagingCenter.Subscribe<FilterViewModel, string>(this, "Message", (sender, arg) => { 
                GetPlaneWithFilter(arg);
                Filter = arg;
            });
        }

        public void GetPlaneWithFilter(string filter)
        {

            /*if (filter.Contains("USA"))
            {
                var a = arrayOfPlanes.PlanesListApi.Where(x => x.Nation == "USA");
                result = result.Union(a);
            }

            if (filter.Contains("Germany"))
            {
                var b = arrayOfPlanes.PlanesListApi.Where(x => x.Nation == "Germany").ToList();
                result = result.Union(b);
            }*/
            LineUsa = filter.Contains("USA") ? GetLineDataPoint("USA", "count") : null;
            LineGermany = filter.Contains("Germany") ? GetLineDataPoint("Germany", "count") : null;
        }

        public async Task FillListFromCacheAsync()
        {
            arrayOfPlanes = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
        }

        public ObservableCollection<DataPoint> GetLineDataPoint(string nation, string task)
        {
            var planesAll = arrayOfPlanes.PlanesListApi.Where(x => x.Nation == nation).ToList();
            var datas = new ObservableCollection<DataPoint>();

            switch (task)
            {
                case "count":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Count();
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;

                case "repaircost":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Max(x => x.RepairCost);
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;

                case "maxspeed":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Max(x => x.MaxSpeedAt0);
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;

                case "maxspeedat5000m":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Max(x => x.MaxSpeedAt5000);
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;

                case "climb":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Min(x => x.Climb);
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;

                case "turntime":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Min(x => x.TurnAt0);
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;

                case "enginepower":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Max(x => x.EnginePower);
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;

                case "weight":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Min(x => x.Weight);
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;

                case "flutter":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Max(x => x.Flutter);
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;

                case "optimalalitude":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Max(x => x.OptimalAlitude);
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;

                case "bombload":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Max(x => x.BombLoad);
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;

                case "burstmass":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Max(x => x.BurstMass);
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;

                case "firstflyyear":
                    foreach (double number in BRArray.PlanesBR())
                    {
                        var planesCount = planesAll.Where(x => x.BR == number).Max(x => x.FirstFlyYear);
                        datas.Add(new DataPoint(number, planesCount));
                    }
                    break;
            }
            return datas;
        }

        private async void OpenFilterModalPageHandler(object obj)
        {
            if (Navigation.ModalStack.Count == 0)
            { 
                await Navigation.PushModalAsync(new FilterPage());
            }
        }
    }
}
