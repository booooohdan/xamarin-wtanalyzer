

using System.Collections.ObjectModel;

namespace WTAnalyzer.DataCollections
{
    public static class TasksCollection
    {
        public static ObservableCollection<string> PlaneTasks() =>
            new ObservableCollection<string>()
            {
                 "Repair Cost",
                 "Max Speed",
                 "Max Speed at 5000 m",
                 "Climb",
                 "Turn Time",
                 "Engine Power",
                 "Weight",
                 "Flutter",
                 "Optimal Alitude",
                 "Bomb Load",
                 "Burst Mass",
                 "First fly Year",
            };
    }
}
