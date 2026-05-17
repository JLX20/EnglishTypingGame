using System.Windows;
using System.Windows.Controls;

namespace EnglishTypingGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SettingsData settings = SettingsService.Load();
            ThemeService.ApplyTheme(settings.ThemeName, settings.BackgroundName, settings.TextSizeName);

            TopicComboBox.ItemsSource = LessonQueryService.GetTopicsForUi();
            TopicComboBox.SelectedIndex = 0;

            LevelComboBox.ItemsSource = LessonQueryService.GetLevelsForUi();
            LevelComboBox.SelectedIndex = 0;

            InputModeComboBox.SelectedIndex = 0;

            RefreshStats();
        }

        private string SelectedTopic
        {
            get
            {
                string topic = TopicComboBox.SelectedItem as string;

                if (string.IsNullOrWhiteSpace(topic))
                    return "Все темы";

                return topic;
            }
        }

        private string SelectedLevel
        {
            get
            {
                string level = LevelComboBox.SelectedItem as string;

                if (string.IsNullOrWhiteSpace(level))
                    return "Все уровни";

                return level;
            }
        }

        private GameInputMode SelectedInputMode
        {
            get
            {
                ComboBoxItem item = InputModeComboBox.SelectedItem as ComboBoxItem;

                if (item == null || item.Tag == null)
                    return GameInputMode.RussianToEnglish;

                string tag = item.Tag.ToString();

                if (tag == "EnglishToRussian")
                    return GameInputMode.EnglishToRussian;

                if (tag == "ExampleToWord")
                    return GameInputMode.ExampleToWord;

                if (tag == "Mixed")
                    return GameInputMode.Mixed;

                return GameInputMode.RussianToEnglish;
            }
        }

        private void LearnButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new LearnWindow(SelectedTopic, SelectedLevel));
        }

        private void GrammarButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new GrammarLearnWindow());
        }

        private void StudyPathButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new StudyPathWindow(SelectedTopic, SelectedLevel));
        }

        private void MiniGamesButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new MiniGamesMenuWindow(SelectedTopic));
        }

        private void MixedTrainingButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new MixedTrainingWindow(SelectedTopic, SelectedLevel));
        }

        private void ClassicGameButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(
                this,
                new GameWindow(SelectedTopic, "TranslationToEnglish", SelectedLevel, SelectedInputMode));
        }

        private void MistakesButton_Click(object sender, RoutedEventArgs e)
        {
            ProgressData progress = ProgressService.Load();

            if (progress.Mistakes == null || progress.Mistakes.Count == 0)
            {
                MessageBox.Show(
                    "Пока нет ошибок. Сначала сыграй обычный раунд или мини-игру.",
                    "Нет ошибок",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return;
            }

            WindowNavigationService.Navigate(
                this,
                new GameWindow("Все темы", "Mistakes", "Все уровни", GameInputMode.RussianToEnglish));
        }

        private void ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new ProgressWindow());
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new SettingsWindow());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Shutdown();
        }

        private void RefreshStats()
        {
            ProgressData progress = ProgressService.Load();

            double totalAccuracy = 0;

            if (progress.TotalWords > 0)
                totalAccuracy = progress.CorrectWords * 100.0 / progress.TotalWords;

            int mistakeCount = progress.Mistakes == null ? 0 : progress.Mistakes.Count;
            int learnedCount = progress.LearnedWords == null ? 0 : progress.LearnedWords.Count;

            StatsText.Text =
                "Изучено слов: " + learnedCount + "\n" +
                "Раундов сыграно: " + progress.TotalRounds + "\n" +
                "Всего заданий: " + progress.TotalWords + "\n" +
                "Правильных ответов: " + progress.CorrectWords + "\n" +
                "Общая точность: " + totalAccuracy.ToString("0.0") + "%\n" +
                "Лучшая точность: " + progress.BestAccuracy.ToString("0.0") + "%\n" +
                "Лучший WPM: " + progress.BestWpm.ToString("0.0") + "\n" +
                "Серия дней: " + progress.CurrentStreak + "\n" +
                "Слов с ошибками: " + mistakeCount;
        }
    }
}