using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace EnglishTypingGame
{
    public partial class StudyPathWindow : Window
    {
        private readonly string _topic;
        private readonly string _level;

        public StudyPathWindow(string topic, string level)
        {
            InitializeComponent();

            _topic = topic;
            _level = level;

            LoadPath();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            WindowNavigationService.HandleCloseToMain(this, e);
        }

        private void LoadPath()
        {
            TopicText.Text = "Тема: " + _topic + " | Сложность: " + _level;

            List<WordItem> words = LessonQueryService.GetWords(_topic, _level);

            int total = words.Count;
            int learned = words.Count(w => ProgressService.IsWordLearned(w.English));

            double percent = total == 0 ? 0 : learned * 100.0 / total;

            bool wordsDone = total > 0 && learned >= total;
            bool gameDone = ProgressService.IsPathStepCompleted(_topic, "Game");
            bool mixedDone = ProgressService.IsPathStepCompleted(_topic, "Mixed");

            SummaryText.Text =
                "Изучено слов в выбранной теме и сложности: " + learned + " из " + total + "\n" +
                "Готовность темы: " + percent.ToString("0.0") + "%\n" +
                "Рекомендуемый путь: слова → правила → мини-игры → смешанная тренировка → финальная игра.";

            MarkButton(WordsButton, wordsDone);
            MarkButton(MixedButton, mixedDone);
            MarkButton(FinalGameButton, gameDone);
        }

        private void MarkButton(System.Windows.Controls.Button button, bool done)
        {
            if (done)
            {
                button.Content = "✓ " + button.Content;
                button.Background = GetBrush("ButtonSuccessBrush", Brushes.ForestGreen);
            }
        }

        private Brush GetBrush(string key, Brush fallback)
        {
            object resource = Application.Current.Resources[key];
            Brush brush = resource as Brush;

            if (brush == null)
                return fallback;

            return brush;
        }

        private void WordsButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new LearnWindow(_topic, _level));
        }

        private void RulesButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new GrammarLearnWindow());
        }

        private void MiniGamesButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new MiniGamesMenuWindow(_topic));
        }

        private void MixedButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new MixedTrainingWindow(_topic, _level));
        }

        private void FinalGameButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(
                this,
                new GameWindow(_topic, "TranslationToEnglish", _level, GameInputMode.Mixed));
        }

        private void ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new ProgressWindow());
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.NavigateToMain(this);
        }
    }
}