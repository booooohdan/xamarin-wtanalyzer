using MarcTron.Plugin;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WTAnalyzer.Helpers
{
    public static class AdmobIntersitials
    {
        public static void LoadIntersitialExplorer()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
#if(DEBUG)
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/8691691433"); //test id
#else
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-8211072909515345/6954166210");
#endif
                    break;
                case Device.iOS:
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-8211072909515345/2545248966");
                    break;
            }
        }

        public static void LoadIntersitialOnePointBr()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-8211072909515345/4400795695");
                    break;
                case Device.iOS:
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-8211072909515345/7606003955");
                    break;
            }
        }

        public static void LoadIntersitialBrChanger()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-8211072909515345/9710345584");
                    break;
                case Device.iOS:
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-8211072909515345/6676065660");
                    break;
            }
        }
    }
}
