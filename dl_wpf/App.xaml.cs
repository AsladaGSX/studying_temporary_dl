using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace dl_wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Debug.WriteLine(
                $"{nameof(OnStartup)}",
                nameof(dl_wpf));

            _ = ConnectAsync();
        }

        private AppServiceConnection? _appServiceConnection;

        private async Task<bool> ConnectAsync()
        {
            if (_appServiceConnection != null)
            {
                return true;
            }

            var appServiceConnection = new AppServiceConnection
            {
                AppServiceName = "InProcessAppService",
                PackageFamilyName = Package.Current.Id.FamilyName
            };
            appServiceConnection.RequestReceived += AppServiceConnection_RequestReceived;
            appServiceConnection.ServiceClosed += AppServiceConnection_ServiceClosed;

            Task<AppServiceConnectionStatus> asyncOperation = appServiceConnection.OpenAsync().AsTask();

            if (await asyncOperation != AppServiceConnectionStatus.Success)
            {
                return false;
            }

            _appServiceConnection = appServiceConnection;
            return true;
        }

        private void AppServiceConnection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
            Debug.WriteLine(
                $"{nameof(AppServiceConnection.ServiceClosed)}: {nameof(AppServiceClosedEventArgs.Status)}={args.Status}",
                nameof(dl_wpf));

            // 切断されたので自殺
            _appServiceConnection = null;
            Current.Dispatcher.Invoke(() => Current.Shutdown());
        }

        private async void AppServiceConnection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            Debug.WriteLine(
                $"{nameof(AppServiceConnection.RequestReceived)}: {nameof(AppServiceRequestReceivedEventArgs.Request.Message)}={args.Request.Message}",
                nameof(dl_wpf));

            ValueSet msg = args.Request.Message;
            msg.Add("result", string.Empty);

            await args.Request.SendResponseAsync(msg);
        }
    }
}
