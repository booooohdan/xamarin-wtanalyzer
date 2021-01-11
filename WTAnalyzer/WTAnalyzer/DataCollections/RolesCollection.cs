using System.Collections.ObjectModel;
using WTAnalyzer.Models;

namespace WTAnalyzer.DataCollections
{
    public static class RolesCollection
    {
        public static ObservableCollection<ChipsItem> PlaneRoles() =>
            new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = "Fighter", CodeName = "Fighter" },
                new ChipsItem() { Name = "Attacker", CodeName = "Attacker" },
                new ChipsItem() { Name = "Bomber", CodeName = "Bomber" },
            };

    }
}
