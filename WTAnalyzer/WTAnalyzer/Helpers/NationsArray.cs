using System.Collections.ObjectModel;
using WTAnalyzer.Models;

namespace WTAnalyzer.Helpers
{
    public class NationsArray
    {
        public static ObservableCollection<ChipsItem> PlaneNations() => 
            new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = "USA", CodeName = "USA" },
                new ChipsItem() { Name = "Germany", CodeName = "Germany" },
                new ChipsItem() { Name = "USSR", CodeName = "USSR" },
                new ChipsItem() { Name = "Britain", CodeName = "Britain" },
                new ChipsItem() { Name = "Japan", CodeName = "Japan" },
                new ChipsItem() { Name = "Italy", CodeName = "Italy" },
                new ChipsItem() { Name = "France", CodeName = "France" },
                new ChipsItem() { Name = "China", CodeName = "China" },
                new ChipsItem() { Name = "Sweden", CodeName = "Sweden" }
            };

       
    }
}
