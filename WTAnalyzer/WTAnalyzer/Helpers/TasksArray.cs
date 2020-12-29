

using System.Collections.ObjectModel;
using WTAnalyzer.Models;

namespace WTAnalyzer.Helpers
{
    public static class TasksArray
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
