using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WTAnalyzer.Models;
using WTAnalyzer.Resx;

namespace WTAnalyzer.DataCollections
{
    public static class OrderCollection
    {
        public static ObservableCollection<string> Order() =>
            new ObservableCollection<string>()
                {
                     AppResources.Ascending,
                     AppResources.Descending
                };
    }
}
