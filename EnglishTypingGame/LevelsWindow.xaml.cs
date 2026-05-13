// LevelsWindow.xaml.cs (исправленный)
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EnglishTypingGame
{
    public partial class LevelsWindow : Window
    {
        public LevelsWindow()
        {
            InitializeComponent();
            LoadLevels();
        }

        private void LoadLevels()
        {
            LevelsList.Children.Clear();

            for (int i = 1; i <= 10; i++)
            {
                bool completed = DataManager.IsLevelCompleted(i);

                Button levelButton = new Button
                {
                    Width = 160,
                    Height = 90,
                    Margin = new Thickness(15),
                    Tag = i,
                    Background = completed ? (Brush)new BrushConverter().ConvertFrom("#50C878") : (Brush)new BrushConverter().ConvertFrom("#888888"),
                    Foreground = Brushes.White,
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Cursor = Cursors.Hand
                };

                StackPanel panel = new StackPanel();
                TextBlock nameText = new TextBlock
                {
                    Text = $"Уровень {i}",
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                TextBlock statusText = new TextBlock
                {
                    Text = completed ? "✅ ВЫУЧЕН" : "🔒 ЗАКРЫТ",
                    FontSize = 12,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                panel.Children.Add(nameText);
                panel.Children.Add(statusText);
                levelButton.Content = panel;
                levelButton.Click += LevelButton_Click;

                LevelsList.Children.Add(levelButton);
            }
        }

        private async void LevelButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int levelId = (int)button.Tag;
            DataManager.CurrentLevel = levelId;

            if (DataManager.IsLevelCompleted(levelId))
            {
                var result = MessageBox.Show($"Уровень {levelId} готов! Пройти калибровку и начать игру?",
                    "Готово", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var calibration = new CalibrationWindow();
                    calibration.ShowDialog();

                    var gameWindow = new RhythmGameWindow();
                    gameWindow.Show();
                    this.Close();
                }
            }
            else
            {
                var learningHub = new LearningHubWindow(levelId);
                learningHub.ShowDialog();

                if (DataManager.IsLevelCompleted(levelId))
                {
                    var calibration = new CalibrationWindow();
                    calibration.ShowDialog();

                    var gameWindow = new RhythmGameWindow();
                    gameWindow.Show();
                    this.Close();
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}