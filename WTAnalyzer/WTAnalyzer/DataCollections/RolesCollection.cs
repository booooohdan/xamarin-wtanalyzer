using System.Collections.ObjectModel;
using WTAnalyzer.Models;
using WTAnalyzer.Resx;

namespace WTAnalyzer.DataCollections
{
    public static class RolesCollection
    {
        public static ObservableCollection<ChipsItem> PlaneRoles() =>
            new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = AppResources.Fighter, CodeName = "Fighter" },
                new ChipsItem() { Name = AppResources.Attacker, CodeName = "Attacker" },
                new ChipsItem() { Name = AppResources.Bomber, CodeName = "Bomber" },
            };

    }
}
