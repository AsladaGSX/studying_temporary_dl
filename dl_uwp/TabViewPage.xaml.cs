using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls;

namespace dl_uwp
{
    using muxc = Microsoft.UI.Xaml.Controls;

    public sealed partial class TabViewPage : Page
    {
        public TabViewPage()
        {
            this.InitializeComponent();
        }

        private void Tabs_AddTabButtonClick(TabView sender, object args)
        {
            Tabs.TabItems.Add(new TabViewItem()
            {
                IconSource = new muxc.SymbolIconSource
                {
                    Symbol = Symbol.Placeholder
                },
                Header = "New Item",
                Content = new TabContentView(),
            });
        }

        private void Tabs_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            Tabs.TabItems.Remove(args.Tab);
        }
    }
}
