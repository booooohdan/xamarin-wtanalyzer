using Akavache;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using WTAnalyzer.DataCollections;
using WTAnalyzer.Models;
using WTAnalyzer.Resx;

namespace WTAnalyzer.Cache
{
    public class ShipFilterDataSetter
    {
        private ObservableCollection<ChipsItem> selectedNations;
        private ObservableCollection<ChipsItem> selectedRanks;
        private ObservableCollection<ChipsItem> selectedRoles;
        private ObservableCollection<ChipsItem> selectedGameTypes;
        private string selectedOrder;
        private string selectedTask;
        public ObservableCollection<ChipsItem> Ranks { get; set; }
        public ObservableCollection<ChipsItem> Nations { get; set; }
        public ObservableCollection<ChipsItem> Roles { get; set; }
        public ObservableCollection<ChipsItem> GameTypes { get; set; }

        public async Task InitAsync()
        {
            selectedTask = AppResources.RepairCost;
            await BlobCache.UserAccount.Invalidate("cachedShipTask");
            await BlobCache.UserAccount.InsertObject("cachedShipTask", selectedTask, TimeSpan.FromDays(7));

            Nations = NationsCollection.ShipNations();
            selectedNations = new ObservableCollection<ChipsItem>()
                {
                    Nations[0],
                    Nations[1],
                    Nations[2],
                    Nations[3],
                    Nations[4],
                    Nations[5],
                };
            await BlobCache.UserAccount.Invalidate("cachedShipNations");
            await BlobCache.UserAccount.InsertObject("cachedShipNations", selectedNations, TimeSpan.FromDays(7));

            Ranks = RanksCollection.ShipRanks();
            selectedRanks = new ObservableCollection<ChipsItem>()
                {
                    Ranks[0],
                    Ranks[1],
                    Ranks[2],
                    Ranks[3],
                    Ranks[4],
                };
            await BlobCache.UserAccount.Invalidate("cachedShipRanks");
            await BlobCache.UserAccount.InsertObject("cachedShipRanks", selectedRanks, TimeSpan.FromDays(7));

            Roles = RolesCollection.ShipRoles();
            selectedRoles = new ObservableCollection<ChipsItem>()
                {
                    Roles[0],
                    Roles[1],
                    Roles[2],
                };
            await BlobCache.UserAccount.Invalidate("cachedShipRoles");
            await BlobCache.UserAccount.InsertObject("cachedShipRoles", selectedRoles, TimeSpan.FromDays(7));

            GameTypes = GameTypeCollection.GameTypes();
            selectedGameTypes = new ObservableCollection<ChipsItem>()
                {
                    GameTypes[0],
                    GameTypes[1],
                    GameTypes[2],
                    GameTypes[3],
                };
            await BlobCache.UserAccount.Invalidate("cachedShipGameTypes");
            await BlobCache.UserAccount.InsertObject("cachedShipGameTypes", selectedGameTypes, TimeSpan.FromDays(7));

            selectedOrder = AppResources.Descending;
            await BlobCache.UserAccount.Invalidate("cachedShipOrder");
            await BlobCache.UserAccount.InsertObject("cachedShipOrder", selectedOrder, TimeSpan.FromDays(7));
        }

    }
}
