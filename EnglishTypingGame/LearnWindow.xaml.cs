using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace EnglishTypingGame
{
    public partial class LearnWindow : Window
    {
        private readonly string _topic;
        private readonly string _level;
        private readonly List<WordItem> _words;
        private int _index;

        public LearnWindow(string topic)
            : this(topic, "Все уровни")
        {
        }

        public LearnWindow(string topic, string level)
        {
            InitializeComponent();

            _topic = topic;
            _level = level;

            _words = LessonQueryService.GetWords(topic, level);

            TopicText.Text = "Тема: " + topic + " | Сложность: " + level;

            ShowWord();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            WindowNavigationService.HandleCloseToMain(this, e);
        }

        private void ShowWord()
        {
            if (_words.Count == 0)
            {
                MessageBox.Show(
                    "В этой теме и сложности пока нет слов.",
                    "Нет слов",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                WindowNavigationService.NavigateToMain(this);
                return;
            }

            if (_index < 0)
                _index = 0;

            if (_index >= _words.Count)
                _index = _words.Count - 1;

            WordItem word = _words[_index];

            WordCounterText.Text = "Слово " + (_index + 1) + " из " + _words.Count;
            EnglishWordText.Text = word.English;
            RussianWordText.Text = word.Russian;
            LevelText.Text = "Тема: " + word.Topic + " | Уровень: " + word.Level;
            ExampleText.Text = "Пример: " + word.Example;

            LearnProgressBar.Value = (_index + 1) * 100.0 / _words.Count;

            PracticeBox.Clear();
            PracticeFeedbackText.Text = "";

            bool learned = ProgressService.IsWordLearned(word.English);

            if (learned)
            {
                PracticeFeedbackText.Foreground = Brushes.ForestGreen;
                PracticeFeedbackText.Text = "Это слово уже отмечено как выученное.";
            }
        }

        private void PracticeBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_words.Count == 0)
                return;

            WordItem word = _words[_index];
            string answer = PracticeBox.Text.Trim();

            if (string.IsNullOrEmpty(answer))
            {
                PracticeFeedbackText.Text = "";
                return;
            }

            if (SoftAnswerComparer.IsCorrect(answer, word.English))
            {
                PracticeFeedbackText.Foreground = Brushes.ForestGreen;
                PracticeFeedbackText.Text = "Правильно!";
            }
            else if (word.English.ToLower().StartsWith(answer.ToLower()))
            {
                PracticeFeedbackText.Foreground = Brushes.DarkOrange;
                PracticeFeedbackText.Text = "Хорошо, продолжай.";
            }
            else
            {
                PracticeFeedbackText.Foreground = Brushes.Firebrick;
                PracticeFeedbackText.Text = "Есть ошибка.";
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            _index--;
            ShowWord();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _index++;

            if (_index >= _words.Count)
                _index = 0;

            ShowWord();
        }

        private void LearnedButton_Click(object sender, RoutedEventArgs e)
        {
            if (_words.Count == 0)
                return;

            WordItem word = _words[_index];

            ProgressService.MarkWordAsLearned(word.English);

            int learnedInThisList = _words.Count(w => ProgressService.IsWordLearned(w.English));

            if (learnedInThisList >= _words.Count)
                ProgressService.MarkPathStepCompleted(_topic, "Words");

            PracticeFeedbackText.Foreground = Brushes.ForestGreen;
            PracticeFeedbackText.Text = "Слово сохранено как выученное.";
        }

        private void MiniGamesButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new MiniGamesMenuWindow(_topic));
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(
                this,
                new GameWindow(_topic, "TranslationToEnglish", _level, GameInputMode.RussianToEnglish));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.NavigateToMain(this);
        }
    }
}