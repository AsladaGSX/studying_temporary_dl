using System;
using System.Linq;
using System.Threading.Tasks;

namespace dl_uwp
{
    public sealed class CommandsViewModel : MVVM.ViewModelBase
    {
        public static CommandsViewModel Instance { get; } = new CommandsViewModel();

        public RelayCommand NewBrowserCommand { get; }
        public RelayCommand CloseAllBrowsersCommand { get; }

        private CommandsViewModel()
        {
            NewBrowserCommand = new RelayCommand(NewBrowserCommand_Invoked);
            CloseAllBrowsersCommand = new RelayCommand(CloseAllBrowsersCommand_Invoked);
        }

        private async void NewBrowserCommand_Invoked(object parameter)
        {
            var viewControl = await WindowManager.CreateNewWindowAsync(typeof(TabViewPage));

            viewControl.Title = "Secondary";
        }

        private async void CloseAllBrowsersCommand_Invoked(object parameter)
        {
            await Task.WhenAll(WindowManager.Views
                .Where(_ => _.GetFrameContent() is TabViewPage)
                .Select(_ => _.CloseAsync().AsTask()));
        }
    }
}
