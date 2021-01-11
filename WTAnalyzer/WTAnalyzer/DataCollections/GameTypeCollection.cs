using System.Collections.ObjectModel;
using WTAnalyzer.Models;

namespace WTAnalyzer.DataCollections
{
    public static class GameTypeCollection
    {
        public static ObservableCollection<ChipsItem> GameTypes() =>
            new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = "Usual", CodeName = "Usual" },
                new ChipsItem() { Name = "Premium", CodeName = "Premium" },
                new ChipsItem() { Name = "Promotional", CodeName = "Promotional" },
                new ChipsItem() { Name = "Squadron", CodeName = "Squadron" },
            };
    }
}
