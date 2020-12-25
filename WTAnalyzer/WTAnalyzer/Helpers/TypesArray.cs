using System.Collections.ObjectModel;
using WTAnalyzer.Models;

namespace WTAnalyzer.Helpers
{
    public static class TypesArray
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
