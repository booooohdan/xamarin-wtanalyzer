using System.Collections.ObjectModel;
using WTAnalyzer.Models;
using WTAnalyzer.Resx;

namespace WTAnalyzer.DataCollections
{
    public static class GameTypeCollection
    {
        public static ObservableCollection<ChipsItem> GameTypes() =>
            new ObservableCollection<ChipsItem>()
            {
                new ChipsItem() { Name = AppResources.Usual, CodeName = "Usual" },
                new ChipsItem() { Name = AppResources.Premium, CodeName = "Premium" },
                new ChipsItem() { Name = AppResources.Promotional, CodeName = "Promotional" },
                new ChipsItem() { Name = AppResources.Squadron, CodeName = "Squadron" },
            };
    }
}
