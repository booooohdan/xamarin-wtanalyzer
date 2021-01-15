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
    public class PlaneFilterDataSetter
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
            await BlobCache.UserAccount.Invalidate("cachedAviaTask");
            await BlobCache.UserAccount.InsertObject("cachedAviaTask", selectedTask, TimeSpan.FromDays(7));

            Nations = NationsCollection.PlaneNations();
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
            await BlobCache.UserAccount.Invalidate("cachedAviaNations");
            await BlobCache.UserAccount.InsertObject("cachedAviaNations", selectedNations, TimeSpan.FromDays(7));

            Ranks = RanksCollection.PlaneRanks();
            selectedRanks = new ObservableCollection<ChipsItem>()
                {
                    Ranks[0],
                    Ranks[1],
                    Ranks[2],
                    Ranks[3],
                    Ranks[4],
                    Ranks[5],
                };
            await BlobCache.UserAccount.Invalidate("cachedAviaRanks");
            await BlobCache.UserAccount.InsertObject("cachedAviaRanks", selectedRanks, TimeSpan.FromDays(7));

            Roles = RolesCollection.PlaneRoles();
            selectedRoles = new ObservableCollection<ChipsItem>()
                {
                    Roles[0],
                    Roles[1],
                    Roles[2],
                };
            await BlobCache.UserAccount.Invalidate("cachedAviaRoles");
            await BlobCache.UserAccount.InsertObject("cachedAviaRoles", selectedRoles, TimeSpan.FromDays(7));

            GameTypes = GameTypeCollection.GameTypes();
            selectedGameTypes = new ObservableCollection<ChipsItem>()
                {
                    GameTypes[0],
                    GameTypes[1],
                    GameTypes[2],
                    GameTypes[3],
                };
            await BlobCache.UserAccount.Invalidate("cachedAviaGameTypes");
            await BlobCache.UserAccount.InsertObject("cachedAviaGameTypes", selectedGameTypes, TimeSpan.FromDays(7));

            selectedOrder = AppResources.Descending;
            await BlobCache.UserAccount.Invalidate("cachedAviaOrder");
            await BlobCache.UserAccount.InsertObject("cachedAviaOrder", selectedOrder, TimeSpan.FromDays(7));
        }
    }
}
