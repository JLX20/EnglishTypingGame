using System.ComponentModel;
using System.Windows;

namespace EnglishTypingGame
{
    public partial class ProgressWindow : Window
    {
        public ProgressWindow()
        {
            InitializeComponent();
            LoadProgress();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            WindowNavigationService.HandleCloseToMain(this, e);
        }

        private void LoadProgress()
        {
            ProgressData progress = ProgressService.Load();

            double totalAccuracy = 0;

            if (progress.TotalWords > 0)
                totalAccuracy = progress.CorrectWords * 100.0 / progress.TotalWords;

            int learnedCount = progress.LearnedWords == null ? 0 : progress.LearnedWords.Count;
            int mistakesCount = progress.Mistakes == null ? 0 : progress.Mistakes.Count;

            ProgressText.Text =
                "Изучено слов: " + learnedCount + "\n" +
                "Раундов сыграно: " + progress.TotalRounds + "\n" +
                "Всего заданий: " + progress.TotalWords + "\n" +
                "Правильных ответов: " + progress.CorrectWords + "\n" +
                "Ошибок всего: " + progress.TotalMistakes + "\n" +
                "Общая точность: " + totalAccuracy.ToString("0.0") + "%\n" +
                "Лучшая точность: " + progress.BestAccuracy.ToString("0.0") + "%\n" +
                "Лучший WPM: " + progress.BestWpm.ToString("0.0") + "\n" +
                "Серия дней: " + progress.CurrentStreak + "\n" +
                "Слов с ошибками: " + mistakesCount;

            MistakesGrid.ItemsSource = progress.Mistakes;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Ты точно хочешь сбросить весь прогресс?",
                "Сброс прогресса",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            ProgressService.Save(new ProgressData());
            LoadProgress();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.NavigateToMain(this);
        }
    }
}