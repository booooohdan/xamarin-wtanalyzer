using Akavache;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using WTAnalyzer.DataCollections;
using WTAnalyzer.Models;

namespace WTAnalyzer.Cache
{
    public class PlaneFilterDataSetter
    {
        private ObservableCollection<ChipsItem> selectedNations;
        private ObservableCollection<ChipsItem> selectedRanks;
        private ObservableCollection<ChipsItem> selectedRoles;
        private string selectedOrder;
        private string selectedTask;
        public ObservableCollection<ChipsItem> Ranks { get; set; }
        public ObservableCollection<ChipsItem> Nations { get; set; }
        public ObservableCollection<ChipsItem> Roles { get; set; }

        public async Task InitAsync()
        {
            selectedTask = "Repair Cost";
            await BlobCache.UserAccount.Invalidate("cachedSelectedTask");
            await BlobCache.UserAccount.InsertObject("cachedSelectedTask", selectedTask, TimeSpan.FromDays(7));

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
            await BlobCache.UserAccount.Invalidate("cachedSelectedNations");
            await BlobCache.UserAccount.InsertObject("cachedSelectedNations", selectedNations, TimeSpan.FromDays(7));

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
            await BlobCache.UserAccount.Invalidate("cachedSelectedRanks");
            await BlobCache.UserAccount.InsertObject("cachedSelectedRanks", selectedRanks, TimeSpan.FromDays(7));

            Roles = RolesCollection.PlaneRoles();
            selectedRoles = new ObservableCollection<ChipsItem>()
                {
                    Roles[0],
                    Roles[1],
                    Roles[2],
                };
            await BlobCache.UserAccount.Invalidate("cachedSelectedRoles");
            await BlobCache.UserAccount.InsertObject("cachedSelectedRoles", selectedRoles, TimeSpan.FromDays(7));

            selectedOrder = "Descending";
            await BlobCache.UserAccount.Invalidate("cachedSelectedOrder");
            await BlobCache.UserAccount.InsertObject("cachedSelectedOrder", selectedOrder, TimeSpan.FromDays(7));
        }
    }
}
