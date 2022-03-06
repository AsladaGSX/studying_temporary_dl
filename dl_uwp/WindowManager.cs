using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace dl_uwp
{
    internal static class WindowManager
    {
        public static IReadOnlyList<AppWindow> Views => views.AsReadOnly();

        private static readonly List<AppWindow> views = new List<AppWindow>();

        // Get a reference to the page instance and assign the
        internal static object GetFrameContent(this AppWindow appWindow) =>
            ElementCompositionPreview.GetAppWindowContent(appWindow) is Frame frame ? frame.Content : null;

        internal static async Task<AppWindow> CreateNewWindowAsync(Type type, object paramenter = null)
        {
            Frame frame = new Frame();
            bool navigated = frame.Navigate(type, paramenter);
            if (!navigated)
            {
                return null;
            }

            AppWindow newWindow = await AppWindow.TryCreateAsync();
            if (newWindow == null)
            {
                return null;
            }

            ElementCompositionPreview.SetAppWindowContent(newWindow, frame);

            // Register event handler.
            newWindow.Closed += NewWindow_Closed;
            void NewWindow_Closed(AppWindow sender, AppWindowClosedEventArgs args)
            {
                Debug.WriteLine($"AppWindow.Closed : Type='{type.Name}' Title='{newWindow.Title}'", nameof(dl_uwp));
                newWindow.Closed -= NewWindow_Closed;
                views.Remove(newWindow);
            };

            views.Add(newWindow);

            Debug.WriteLine($"new window created. Type='{type.Name}'", nameof(dl_uwp));

            // Show window.
            await newWindow.TryShowAsync();

            return newWindow;
        }
    }
}
