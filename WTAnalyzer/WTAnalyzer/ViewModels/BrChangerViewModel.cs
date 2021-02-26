using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WTAnalyzer.DataCollections;
using WTAnalyzer.Helpers;
using WTAnalyzer.Models;
using WTAnalyzer.Resx;
using WTAnalyzer.ViewModels.BaseViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace WTAnalyzer.ViewModels
{
    [QueryProperty("Id", "id")]
    public class BrChangerViewModel : BaseViewModel
    {
        #region Define variables

        private string id;
        private string name;
        private double currentBr;
        private int newBr;
        private double[] brArray;
        private bool isMainVisible;
        private bool isPlaceHolderVisible;
        #endregion

        #region View Properties
        public string Id
        {
            get => id;
            set
            {
                id = Uri.UnescapeDataString(value);
                OnPropertyChanged();
                SelectCorrectBr();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public double CurrentBr
        {
            get => currentBr;
            set
            {
                currentBr = value;
                OnPropertyChanged();
            }
        }

        public int NewBr
        {
            get => newBr;
            set
            {
                if (value >= 0)
                {
                    newBr = value;
                    OnPropertyChanged();
                    ChooseWhichDataSetToListView();
                }
            }
        }

        public double[] BrArray
        {
            get => brArray;
            set
            {
                brArray = value;
                OnPropertyChanged();
            }
        }

        public bool IsMainVisible
        {
            get => isMainVisible;
            set
            {
                isMainVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsPlaceHolderVisible
        {
            get => isPlaceHolderVisible;
            set
            {
                isPlaceHolderVisible = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public BrChangerViewModel()
        {
            IsPlaceHolderVisible = true;
            IsMainVisible = false;

            MessagingCenter.Subscribe<VehiclesSearchHandler, string>(this, "searchResult",
                (sender, arg) =>
                {
                    if (arg != null)
                    {
                        Id = arg;
                    }
                });
        }

        private void SelectCorrectBr()
        {
            double brValue = 0;

            if (Id.StartsWith("1"))
            {
                BrArray = BRArray.PlanesBR();
                var lists = arrayOfPlanes.PlanesListApi.ToList();
                brValue = lists.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR.Value;
            }

            if (Id.StartsWith("2"))
            {
                BrArray = BRArray.TanksBR();
                var lists = arrayOfTanks.TanksListApi.ToList();
                brValue = lists.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR.Value;
            }

            if (Id.StartsWith("3"))
            {
                BrArray = BRArray.HelisBR();
                var lists = arrayOfHelis.HelisListApi.ToList();
                brValue = lists.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR.Value;
            }

            if (Id.StartsWith("4"))
            {
                BrArray = BRArray.ShipsBR();
                var lists = arrayOfShips.ShipsListApi.ToList();
                brValue = lists.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR.Value;
            }

            NewBr = brValue != 0 ? BRArray.PlanesBR().IndexOf(brValue) - 1 : BRArray.PlanesBR().IndexOf(brValue);
        }

        private void ChooseWhichDataSetToListView()
        {
            IsPlaceHolderVisible = false;
            IsMainVisible = true;

            if (Id.StartsWith("1"))
            {
                SetPlaneDataToListView();
            }
            if (Id.StartsWith("2"))
            {
                SetTankDataToListView();
            }
            if (Id.StartsWith("3"))
            {
                SetHeliDataToListView();
            }
            if (Id.StartsWith("4"))
            {
                SetShipDataToListView();
            }
        }

        private void SetPlaneDataToListView()
        {
            #region Define local variables
            var planeListApi = arrayOfPlanes.PlanesListApi.ToList();
            var nation = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Nation;

            Name = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Name; //Visual binding
            CurrentBr = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR.Value; //Visual binding
            #endregion

            #region Linq queries

            var currentVar = from p in planeListApi
                             where p.BR <= CurrentBr + 1.1 && p.BR >= CurrentBr - 1.1 && p.Nation != nation
                             orderby p.MaxSpeedAt0 descending
                             select p;
            var currentRange = currentVar.ToList();

            var newVar = from p in planeListApi
                         where p.BR <= BrArray[NewBr] + 1.1 && p.BR >= BrArray[NewBr] - 1.1 && p.Nation != nation
                         orderby p.MaxSpeedAt0 descending
                         select p;
            var newRange = newVar.ToList();


            var InList1ButNotList2 = currentRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })
                .Except(newRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })).ToList();

            var InList2ButNotList1 = newRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })
                .Except(currentRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })).ToList();
            #endregion

            var leftExceptList = new ObservableCollection<ListViewItem>();
            var rightExceptList = new ObservableCollection<ListViewItem>();

            foreach (var item in InList2ButNotList1)
            {
                leftExceptList.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, "", "", item.BR, item.VehicleId));
            }

            foreach (var item in InList1ButNotList2)
            {
                rightExceptList.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, "", "", item.BR, item.VehicleId));
            }

            LeftListView = leftExceptList;
            RightListView = rightExceptList;
        }

        private void SetTankDataToListView()
        {
            #region Define local variables
            var tankListApi = arrayOfTanks.TanksListApi.ToList();
            var nation = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Nation;

            Name = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Name; //Visual binding
            CurrentBr = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR.Value; //Visual binding
            #endregion

            #region Linq queries

            var currentVar = from p in tankListApi
                             where p.BR <= CurrentBr + 1.1 && p.BR >= CurrentBr - 1.1 && p.Nation != nation
                             orderby p.Penetration descending
                             select p;
            var currentRange = currentVar.ToList();

            var newVar = from p in tankListApi
                         where p.BR <= BrArray[NewBr] + 1.1 && p.BR >= BrArray[NewBr] - 1.1 && p.Nation != nation
                         orderby p.Penetration descending
                         select p;
            var newRange = newVar.ToList();


            var InList1ButNotList2 = currentRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })
                .Except(newRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })).ToList();

            var InList2ButNotList1 = newRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })
                .Except(currentRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })).ToList();
            #endregion

            var leftExceptList = new ObservableCollection<ListViewItem>();
            var rightExceptList = new ObservableCollection<ListViewItem>();

            foreach (var item in InList2ButNotList1)
            {
                leftExceptList.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, "", "", item.BR, item.VehicleId));
            }

            foreach (var item in InList1ButNotList2)
            {
                rightExceptList.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, "", "", item.BR, item.VehicleId));
            }

            LeftListView = leftExceptList;
            RightListView = rightExceptList;
        }

        private void SetHeliDataToListView()
        {
            #region Define local variables
            var heliListApi = arrayOfHelis.HelisListApi.ToList();
            var nation = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Nation;

            Name = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Name; //Visual binding
            CurrentBr = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR.Value; //Visual binding
            #endregion

            #region Linq queries

            var currentVar = from p in heliListApi
                             where p.BR <= CurrentBr + 1.1 && p.BR >= CurrentBr - 1.1 && p.Nation != nation
                             orderby p.AGMCount descending
                             select p;
            var currentRange = currentVar.ToList();

            var newVar = from p in heliListApi
                         where p.BR <= BrArray[NewBr] + 1.1 && p.BR >= BrArray[NewBr] - 1.1 && p.Nation != nation
                         orderby p.AGMCount descending
                         select p;
            var newRange = newVar.ToList();


            var InList1ButNotList2 = currentRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })
                .Except(newRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })).ToList();

            var InList2ButNotList1 = newRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })
                .Except(currentRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })).ToList();
            #endregion

            var leftExceptList = new ObservableCollection<ListViewItem>();
            var rightExceptList = new ObservableCollection<ListViewItem>();

            foreach (var item in InList2ButNotList1)
            {
                leftExceptList.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, "", "", item.BR, item.VehicleId));
            }

            foreach (var item in InList1ButNotList2)
            {
                rightExceptList.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, "", "", item.BR, item.VehicleId));
            }

            LeftListView = leftExceptList;
            RightListView = rightExceptList;
        }

        private void SetShipDataToListView()
        {
            #region Define local variables
            var shipListApi = arrayOfShips.ShipsListApi.ToList();
            var nation = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Nation;

            Name = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Name; //Visual binding
            CurrentBr = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR.Value; //Visual binding
            #endregion

            #region Linq queries

            var currentVar = from p in shipListApi
                             where p.BR <= CurrentBr + 1.1 && p.BR >= CurrentBr - 1.1 && p.Nation != nation
                             orderby p.MainCaliberSize descending
                             select p;
            var currentRange = currentVar.ToList();

            var newVar = from p in shipListApi
                         where p.BR <= BrArray[NewBr] + 1.1 && p.BR >= BrArray[NewBr] - 1.1 && p.Nation != nation
                         orderby p.MainCaliberSize descending
                         select p;
            var newRange = newVar.ToList();


            var InList1ButNotList2 = currentRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })
                .Except(newRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })).ToList();

            var InList2ButNotList1 = newRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })
                .Except(currentRange.Select(x => new { x.Nation, x.Class, x.Type, x.Name, x.BR, x.VehicleId })).ToList();
            #endregion

            var leftExceptList = new ObservableCollection<ListViewItem>();
            var rightExceptList = new ObservableCollection<ListViewItem>();

            foreach (var item in InList2ButNotList1)
            {
                leftExceptList.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, "", "", item.BR, item.VehicleId));
            }

            foreach (var item in InList1ButNotList2)
            {
                rightExceptList.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, "", "", item.BR, item.VehicleId));
            }

            LeftListView = leftExceptList;
            RightListView = rightExceptList;
        }
    }
}
