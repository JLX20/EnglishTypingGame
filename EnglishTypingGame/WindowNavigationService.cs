using System.ComponentModel;
using System.Windows;

namespace EnglishTypingGame
{
    public static class WindowNavigationService
    {
        private static bool _isNavigating;

        public static bool IsNavigating
        {
            get { return _isNavigating; }
        }

        public static void Navigate(Window currentWindow, Window nextWindow)
        {
            if (currentWindow == null || nextWindow == null)
                return;

            _isNavigating = true;

            Application.Current.MainWindow = nextWindow;
            nextWindow.Show();

            currentWindow.Close();

            _isNavigating = false;
        }

        public static void NavigateToMain(Window currentWindow)
        {
            Navigate(currentWindow, new MainWindow());
        }

        public static void HandleCloseToMain(Window currentWindow, CancelEventArgs e)
        {
            if (_isNavigating)
                return;

            if (currentWindow is MainWindow)
                return;

            e.Cancel = true;
            NavigateToMain(currentWindow);
        }

        public static void Shutdown()
        {
            _isNavigating = true;
            Application.Current.Shutdown();
        }
    }
}