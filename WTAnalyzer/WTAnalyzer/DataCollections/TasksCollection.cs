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
                 AppResources.OptimalAlitude,
                 AppResources.BombLoad,
                 AppResources.BurstMass,
                 AppResources.FirstFlyYear,
            };
    }
}
