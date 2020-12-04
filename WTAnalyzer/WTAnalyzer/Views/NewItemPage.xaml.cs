using System;
using System.Collections.Generic;
using System.ComponentModel;
using WTAnalyzer.Models;
using WTAnalyzer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WTAnalyzer.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}