using System.ComponentModel;
using System.Windows;

namespace EnglishTypingGame
{
    public static class WindowNavigationService
    {
        private static bool _isNavigating;
        private static bool _isShuttingDown;

        public static bool IsNavigating
        {
            get { return _isNavigating; }
        }

        public static bool IsShuttingDown
        {
            get { return _isShuttingDown; }
        }

        public static void Navigate(Window currentWindow, Window nextWindow)
        {
            if (currentWindow == null || nextWindow == null)
                return;

            if (_isShuttingDown)
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
            if (_isNavigating || _isShuttingDown)
                return;

            _isShuttingDown = true;
            e.Cancel = false;
            Application.Current.Shutdown();
        }

        public static void Shutdown()
        {
            if (_isShuttingDown)
                return;

            _isShuttingDown = true;
            Application.Current.Shutdown();
        }
    }
}