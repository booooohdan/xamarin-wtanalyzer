using System.Collections.ObjectModel;
using WTAnalyzer.Models;
using WTAnalyzer.Resx;

namespace WTAnalyzer.DataCollections
{
    public class NationsCollection
    {
        public static ObservableCollection<ChipsItem> PlaneNations() =>
            new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = AppResources.USA, CodeName = "USA" },
                new ChipsItem() { Name = AppResources.Germany, CodeName = "Germany" },
                new ChipsItem() { Name = AppResources.USSR, CodeName = "USSR" },
                new ChipsItem() { Name = AppResources.Britain, CodeName = "Britain" },
                new ChipsItem() { Name = AppResources.Japan, CodeName = "Japan" },
                new ChipsItem() { Name = AppResources.Italy, CodeName = "Italy" },
                new ChipsItem() { Name = AppResources.France, CodeName = "France" },
                new ChipsItem() { Name = AppResources.China, CodeName = "China" },
                new ChipsItem() { Name = AppResources.Sweden, CodeName = "Sweden" }
            };


    }
}
