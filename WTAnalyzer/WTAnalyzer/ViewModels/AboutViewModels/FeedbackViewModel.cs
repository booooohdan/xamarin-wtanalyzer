using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WTAnalyzer.ViewModels.BaseViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WTAnalyzer.ViewModels.AboutViewModels
{
    public class FeedbackViewModel : BaseViewModel
    {
        public ICommand GmailCommand { get; }
        public ICommand RedditCommand { get; }
        public ICommand VkCommand { get; }
        public ICommand PatreonCommand { get; }
        public ICommand VersusCommand { get; }
        public ICommand SkillMeterCommand { get; }
        public ICommand WTQuizCommand { get; }
        public ICommand WoTQuizCommand { get; }

        public FeedbackViewModel()
        {
            GmailCommand = new Command(GmailHandler);
            RedditCommand = new Command(RedditHandler);
            VkCommand = new Command(VkHandler);
            PatreonCommand = new Command(PatreonHandler);
            VersusCommand = new Command(VersusHandler);
            SkillMeterCommand = new Command(SkillMeterHandler);
            WTQuizCommand = new Command(WtQuizHandler);
            WoTQuizCommand = new Command(WotQuizHandler);
        }
        private async void RedditHandler(object obj)
        {
            var uri = "https://www.reddit.com/r/wave_app/";
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        private async void VkHandler(object obj)
        {
            var uri = "https://www.vk.com/wave_app/";
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        private void GmailHandler(object obj)
        {
            Launcher.OpenAsync(new Uri("mailto:waveappfeedback@gmail.com"));
        }

        private async void PatreonHandler(object obj)
        {
            var uri = "https://www.patreon.com/wave_app";
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        private void VersusHandler(object obj)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    Launcher.OpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.wave.wtversus"));
                    break;
                case Device.iOS:
                    //Launcher.OpenAsync(new Uri("https://apps.apple.com/us/app/id1542964380"));
                    //TODO: Replace on AppStore id
                    break;
            }
        }

        private void SkillMeterHandler(object obj)
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

        private void WtQuizHandler(object obj)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    Launcher.OpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.wave.wtquiz"));
                    break;
                case Device.iOS:
                    //Launcher.OpenAsync(new Uri("https://apps.apple.com/us/app/id1542964380"));
                    //TODO: Replace on AppStore id
                    break;
            }
        }

        private void WotQuizHandler(object obj)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    Launcher.OpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.wave.wotquiz"));
                    break;
                case Device.iOS:
                    //Launcher.OpenAsync(new Uri("https://apps.apple.com/us/app/id1542964380"));
                    //TODO: Replace on AppStore id
                    break;
            }
        }
    }
}
