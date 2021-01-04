
using System.Collections.ObjectModel;
using WTAnalyzer.Models;

namespace WTAnalyzer.DataCollections
{
    public static class RanksCollection
    {
        public static ObservableCollection<ChipsItem> PlaneRanks() =>
           new ObservableCollection<ChipsItem>()
           {
                new ChipsItem() { Name = "I", CodeName = "I" },
                new ChipsItem() { Name = "II", CodeName = "II" },
                new ChipsItem() { Name = "III", CodeName = "III" },
                new ChipsItem() { Name = "IV", CodeName = "IV" },
                new ChipsItem() { Name = "V", CodeName = "V" },
                new ChipsItem() { Name = "VI", CodeName = "VI" },
           };
    }
}
