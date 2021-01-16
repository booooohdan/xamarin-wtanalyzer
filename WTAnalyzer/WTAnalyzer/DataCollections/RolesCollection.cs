using System.Collections.ObjectModel;
using WTAnalyzer.Models;
using WTAnalyzer.Resx;

namespace WTAnalyzer.DataCollections
{
    public static class RolesCollection
    {
        public static ObservableCollection<ChipsItem> PlaneRoles() =>
            new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = AppResources.Fighter, CodeName = "Fighter" },
                new ChipsItem() { Name = AppResources.Attacker, CodeName = "Attacker" },
                new ChipsItem() { Name = AppResources.Bomber, CodeName = "Bomber" },
            };

        public static ObservableCollection<ChipsItem> TankRoles() =>
            new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = AppResources.LightTank, CodeName = "LightTank" },
                new ChipsItem() { Name = AppResources.MediumTank, CodeName = "MediumTank" },
                new ChipsItem() { Name = AppResources.HeavyTank, CodeName = "TankHeavy" },
                new ChipsItem() { Name = AppResources.Destroyer, CodeName = "DestroyerTank" },
                new ChipsItem() { Name = AppResources.SPAA, CodeName = "TankSPAA" },
           };

        public static ObservableCollection<ChipsItem> HeliRoles() =>
            new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = AppResources.AttackHelicopter, CodeName = "AttackHelicopter" },
                new ChipsItem() { Name = AppResources.UtilityHelicopter, CodeName = "UtilityHelicopter" },
            };

        public static ObservableCollection<ChipsItem> ShipRoles() =>
           new ObservableCollection<ChipsItem>()
           {
                new ChipsItem() { Name = AppResources.ShipDestroyer, CodeName = "ShipDestroyer" },
                new ChipsItem() { Name = AppResources.Cruiser, CodeName = "ShipCruiser" },
                new ChipsItem() { Name = AppResources.Battleship, CodeName = "Battleship" },
            };
    }
}
