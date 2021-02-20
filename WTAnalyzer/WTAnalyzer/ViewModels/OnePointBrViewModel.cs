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
        private string id;
        private string name;
        private int vehicleValue;
        private string vehicleValueDesc;
        private string valueDifference;
        private int selectedTask;
        private ObservableCollection<string> tasks;
        public string Id
        {
            get => id;
            set
            {
                id = Uri.UnescapeDataString(value);
                OnPropertyChanged();
                SelectCorrectCollection();
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

        public int VehicleValue
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

        public string ValueDifference
        {
            get => valueDifference;
            set
            {
                valueDifference = value;
                OnPropertyChanged();
            }
        }

        public int SelectedTask
        {
            get => selectedTask;
            set
            {
                selectedTask = value;
                OnPropertyChanged();
                SetPlaneDataToListView();
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

        private void SelectCorrectCollection()
        {
            if (Id.StartsWith("1"))
            {
                Tasks = TasksCollection.PlaneTasks();
                SelectedTask = 2;
            }
            if (Id.StartsWith("2"))
            {
                Tasks = TasksCollection.TankTasks();
                SelectedTask = 0;
            }
            if (Id.StartsWith("3"))
            {
                Tasks = TasksCollection.HeliTasks();
                SelectedTask = 0;
            }
            if (Id.StartsWith("4"))
            {
                Tasks = TasksCollection.ShipTasks();
                SelectedTask = 0;
            }
        }

        private void SetPlaneDataToListView()
        {
            var planeListApi = arrayOfPlanes.PlanesListApi.ToList();
            var dataForListView = new ObservableCollection<ListViewItem>();

            Name = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Name;
            var nation = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).Nation;
            var BR = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).BR;
            var ID = Convert.ToDouble(Id);
            bool isAscending;

            var planevar = from p in planeListApi
                           where p.BR <= BR + 1.0 && p.BR >= BR - 1.0 && p.Nation != nation
                           orderby p.MaxSpeedAt0 descending
                           select p;
            var planes = planevar.ToList();



            foreach (var item in planes)
            {
                if (SelectedTask == 0) 
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).RepairCost.Value;
                    VehicleValueDesc = AppResources.SL;
                    ValueDifference = (item.RepairCost.Value - VehicleValue).ToString();
                    isAscending = true;

                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.RepairCost, AppResources.SL, item.BR, ValueDifference));
                }
                if (SelectedTask == 1) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeedAt0, AppResources.KmH, item.BR, item.Id)); }
                if (SelectedTask == 2) 
                {
                    VehicleValue = planeListApi.FirstOrDefault(x => x.VehicleId == Convert.ToInt32(Id)).MaxSpeedAt5000.Value;
                    VehicleValueDesc = AppResources.KmH;
                    ValueDifference = (item.MaxSpeedAt5000.Value - VehicleValue).ToString();
                    isAscending = true;
                    dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.MaxSpeedAt5000, AppResources.KmH, item.BR, ValueDifference)); 
                }
                if (SelectedTask == 3) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Climb, AppResources.S, item.BR, item.Id)); }
                if (SelectedTask == 4) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.TurnAt0, AppResources.S, item.BR, item.Id)); }
                if (SelectedTask == 5) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.EnginePower, "", item.BR, item.Id)); }
                if (SelectedTask == 6) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Weight, AppResources.Kg, item.BR, item.Id)); }
                if (SelectedTask == 7) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.Flutter, AppResources.KmH, item.BR, item.Id)); }
                if (SelectedTask == 8) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.OptimalAlitude, AppResources.M, item.BR, item.Id)); }
                if (SelectedTask == 9) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.BombLoad, AppResources.Kg, item.BR, item.Id)); }
                if (SelectedTask == 10) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.BurstMass, AppResources.KgS, item.BR, item.Id)); }
                if (SelectedTask == 11) { dataForListView.Add(new ListViewItem(item.Nation, item.Class, item.Type, item.Name, item.FirstFlyYear, AppResources.Y, item.BR, item.Id)); }

            }


            LeftListView = dataForListView.Where(x => Convert.ToInt32(x.Id) > 0).OrderByDescending(x=>x.Value).ToObservableCollection();
            RightListView = dataForListView.Where(x => Convert.ToInt32(x.Id) <= 0).OrderByDescending(x => x.Value).ToObservableCollection();


        }
    }
}