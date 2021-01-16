using System.Collections.ObjectModel;
using WTAnalyzer.Resx;

namespace WTAnalyzer.DataCollections
{
    public static class TasksCollection
    {
        public static ObservableCollection<string> PlaneTasks() =>
            new ObservableCollection<string>()
            {
                 AppResources.RepairCost,
                 AppResources.MaxSpeed,
                 AppResources.MaxSpeedAt5000M,
                 AppResources.Climb,
                 AppResources.TurnTime,
                 AppResources.EnginePower,
                 AppResources.Weight,
                 AppResources.Flutter,
                 AppResources.BombLoad,
                 AppResources.BurstMass,
                 AppResources.FirstFlyYear,
            };

        public static ObservableCollection<string> TankTasks() =>
            new ObservableCollection<string>()
            {
                 AppResources.RepairCost,
                 AppResources.SpeedAtTerrain,                 
                 AppResources.SpeedAtRoad,                 
                 AppResources.ReverseSpeed,                 
                 AppResources.EnginePower,                 
                 AppResources.Weight,                 
                 AppResources.Penetration,                 
                 AppResources.ReloadTime,                 
                 AppResources.TurretArmor,                 
                 AppResources.UpperArmorPlate,                 
                 AppResources.LowerArmorPlate,                 
                 AppResources.FirstRideYear,                 
            };

        public static ObservableCollection<string> HeliTasks() =>
            new ObservableCollection<string>()
            {
                 AppResources.RepairCost,
                 AppResources.SpeedAtSeaLevel,
                 AppResources.ClimbTime01000M,
                 AppResources.Weight,
                 AppResources.ATGMCount,
                 AppResources.ATGMRange,
                 AppResources.UnguidedMissile,
                 AppResources.FirstFlyYear,
            };

        public static ObservableCollection<string> ShipTasks() =>
            new ObservableCollection<string>()
            {
                 AppResources.RepairCost,
                 AppResources.MainCaliberSize,
                 AppResources.MainCaliberReload,
                 AppResources.TorpedoCount,
                 AppResources.TorpedoSpeed,
                 AppResources.TorpedoTNT,
                 AppResources.MaxSpeedShip,
                 AppResources.CirculationTime,
                 AppResources.TowerArmor,
                 AppResources.HullArmor,
                 AppResources.CrewCount,
                 AppResources.Displacement,
                 AppResources.LaunchedYear,
            };
    }
}
