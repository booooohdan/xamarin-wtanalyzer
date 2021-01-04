using System.Collections.ObjectModel;
using WTAnalyzer.Models;

namespace WTAnalyzer.DataCollections
{
    public static class TypesCollection
    {
        public static ObservableCollection<ChipsItem> PlaneTypes() =>
            new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = "Fighter", CodeName = "Fighter" },
                new ChipsItem() { Name = "Attacker", CodeName = "Attacker" },
                new ChipsItem() { Name = "Bomber", CodeName = "Bomber" },
            };

    }
}
