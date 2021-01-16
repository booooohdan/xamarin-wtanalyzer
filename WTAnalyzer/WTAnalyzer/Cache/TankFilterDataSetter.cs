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
    public class TankFilterDataSetter
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
            await BlobCache.UserAccount.Invalidate("cachedTankTask");
            await BlobCache.UserAccount.InsertObject("cachedTankTask", selectedTask, TimeSpan.FromDays(7));

            Nations = NationsCollection.TankNations();
            selectedNations = new ObservableCollection<ChipsItem>()
                {
                    Nations[0],
                    Nations[1],
                    Nations[2],
                    Nations[3],
                    Nations[4],
                    Nations[5],
                    Nations[6],
                    Nations[7],
                    Nations[8],
                };
            await BlobCache.UserAccount.Invalidate("cachedTankNations");
            await BlobCache.UserAccount.InsertObject("cachedTankNations", selectedNations, TimeSpan.FromDays(7));

            Ranks = RanksCollection.TankRanks();
            selectedRanks = new ObservableCollection<ChipsItem>()
                {
                    Ranks[0],
                    Ranks[1],
                    Ranks[2],
                    Ranks[3],
                    Ranks[4],
                    Ranks[5],
                    Ranks[6],
                };
            await BlobCache.UserAccount.Invalidate("cachedTankRanks");
            await BlobCache.UserAccount.InsertObject("cachedTankRanks", selectedRanks, TimeSpan.FromDays(7));

            Roles = RolesCollection.TankRoles();
            selectedRoles = new ObservableCollection<ChipsItem>()
                {
                    Roles[0],
                    Roles[1],
                    Roles[2],
                    Roles[3],
                    Roles[4],
                };
            await BlobCache.UserAccount.Invalidate("cachedTankRoles");
            await BlobCache.UserAccount.InsertObject("cachedTankRoles", selectedRoles, TimeSpan.FromDays(7));

            GameTypes = GameTypeCollection.GameTypes();
            selectedGameTypes = new ObservableCollection<ChipsItem>()
                {
                    GameTypes[0],
                    GameTypes[1],
                    GameTypes[2],
                    GameTypes[3],
                };
            await BlobCache.UserAccount.Invalidate("cachedTankGameTypes");
            await BlobCache.UserAccount.InsertObject("cachedTankGameTypes", selectedGameTypes, TimeSpan.FromDays(7));

            selectedOrder = AppResources.Descending;
            await BlobCache.UserAccount.Invalidate("cachedTankOrder");
            await BlobCache.UserAccount.InsertObject("cachedTankOrder", selectedOrder, TimeSpan.FromDays(7));
        }

    }
}
