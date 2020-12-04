using System;
using System.Collections.Generic;
using WTAnalyzer.ViewModels;
using WTAnalyzer.Views;
using Xamarin.Forms;

namespace WTAnalyzer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
