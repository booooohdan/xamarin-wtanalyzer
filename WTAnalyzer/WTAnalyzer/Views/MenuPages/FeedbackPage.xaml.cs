﻿using Plugin.StoreReview;
using WTAnalyzer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WTAnalyzer.Views.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPage : ContentPage
    {
        public FeedbackPage()
        {
            InitializeComponent();
            BindingContext = new FeedbackViewModel(Navigation);
        }

        // RatingBar value change handler
        private async void SfRating_ValueChanged(object sender, Syncfusion.SfRating.XForms.ValueEventArgs e)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    CrossStoreReview.Current.OpenStoreReviewPage("com.wtwave.wtinsider");
                    break;
                case Device.iOS:
                    CrossStoreReview.Current.OpenStoreReviewPage(""/*"1542964380"*/);
                    break;
            }
            await CrossStoreReview.Current.RequestReview(false);
        }
    }
}