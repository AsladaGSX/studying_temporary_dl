using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace dl_uwp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var dlwin = await WindowManager.CreateNewWindowAsync(typeof(DownloaderViewPage), null);
            dlwin.Title = "Downloader";

            await dlwin.TryShowAsync();

            await ApplicationView.GetForCurrentView().TryConsolidateAsync();
        }
    }
}
