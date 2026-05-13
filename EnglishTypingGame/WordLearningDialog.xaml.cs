// WordLearningDialog.xaml.cs
using System.Windows;
using System.Windows.Input;
using EnglishTypingGame.Models;

namespace EnglishTypingGame
{
    public partial class WordLearningDialog : Window
    {
        private Word word;

        public WordLearningDialog(Word w)
        {
            InitializeComponent();
            word = w;
            EnglishWord.Text = word.English;
            RussianWord.Text = "???";
            HintText.Text = $"Введите перевод слова (длина: {word.Russian.Length} букв)";
            TypingBox.Focus();
        }

        private void TypingBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Check_Click(sender, e);
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TypingBox.Text))
            {
                HintText.Text = "Введите перевод!";
                return;
            }

            if (TypingBox.Text.Trim().ToLower() == word.Russian.ToLower())
            {
                word.IsLearned = true;
                MessageBox.Show($"✅ Правильно! Слово «{word.English}» выучено.",
                    "Отлично!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                HintText.Text = $"❌ Неправильно. Правильный перевод: {word.Russian}. Попробуйте ещё раз!";
                RussianWord.Text = word.Russian;
                TypingBox.Text = "";
                TypingBox.Focus();
            }
        }
    }
}