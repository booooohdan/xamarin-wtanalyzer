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
using WTAnalyzer.Resx;
using WTAnalyzer.ViewModels.BaseViewModels;
using WTAnalyzer.XmlHandler;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels.VehicleViewModels
{
    public class OverallViewModel : BaseViewModel
    {
        #region Define variables

        public INavigation Navigation { get; set; }
        List<Plane> planes;
        List<Tank> tanks;
        List<Heli> helis;
        List<Ship> ships;

        #endregion

        #region View Properties

        private ObservableCollection<ChartsItem> doughnutCountPerNationData { get; set; }
        private ObservableCollection<ChartsItem> lineUsual { get; set; }
        private ObservableCollection<ChartsItem> linePremium { get; set; }
        private ObservableCollection<ChartsItem> linePromotional { get; set; }
        private ObservableCollection<ChartsItem> lineSquadron { get; set; }
        private ObservableCollection<ChartsItem> doughnutAviaRolesData { get; set; }
        private ObservableCollection<ChartsItem> doughnutTankRolesData { get; set; }
        private ObservableCollection<ChartsItem> doughnutHeliRolesData { get; set; }
        private ObservableCollection<ChartsItem> doughnutShipRolesData { get; set; }
        private ObservableCollection<ChartsItem> lineAvia { get; set; }
        private ObservableCollection<ChartsItem> lineTank { get; set; }
        private ObservableCollection<ChartsItem> lineHeli { get; set; }
        private ObservableCollection<ChartsItem> lineShip { get; set; }

        public ObservableCollection<ChartsItem> DoughnutCountPerNationData
        {
            get => doughnutCountPerNationData;
            set
            {
                doughnutCountPerNationData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> LineUsual
        {
            get => lineUsual;
            set
            {
                lineUsual = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> LinePremium
        {
            get => linePremium;
            set
            {
                linePremium = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> LinePromotional
        {
            get => linePromotional;
            set
            {
                linePromotional = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> LineSquadron
        {
            get => lineSquadron;
            set
            {
                lineSquadron = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> DoughnutAviaRolesData
        {
            get => doughnutAviaRolesData;
            set
            {
                doughnutAviaRolesData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> DoughnutTankRolesData
        {
            get => doughnutTankRolesData;
            set
            {
                doughnutTankRolesData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> DoughnutHeliRolesData
        {
            get => doughnutHeliRolesData;
            set
            {
                doughnutHeliRolesData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> DoughnutShipRolesData
        {
            get => doughnutShipRolesData;
            set
            {
                doughnutShipRolesData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> LineAvia
        {
            get => lineAvia;
            set
            {
                lineAvia = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> LineTank
        {
            get => lineTank;
            set
            {
                lineTank = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> LineHeli
        {
            get => lineHeli;
            set
            {
                lineHeli = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartsItem> LineShip
        {
            get => lineShip;
            set
            {
                lineShip = value;
                OnPropertyChanged();
            }
        }


        #endregion

        public OverallViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Busy = true;
            Task.Run(()=> 
            {
                Task.Run(GetVehicleDataFromCacheAsync).Wait();
                CreateCountPerNation();
                CreateCountPerType();
                CreateRolePerNation();
                CreateCountPerYear();
                Busy = false;
            });
        }

        private async Task GetVehicleDataFromCacheAsync()
        {
            var arrayOfPlanes = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
            var arrayOfTanks = await BlobCache.UserAccount.GetObject<ArrayOfTanks>("cachedArrayOfTanks");
            var arrayOfHelis = await BlobCache.UserAccount.GetObject<ArrayOfHelis>("cachedArrayOfHelis");
            var arrayOfShips = await BlobCache.UserAccount.GetObject<ArrayOfShips>("cachedArrayOfShips");
            planes = arrayOfPlanes.PlanesListApi.ToList();
            tanks = arrayOfTanks.TanksListApi.ToList();
            helis = arrayOfHelis.HelisListApi.ToList();
            ships = arrayOfShips.ShipsListApi.ToList();
        }

        private void CreateCountPerNation()
        {
            var vehicleList = (planes.Select(item => new ChartsItem(null, item.Nation))).ToList();
            vehicleList.AddRange(tanks.Select(item => new ChartsItem(null, item.Nation)));
            vehicleList.AddRange(helis.Select(item => new ChartsItem(null, item.Nation)));
            vehicleList.AddRange(ships.Select(item => new ChartsItem(null, item.Nation)));

            var usa = vehicleList.Where(x => x.YValue.ToString() == "USA").Count();
            var germ = vehicleList.Where(x => x.YValue.ToString() == "Germany").Count();
            var ussr = vehicleList.Where(x => x.YValue.ToString() == "USSR").Count();
            var gb = vehicleList.Where(x => x.YValue.ToString() == "Britain").Count();
            var jpn = vehicleList.Where(x => x.YValue.ToString() == "Japan").Count();
            var it = vehicleList.Where(x => x.YValue.ToString() == "Italy").Count();
            var fr = vehicleList.Where(x => x.YValue.ToString() == "France").Count();
            var ch = vehicleList.Where(x => x.YValue.ToString() == "China").Count();
            var sw = vehicleList.Where(x => x.YValue.ToString() == "Sweden").Count();

            DoughnutCountPerNationData = new ObservableCollection<ChartsItem>
            {
                new ChartsItem(AppResources.USA, usa),
                new ChartsItem(AppResources.Germany, germ),
                new ChartsItem(AppResources.USSR, ussr),
                new ChartsItem(AppResources.Britain, gb),
                new ChartsItem(AppResources.Japan, jpn),
                new ChartsItem(AppResources.Italy, it),
                new ChartsItem(AppResources.France, fr),
                new ChartsItem(AppResources.China, ch),
                new ChartsItem(AppResources.Sweden, sw)
            };
        }

        private void CreateCountPerType()
        {
            LineUsual = GetDataForVehicleCountPerType("Usual");
            LinePremium = GetDataForVehicleCountPerType("Premium");
            LinePromotional = GetDataForVehicleCountPerType("Promotional");
            LineSquadron = GetDataForVehicleCountPerType("Squadron");
        }

        private ObservableCollection<ChartsItem> GetDataForVehicleCountPerType(string gameType)
        {
            var vehicleList = (planes.Select(item => new ChartsItem(item.Type, item.Nation))).ToList();
            vehicleList.AddRange(tanks.Select(item => new ChartsItem(item.Type, item.Nation)));
            vehicleList.AddRange(helis.Select(item => new ChartsItem(item.Type, item.Nation)));
            vehicleList.AddRange(ships.Select(item => new ChartsItem(item.Type, item.Nation)));

            var dataForCharts = new ObservableCollection<ChartsItem>();
            foreach (var nation in NationsCollection.PlaneNations())
            {
                var planesCount = vehicleList.Where
                    (x => x.XValue.ToString() == gameType
                    && x.YValue.ToString() == nation.CodeName).Count();
                dataForCharts.Add(new ChartsItem(nation.Name, planesCount));
            }
            return dataForCharts;
        }

        private void CreateRolePerNation()
        {
            var vehicleList = (planes.Select(item => new ChartsItem(item.Class, item.Nation))).ToList();
            vehicleList.AddRange(tanks.Select(item => new ChartsItem(item.Class, item.Nation)));
            vehicleList.AddRange(helis.Select(item => new ChartsItem(item.Class, item.Nation)));
            vehicleList.AddRange(ships.Select(item => new ChartsItem(item.Class, item.Nation)));

            var fighter = vehicleList.Where(x => x.XValue.ToString() == "Fighter").Count();
            var attacker = vehicleList.Where(x => x.XValue.ToString() == "Attacker").Count();
            var bomber = vehicleList.Where(x => x.XValue.ToString() == "Bomber").Count();

            var lightTank = vehicleList.Where(x => x.XValue.ToString() == "LightTank").Count();
            var mediumTank = vehicleList.Where(x => x.XValue.ToString() == "MediumTank").Count();
            var tankHeavy = vehicleList.Where(x => x.XValue.ToString() == "TankHeavy").Count();
            var destroyerTank = vehicleList.Where(x => x.XValue.ToString() == "DestroyerTank").Count();
            var tankSPAA = vehicleList.Where(x => x.XValue.ToString() == "TankSPAA").Count();

            var attackHelicopter = vehicleList.Where(x => x.XValue.ToString() == "AttackHelicopter").Count();
            var utilityHelicopter = vehicleList.Where(x => x.XValue.ToString() == "UtilityHelicopter").Count();

            var shipDestroyer = vehicleList.Where(x => x.XValue.ToString() == "ShipDestroyer").Count();
            var shipCruiser = vehicleList.Where(x => x.XValue.ToString() == "ShipCruiser").Count();
            var battleship = vehicleList.Where(x => x.XValue.ToString() == "Battleship").Count();

            DoughnutAviaRolesData = new ObservableCollection<ChartsItem>
            {
                new ChartsItem(AppResources.Fighter, fighter),
                new ChartsItem(AppResources.Attacker, attacker),
                new ChartsItem(AppResources.Bomber, bomber),
            };

            DoughnutTankRolesData = new ObservableCollection<ChartsItem>
            {
                new ChartsItem(AppResources.LightTank, lightTank),
                new ChartsItem(AppResources.MediumTank, mediumTank),
                new ChartsItem(AppResources.HeavyTank, tankHeavy),
                new ChartsItem(AppResources.Destroyer, destroyerTank),
                new ChartsItem(AppResources.SPAA, tankSPAA),
            };

            DoughnutHeliRolesData = new ObservableCollection<ChartsItem>
            {
                new ChartsItem(AppResources.AttackHelicopter, attackHelicopter),
                new ChartsItem(AppResources.UtilityHelicopter, utilityHelicopter),
            };

            DoughnutShipRolesData = new ObservableCollection<ChartsItem>
            {
                new ChartsItem(AppResources.ShipDestroyer, shipDestroyer),
                new ChartsItem(AppResources.Cruiser, shipCruiser),
                new ChartsItem(AppResources.Battleship, battleship),
            };
        }

        private void CreateCountPerYear()
        {
            LineAvia = GetDataForVehicleCountPerYear("1");
            LineTank = GetDataForVehicleCountPerYear("2");
            LineHeli = GetDataForVehicleCountPerYear("3");
            LineShip = GetDataForVehicleCountPerYear("4");
        }

        private ObservableCollection<ChartsItem> GetDataForVehicleCountPerYear(string idStartSymbol)
        {
            var vehicleList = (planes.Select(item => new ChartsItem(item.VehicleId, item.FirstFlyYear))).ToList();
            vehicleList.AddRange(tanks.Select(item => new ChartsItem(item.VehicleId, item.FirstRideYear)));
            vehicleList.AddRange(helis.Select(item => new ChartsItem(item.VehicleId, item.FirstFlyYear)));
            vehicleList.AddRange(ships.Select(item => new ChartsItem(item.VehicleId, item.FirstLaunchYear)));

            var dataForCharts = new ObservableCollection<ChartsItem>();
            for (int year = 1900; year < 2020; year++)
            {
                var count = vehicleList.Where
                    (x => x.XValue.ToString().StartsWith(idStartSymbol)
                    && (int)x.YValue == year).Count();
                dataForCharts.Add(new ChartsItem(year, count));
            }
            return dataForCharts;
        }
    }
}
