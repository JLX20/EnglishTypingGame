// MainWindow.xaml.cs
using System.Windows;

namespace EnglishTypingGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LevelsBtn_Click(object sender, RoutedEventArgs e)
        {
            var levelsWindow = new LevelsWindow();
            levelsWindow.Show();
            this.Close();
        }

        private void CardsBtn_Click(object sender, RoutedEventArgs e)
        {
            var cardsWindow = new CardsLibraryWindow();
            cardsWindow.Show();
            this.Close();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.Owner = this;
            settingsWindow.ShowDialog();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
    }
}