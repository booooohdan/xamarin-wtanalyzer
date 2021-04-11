using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace WTAnalyzer.iOS
{
    public class AppTracking : IAppTracking
    {
        public Task<bool> IsAuthorized()
        {
            return Task.FromResult(GetAppTrackingStatus());
        }
        private bool GetAppTrackingStatus()
        {
            var status = AppTrackingTransparency.ATTrackingManager.TrackingAuthorizationStatus;
            return status switch
            {
                AppTrackingTransparency.ATTrackingManagerAuthorizationStatus.Authorized => true,
                _ => false,
            };
        }
        public Task<bool> RequestAuthorizationAsync()
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            try
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(14, 0))
                {
                    AppTrackingTransparency.ATTrackingManager.RequestTrackingAuthorization((result) =>
                    {
                        if (result == AppTrackingTransparency.ATTrackingManagerAuthorizationStatus.Authorized)
                            taskCompletionSource.SetResult(true);
                        else
                            taskCompletionSource.SetResult(false);
                    });
                }
                else
                    taskCompletionSource.SetResult(true);
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }
            return taskCompletionSource.Task;
        }
    }
}