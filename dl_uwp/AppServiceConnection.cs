using System.Diagnostics;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;

namespace dl_uwp
{
    // AppServiceConnection に関する動作を記述します。
    sealed partial class App
    {
        private BackgroundTaskDeferral _appServiceDeferral;
        private AppServiceConnection _appServiceConnection;

        protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            Debug.WriteLine(nameof(OnBackgroundActivated), nameof(dl_uwp));
            base.OnBackgroundActivated(args);

            if (args.TaskInstance.TriggerDetails is AppServiceTriggerDetails appService)
            {
                Debug.WriteLine($"AppService Triggerd: Name={appService.Name}", nameof(dl_uwp));

                // 保存しておかないと、サービスが一瞬で終了してしまう。
                _appServiceDeferral = args.TaskInstance.GetDeferral();
                // args.TaskInstance.Canceled += TaskInstance_Canceled; // for Windows 8.x App only. this is Desktop.

                _appServiceConnection = appService.AppServiceConnection;
                //_appServiceConnection.RequestReceived += AppServiceConnection_RequestReceived;
                //_appServiceConnection.ServiceClosed += AppServiceConnection_ServiceClosed;
            }
        }
    }
}
