using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WTAnalyzer.DataCollections;
using WTAnalyzer.Helpers;
using WTAnalyzer.Models;
using WTAnalyzer.Resx;
using WTAnalyzer.ViewModels.BaseViewModels;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    [QueryProperty("Id", "id")]
    public class OnePointBrViewModel : BaseViewModel
    {
        #region Define variables

        private string id;
        private string name;
        private double vehicleValue;
        private string vehicleValueDesc;
        private string ValueDifference;
        private int selectedTask;
        private ObservableCollection<string> tasks;
        #endregion

        #region View Properties
        public string Id
        {
            get => id;
            set
            {
                id = Uri.UnescapeDataString(value);
                OnPropertyChanged();
                SelectCorrectTasks();
            }
        }
        public int SelectedTask
        {
            get => selectedTask;
            set
            {
                selectedTask = value;
                OnPropertyChanged();
                ChooseWhichDataSetToListView();
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

        public double VehicleValue
        {
            get => vehicleValue;
            set
            {
                vehicleValue = value;
                OnPropertyChanged();
            }
        }

        public string VehicleValueDesc
        {
            get => vehicleValueDesc;
            set
            {
                vehicleValueDesc = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Tasks
        {
            get => tasks;
            set
            {
                tasks = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public OnePointBrViewModel()
        {
            MessagingCenter.Subscribe<VehiclesSearchHandler, string>(this, "searchResult",
                (sender, arg) => {
                    if (arg != null)
                    {
                        Id = arg;
                    }
                });
        }

        private void SelectCorrectTasks()
        {
            if (Id.StartsWith("1"))
            {
                Tasks = TasksCollection.PlaneTasks();
                SelectedTask = 2;
            }
            if (Id.StartsWith("2"))
            {
                Tasks = TasksCollection.TankTasks();
                SelectedTask = 1;
            }
            if (Id.StartsWith("3"))
            {
                Tasks = TasksCollection.HeliTasks();
                SelectedTask = 4;
            }
            if (Id.StartsWith("4"))
            {
                Tasks = TasksCollection.ShipTasks();
                SelectedTask = 0;
            }
        }

        private void ChooseWhichDataSetToListView()
        {
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
            var planeListApi = arrayOfPlanes.PlanesListApi.ToList();
            var dataForListView = new ObservableCollection<ListViewItem>();
            bool moreIsBetter = true;
            //Collection init

            Name = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Name; //Visual binding

            var nation = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Nation;
            var BR = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR;
            //Needed for linq query

            var planevar = from p in planeListApi
                           where p.BR <= BR + 1.0 && p.BR >= BR - 1.0 && p.Nation != nation
                           select p;
            var planes = planevar.ToList();


            foreach (var item in planes)
            {
                if (SelectedTask == 0)
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).RepairCost.Value;
                    VehicleValueDesc = AppResources.SL;
                    ValueDifference = (item.RepairCost.Value - VehicleValue).ToString();
                    moreIsBetter = false;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.RepairCost, AppResources.SL, item.BR, ValueDifference));
                }
                if (SelectedTask == 1)
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).MaxSpeedAt0.Value;
                    VehicleValueDesc = AppResources.KmH;
                    ValueDifference = (item.MaxSpeedAt0.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeedAt0, AppResources.KmH, item.BR, ValueDifference));
                }
                if (SelectedTask == 2) 
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).MaxSpeedAt5000.Value;
                    VehicleValueDesc = AppResources.KmH;
                    ValueDifference = (item.MaxSpeedAt5000.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeedAt5000, AppResources.KmH, item.BR, ValueDifference)); 
                }

                if (SelectedTask == 3)
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).Climb.Value;
                    VehicleValueDesc = AppResources.S;
                    ValueDifference = (item.Climb.Value - VehicleValue).ToString();
                    moreIsBetter = false;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Climb, AppResources.S, item.BR, ValueDifference));
                }

                if (SelectedTask == 4)
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).TurnAt0.Value;
                    VehicleValueDesc = AppResources.S;
                    ValueDifference = (Math.Round(item.TurnAt0.Value - VehicleValue, 2)).ToString();
                    moreIsBetter = false;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.TurnAt0, AppResources.S, item.BR, ValueDifference));
                }

                if (SelectedTask == 5)
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).EnginePower.Value;
                    VehicleValueDesc = "";
                    ValueDifference = (item.EnginePower.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.EnginePower, "", item.BR, ValueDifference));
                }

                if (SelectedTask == 6)
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).Weight.Value;
                    VehicleValueDesc = AppResources.Kg;
                    ValueDifference = (item.Weight.Value - VehicleValue).ToString();
                    moreIsBetter = false;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Weight, AppResources.Kg, item.BR, ValueDifference));
                }

                if (SelectedTask == 7)
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).Flutter.Value;
                    VehicleValueDesc = AppResources.KmH;
                    ValueDifference = (item.Flutter.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Flutter, AppResources.KmH, item.BR, ValueDifference));
                }


                if (SelectedTask == 8)
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).BombLoad.Value;
                    VehicleValueDesc = AppResources.Kg;
                    ValueDifference = (item.BombLoad.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.BombLoad, AppResources.Kg, item.BR, ValueDifference));
                }

                if (SelectedTask == 9)
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).BurstMass.Value;
                    VehicleValueDesc = AppResources.KgS;
                    ValueDifference = (Math.Round(item.BurstMass.Value - VehicleValue, 2)).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.BurstMass, AppResources.KgS, item.BR, ValueDifference));
                }

                if (SelectedTask == 10)
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).FirstFlyYear.Value;
                    VehicleValueDesc = AppResources.Y;
                    ValueDifference = (item.FirstFlyYear.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.FirstFlyYear, AppResources.Y, item.BR, ValueDifference));
                }
            }


            if (moreIsBetter)
            {
                LeftListView = dataForListView.Where(x => Convert.ToDouble(x.Id) > 0).OrderByDescending(x => x.Value).ToObservableCollection();
                RightListView = dataForListView.Where(x => Convert.ToDouble(x.Id) <= 0).OrderByDescending(x => x.Value).ToObservableCollection();
            }
            else
            {
                LeftListView = dataForListView.Where(x => Convert.ToDouble(x.Id) < 0).OrderBy(x => x.Value).ToObservableCollection();
                RightListView = dataForListView.Where(x => Convert.ToDouble(x.Id) >= 0).OrderBy(x => x.Value).ToObservableCollection();

            }
        }

        private void SetTankDataToListView()
        {
            var tankListApi = arrayOfTanks.TanksListApi.ToList();
            var dataForListView = new ObservableCollection<ListViewItem>();
            bool moreIsBetter = true;
            //Collection init

            Name = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Name; //Visual binding

            var nation = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Nation;
            var BR = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR;
            //Needed for linq query

            var tankvar = from p in tankListApi
                           where p.BR <= BR + 1.0 && p.BR >= BR - 1.0 && p.Nation != nation
                           select p;
            var tanks = tankvar.ToList();


            foreach (var item in tanks)
            {
                if (SelectedTask == 0)
                {
                    VehicleValue = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).RepairCost.Value;
                    VehicleValueDesc = AppResources.SL;
                    ValueDifference = (item.RepairCost.Value - VehicleValue).ToString();
                    moreIsBetter = false;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.RepairCost, AppResources.SL, item.BR, ValueDifference));
                }

                if (SelectedTask == 1)
                {
                    VehicleValue = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).MaxSpeedAtTerrain.Value;
                    VehicleValueDesc = AppResources.KmH;
                    ValueDifference = (item.MaxSpeedAtTerrain.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeedAtTerrain, AppResources.KmH, item.BR, ValueDifference));
                }

                if (SelectedTask == 2)
                {
                    VehicleValue = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).MaxSpeedAtRoad.Value;
                    VehicleValueDesc = AppResources.KmH;
                    ValueDifference = (item.MaxSpeedAtRoad.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeedAtRoad, AppResources.KmH, item.BR, ValueDifference));
                }

                if (SelectedTask == 3)
                {
                    VehicleValue = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).MaxReverseSpeed.Value;
                    VehicleValueDesc = AppResources.KmH;
                    ValueDifference = (item.MaxReverseSpeed.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxReverseSpeed, AppResources.KmH, item.BR, ValueDifference));
                }

                if (SelectedTask == 4)
                {
                    VehicleValue = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).EnginePower.Value;
                    VehicleValueDesc = "";
                    ValueDifference = (item.EnginePower.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.EnginePower, "", item.BR, ValueDifference));
                }

                if (SelectedTask == 5)
                {
                    VehicleValue = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).Weight.Value;
                    VehicleValueDesc = AppResources.Kg;
                    ValueDifference = (Math.Round(item.Weight.Value - VehicleValue, 2)).ToString();
                    moreIsBetter = false;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Weight, AppResources.Kg, item.BR, ValueDifference));
                }

                if (SelectedTask == 6)
                {
                    VehicleValue = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).Penetration.Value;
                    VehicleValueDesc = AppResources.Mm;
                    ValueDifference = (item.Penetration.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Penetration, AppResources.Mm, item.BR, ValueDifference));
                }


                if (SelectedTask == 7)
                {
                    VehicleValue = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).ReloadTime.Value;
                    VehicleValueDesc = AppResources.S;
                    ValueDifference = (Math.Round(item.ReloadTime.Value - VehicleValue, 2)).ToString();
                    moreIsBetter = false;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ReloadTime, AppResources.S, item.BR, ValueDifference));
                }

                if (SelectedTask == 8)
                {
                    VehicleValue = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).ReducedArmorFrontTurret.Value;
                    VehicleValueDesc = AppResources.Mm;
                    ValueDifference = (Math.Round(item.ReducedArmorFrontTurret.Value - VehicleValue, 2)).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ReducedArmorFrontTurret, AppResources.Mm, item.BR, ValueDifference));
                }

                if (SelectedTask == 9)
                {
                    VehicleValue = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).ReducedArmorTopSheet.Value;
                    VehicleValueDesc = AppResources.Mm;
                    ValueDifference = (item.ReducedArmorTopSheet.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ReducedArmorTopSheet, AppResources.Mm, item.BR, ValueDifference));
                }

                if (SelectedTask == 10)
                {
                    VehicleValue = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).ReducedArmorBottomSheet.Value;
                    VehicleValueDesc = AppResources.Mm;
                    ValueDifference = (item.ReducedArmorBottomSheet.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ReducedArmorBottomSheet, AppResources.Mm, item.BR, ValueDifference));
                }

                if (SelectedTask == 11)
                {
                    VehicleValue = tankListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).FirstRideYear.Value;
                    VehicleValueDesc = AppResources.Y;
                    ValueDifference = (item.FirstRideYear.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.FirstRideYear, AppResources.Y, item.BR, ValueDifference));
                }
            }


            if (moreIsBetter)
            {
                LeftListView = dataForListView.Where(x => Convert.ToDouble(x.Id) > 0).OrderByDescending(x => x.Value).ToObservableCollection();
                RightListView = dataForListView.Where(x => Convert.ToDouble(x.Id) <= 0).OrderByDescending(x => x.Value).ToObservableCollection();
            }
            else
            {
                LeftListView = dataForListView.Where(x => Convert.ToDouble(x.Id) < 0).OrderBy(x => x.Value).ToObservableCollection();
                RightListView = dataForListView.Where(x => Convert.ToDouble(x.Id) >= 0).OrderBy(x => x.Value).ToObservableCollection();

            }
        }

        private void SetHeliDataToListView()
        {
            var heliListApi = arrayOfHelis.HelisListApi.ToList();
            var dataForListView = new ObservableCollection<ListViewItem>();
            bool moreIsBetter = true;
            //Collection init

            Name = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Name; //Visual binding

            var nation = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Nation;
            var BR = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR;
            //Needed for linq query

            var helivar = from p in heliListApi
                          where p.BR <= BR + 1.0 && p.BR >= BR - 1.0 && p.Nation != nation
                          select p;
            var helis = helivar.ToList();


            foreach (var item in helis)
            {
                if (SelectedTask == 0)
                {
                    VehicleValue = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).RepairCost.Value;
                    VehicleValueDesc = AppResources.SL;
                    ValueDifference = (item.RepairCost.Value - VehicleValue).ToString();
                    moreIsBetter = false;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.RepairCost, AppResources.SL, item.BR, ValueDifference));
                }

                if (SelectedTask == 1)
                {
                    VehicleValue = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).MaxSpeed.Value;
                    VehicleValueDesc = AppResources.KmH;
                    ValueDifference = (item.MaxSpeed.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeed, AppResources.KmH, item.BR, ValueDifference));
                }

                if (SelectedTask == 2)
                {
                    VehicleValue = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).ClimbTo1000.Value;
                    VehicleValueDesc = AppResources.S;
                    ValueDifference = (item.ClimbTo1000.Value - VehicleValue).ToString();
                    moreIsBetter = false;


                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ClimbTo1000, AppResources.S, item.BR, ValueDifference));
                }               

                if (SelectedTask == 3)
                {
                    VehicleValue = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).Weight.Value;
                    VehicleValueDesc = AppResources.Kg;
                    ValueDifference = (Math.Round(item.Weight.Value - VehicleValue, 2)).ToString();
                    moreIsBetter = false;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Weight, AppResources.Kg, item.BR, ValueDifference));
                }

                if (SelectedTask == 4)
                {
                    VehicleValue = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).AGMCount.Value;
                    VehicleValueDesc = AppResources.Item;
                    ValueDifference = (item.AGMCount.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.AGMCount, AppResources.Item, item.BR, ValueDifference));
                }

                if (SelectedTask == 5)
                {
                    VehicleValue = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).AGMRange.Value;
                    VehicleValueDesc = AppResources.M;
                    ValueDifference = (item.AGMRange.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.AGMRange, AppResources.M, item.BR, ValueDifference));
                }

                if (SelectedTask == 6)
                {
                    VehicleValue = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).ASMCount.Value;
                    VehicleValueDesc = AppResources.Item;
                    ValueDifference = (item.ASMCount.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ASMCount, AppResources.Item, item.BR, ValueDifference));
                }

                if (SelectedTask == 7)
                {
                    VehicleValue = heliListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).FirstFlyYear.Value;
                    VehicleValueDesc = AppResources.Y;
                    ValueDifference = (item.FirstFlyYear.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.FirstFlyYear, AppResources.Y, item.BR, ValueDifference));
                }
            }


            if (moreIsBetter)
            {
                LeftListView = dataForListView.Where(x => Convert.ToDouble(x.Id) > 0).OrderByDescending(x => x.Value).ToObservableCollection();
                RightListView = dataForListView.Where(x => Convert.ToDouble(x.Id) <= 0).OrderByDescending(x => x.Value).ToObservableCollection();
            }
            else
            {
                LeftListView = dataForListView.Where(x => Convert.ToDouble(x.Id) < 0).OrderBy(x => x.Value).ToObservableCollection();
                RightListView = dataForListView.Where(x => Convert.ToDouble(x.Id) >= 0).OrderBy(x => x.Value).ToObservableCollection();

            }
        }

        private void SetShipDataToListView()
        {
            var shipListApi = arrayOfShips.ShipsListApi.ToList();
            var dataForListView = new ObservableCollection<ListViewItem>();
            bool moreIsBetter = true;
            //Collection init

            Name = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Name; //Visual binding

            var nation = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Nation;
            var BR = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR;
            //Needed for linq query

            var shipvar = from p in shipListApi
                          where p.BR <= BR + 1.0 && p.BR >= BR - 1.0 && p.Nation != nation
                          select p;
            var ships = shipvar.ToList();


            foreach (var item in ships)
            {
                if (SelectedTask == 0)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).RepairCost.Value;
                    VehicleValueDesc = AppResources.SL;
                    ValueDifference = (item.RepairCost.Value - VehicleValue).ToString();
                    moreIsBetter = false;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.RepairCost, AppResources.SL, item.BR, ValueDifference));
                }

                if (SelectedTask == 1)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).MainCaliberSize.Value;
                    VehicleValueDesc = AppResources.Mm;
                    ValueDifference = (item.MainCaliberSize.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MainCaliberSize, AppResources.Mm, item.BR, ValueDifference));
                }

                if (SelectedTask == 2)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).MainCaliberReload.Value;
                    VehicleValueDesc = AppResources.S;
                    ValueDifference = (Math.Round(item.MainCaliberReload.Value - VehicleValue, 2)).ToString();
                    moreIsBetter = false;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MainCaliberReload, AppResources.S, item.BR, ValueDifference));
                }

                if (SelectedTask == 3)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).TorpedoItem.Value;
                    VehicleValueDesc = AppResources.Item;
                    ValueDifference = (item.TorpedoItem.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.TorpedoItem, AppResources.Item, item.BR, ValueDifference));
                }

                if (SelectedTask == 4)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).TorpedoMaxSpeed.Value;
                    VehicleValueDesc = AppResources.KmH;
                    ValueDifference = (item.TorpedoMaxSpeed.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.TorpedoMaxSpeed, AppResources.KmH, item.BR, ValueDifference));
                }

                if (SelectedTask == 5)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).TorpedoTNT.Value;
                    VehicleValueDesc = AppResources.Kg;
                    ValueDifference = (Math.Round(item.TorpedoTNT.Value - VehicleValue, 2)).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.TorpedoTNT, AppResources.Kg, item.BR, ValueDifference));
                }

                if (SelectedTask == 6)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).MaxSpeed.Value;
                    VehicleValueDesc = AppResources.KmH;
                    ValueDifference = (item.MaxSpeed.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeed, AppResources.Mm, item.BR, ValueDifference));
                }

                if (SelectedTask == 7)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).Turn360.Value;
                    VehicleValueDesc = AppResources.S;
                    ValueDifference = (item.Turn360.Value - VehicleValue).ToString();
                    moreIsBetter = false;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Turn360, AppResources.S, item.BR, ValueDifference));
                }

                if (SelectedTask == 8)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).ArmorTower.Value;
                    VehicleValueDesc = AppResources.Mm;
                    ValueDifference = (Math.Round(item.ArmorTower.Value - VehicleValue, 2)).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ArmorTower, AppResources.Mm, item.BR, ValueDifference));
                }

                if (SelectedTask == 9)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).ArmorHull.Value;
                    VehicleValueDesc = AppResources.Mm;
                    ValueDifference = (item.ArmorHull.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.ArmorHull, AppResources.Mm, item.BR, ValueDifference));
                }

                if (SelectedTask == 10)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).CrewCount.Value;
                    VehicleValueDesc = AppResources.Item;
                    ValueDifference = (item.CrewCount.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.CrewCount, AppResources.Item, item.BR, ValueDifference));
                }

                if (SelectedTask == 11)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).Displacement.Value;
                    VehicleValueDesc = AppResources.T;
                    ValueDifference = (item.Displacement.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Displacement, AppResources.T, item.BR, ValueDifference));
                }

                if (SelectedTask == 12)
                {
                    VehicleValue = shipListApi.FirstOrDefault(x => x.VehicleId == Convert.ToDouble(Id)).FirstLaunchYear.Value;
                    VehicleValueDesc = AppResources.Y;
                    ValueDifference = (item.FirstLaunchYear.Value - VehicleValue).ToString();

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.FirstLaunchYear, AppResources.Y, item.BR, ValueDifference));
                }
            }


            if (moreIsBetter)
            {
                LeftListView = dataForListView.Where(x => Convert.ToDouble(x.Id) > 0).OrderByDescending(x => x.Value).ToObservableCollection();
                RightListView = dataForListView.Where(x => Convert.ToDouble(x.Id) <= 0).OrderByDescending(x => x.Value).ToObservableCollection();
            }
            else
            {
                LeftListView = dataForListView.Where(x => Convert.ToDouble(x.Id) < 0).OrderBy(x => x.Value).ToObservableCollection();
                RightListView = dataForListView.Where(x => Convert.ToDouble(x.Id) >= 0).OrderBy(x => x.Value).ToObservableCollection();

            }
        }
    }
}