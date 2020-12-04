using System;
using WTAnalyzer.Services;
using WTAnalyzer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WTAnalyzer
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
