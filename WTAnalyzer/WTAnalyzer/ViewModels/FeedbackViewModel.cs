using Akavache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;
using WTAnalyzer.ViewModels.BaseViewModels;
using WTAnalyzer.Views;
using WTAnalyzer.Views.ServicePages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels
{
    public class FeedbackViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand RedditCommand { get; }
        public ICommand VKCommand { get; }
        public ICommand GMailCommand { get; }
        public ICommand HintsCommand { get; }
        public ICommand AboutAppCommand { get; }
        public ICommand PatreonCommand { get; }
        public ICommand App1Command { get; }
        public ICommand App2Command { get; }
        public ICommand Game1Command { get; }
        public ICommand Game2Command { get; }
        public ICommand UpdateCacheCommand { get; }

        public FeedbackViewModel(INavigation navigation)
        {
            Navigation = navigation;
            RedditCommand = new Command(RedditHandler);
            VKCommand = new Command(VKHandler);
            GMailCommand = new Command(GMailHandler);
            HintsCommand = new Command(HintsHandler);
            AboutAppCommand = new Command(AboutAppHandler);
            PatreonCommand = new Command(PatreonHandler);
            App1Command = new Command(App1Handler);
            App2Command = new Command(App2Handler);
            Game1Command = new Command(Game1Handler);
            Game2Command = new Command(Game2Handler);
            UpdateCacheCommand = new Command(UpdateCacheHandler);
        }

        private void RedditHandler(object obj)
        {
            Launcher.OpenAsync(new Uri("https://www.reddit.com/r/wave_app/"));
        }

        private void VKHandler(object obj)
        {
            Launcher.OpenAsync(new Uri("https://www.vk.com/wave_app/"));
        }

        private void GMailHandler(object obj)
        {
            Launcher.OpenAsync(new Uri("mailto:waveappfeedback@gmail.com"));
        }

        private async void HintsHandler(object obj)
        {
            if (Navigation.ModalStack.Count == 0)
            {               
                //await Navigation.PushAsync(new HintsPage());
            }
        }

        private async void AboutAppHandler(object obj)
        {
            if (Navigation.ModalStack.Count == 0)
            {
                //await Navigation.PushAsync(new AboutAppPage());
            }
        }

        private void PatreonHandler(object obj)
        {
            Launcher.OpenAsync(new Uri("https://www.patreon.com/wave_app"));
        }

        private void App1Handler(object obj)
        {
            Launcher.OpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.wave.wtversus"));
        }

        private void App2Handler(object obj)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                  Launcher.OpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.wave.skillmeter"));
                    break;
                case Device.iOS:
                    Launcher.OpenAsync(new Uri("https://apps.apple.com/us/app/id1542964380"));
                    break;
            }
        }

        private void Game1Handler(object obj)
        {
            Launcher.OpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.wave.wtquiz"));
        }

        private void Game2Handler(object obj)
        {
            Launcher.OpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.wave.wotquiz"));
        }

        private async void UpdateCacheHandler(object obj)
        {
            await BlobCache.UserAccount.Invalidate("cachedArrayOfPlanes");
            await BlobCache.UserAccount.Invalidate("cachedArrayOfTanks");
            await BlobCache.UserAccount.Invalidate("cachedArrayOfHelis");
            await BlobCache.UserAccount.Invalidate("cachedArrayOfShips");
            Process.GetCurrentProcess().Kill();
        }
    }
}
