
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

        public static ObservableCollection<ChipsItem> TankRanks() =>
           new ObservableCollection<ChipsItem>()
           {
                new ChipsItem() { Name = "I", CodeName = "I" },
                new ChipsItem() { Name = "II", CodeName = "II" },
                new ChipsItem() { Name = "III", CodeName = "III" },
                new ChipsItem() { Name = "IV", CodeName = "IV" },
                new ChipsItem() { Name = "V", CodeName = "V" },
                new ChipsItem() { Name = "VI", CodeName = "VI" },
                new ChipsItem() { Name = "VII", CodeName = "VII" },
          };

        public static ObservableCollection<ChipsItem> HeliRanks() =>
           new ObservableCollection<ChipsItem>()
           {
                new ChipsItem() { Name = "V", CodeName = "V" },
                new ChipsItem() { Name = "VI", CodeName = "VI" },
                new ChipsItem() { Name = "VII", CodeName = "VII" },
          };

        public static ObservableCollection<ChipsItem> ShipRanks() =>
           new ObservableCollection<ChipsItem>()
           {
                new ChipsItem() { Name = "I", CodeName = "I" },
                new ChipsItem() { Name = "II", CodeName = "II" },
                new ChipsItem() { Name = "III", CodeName = "III" },
                new ChipsItem() { Name = "IV", CodeName = "IV" },
                new ChipsItem() { Name = "V", CodeName = "V" },
           };
    }
}
