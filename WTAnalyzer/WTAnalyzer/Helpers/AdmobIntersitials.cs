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
                    //CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/8691691433"); //test id
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-8211072909515345/1155126777");
                    break;
                case Device.iOS:
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-8211072909515345/3733188651");
                    break;
            }
        }

        public static void LoadIntersitialOnePointBr()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-8211072909515345/4214736085");
                    break;
                case Device.iOS:
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-8211072909515345/6495083365");
                    break;
            }
        }

        public static void LoadIntersitialBrChanger()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-8211072909515345/7160657144");
                    break;
                case Device.iOS:
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-8211072909515345/7651132803");
                    break;
            }
        }
    }
}
