using Windows.UI.Xaml.Controls;

namespace dl_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DownloaderViewPage : Page
    {
        public DownloaderViewPage()
        {
            this.InitializeComponent();
        }

        public CommandsViewModel Commands { get; } = CommandsViewModel.Instance;
    }
}
