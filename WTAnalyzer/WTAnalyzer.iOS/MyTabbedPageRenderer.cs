using CoreGraphics;
using System.ComponentModel;
using UIKit;
using WTAnalyzer;
using WTAnalyzer.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AppShell), typeof(MyTabbedPageRenderer))]
namespace WTAnalyzer.iOS
{
    public class MyTabbedPageRenderer : ShellRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            return new CustomTabbarAppearance();
        }
    }

    public class CustomTabbarAppearance : IShellTabBarAppearanceTracker
    {
        public void Dispose()
        {

        }

        public void ResetAppearance(UITabBarController controller)
        {

        }

        public void SetAppearance(UITabBarController controller, ShellAppearance appearance)
        {

        }

        public void UpdateLayout(UITabBarController controller)
        {

            foreach (UIViewController vc in controller.ViewControllers)
            {
                vc.TabBarItem.ImageInsets = new UIEdgeInsets(50, 50, 50, 50);
            }
        }
    }
}