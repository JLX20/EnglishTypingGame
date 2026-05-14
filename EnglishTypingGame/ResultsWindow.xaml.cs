using System.ComponentModel;
using System.Windows;

namespace EnglishTypingGame
{
    public partial class ResultsWindow : Window
    {
        private readonly GameResult _result;

        public ResultsWindow(GameResult result)
        {
            InitializeComponent();

            _result = result;
            ShowResult();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            WindowNavigationService.HandleCloseToMain(this, e);
        }

        private void ShowResult()
        {
            if (_result == null)
            {
                TitleText.Text = "Результаты";
                ResultText.Text = "Нет данных.";
                AdviceText.Text = "";
                return;
            }

            TitleText.Text = _result.GameName;

            ResultText.Text =
                "Всего заданий: " + _result.TotalWords + "\n" +
                "Правильно: " + _result.CorrectWords + "\n" +
                "Ошибок: " + _result.WrongWords + "\n" +
                "Точность: " + _result.Accuracy.ToString("0.0") + "%\n" +
                "Скорость: " + _result.Wpm.ToString("0.0") + " WPM\n" +
                "Время: " + _result.Duration.ToString(@"mm\:ss");

            if (_result.Accuracy >= 90)
            {
                AdviceText.Text = "Отличный результат! Можно переходить к более сложным словам или грамматике.";
            }
            else if (_result.Accuracy >= 70)
            {
                AdviceText.Text = "Хороший результат. Повтори слова, где были ошибки, и попробуй ещё раз.";
            }
            else
            {
                AdviceText.Text = "Лучше вернуться к изучению слов и пройти мини-игры ещё раз.";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.NavigateToMain(this);
        }
    }
}