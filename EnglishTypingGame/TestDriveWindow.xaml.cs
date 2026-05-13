// TestDriveWindow.xaml.cs
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EnglishTypingGame.Models;

namespace EnglishTypingGame
{
    public partial class TestDriveWindow : Window
    {
        private List<Word> testWords;
        private int currentIndex = 0;
        private int correct = 0;
        private int wrong = 0;
        private int requiredCorrect;

        public TestDriveWindow(int level)
        {
            InitializeComponent();
            testWords = DataManager.GetWordsByLevel(level).ToList();
            requiredCorrect = (int)(testWords.Count * 0.7); // 70% для прохода
            ShowNextWord();
        }

        private void ShowNextWord()
        {
            if (currentIndex < testWords.Count)
            {
                RussianWord.Text = testWords[currentIndex].Russian;
                AnswerBox.Text = "";
                AnswerBox.Focus();
                QuestionText.Text = $"Слово {currentIndex + 1} из {testWords.Count}";
                NextButton.Visibility = Visibility.Collapsed;
                ScoreText.Text = $"✅ Правильно: {correct} | ❌ Ошибок: {wrong}";
            }
            else
            {
                FinishTest();
            }
        }

        private void AnswerBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && NextButton.Visibility != Visibility.Visible)
            {
                CheckAnswer();
            }
        }

        private void CheckAnswer()
        {
            if (string.IsNullOrWhiteSpace(AnswerBox.Text))
            {
                MessageBox.Show("Введите ответ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (AnswerBox.Text.Trim().ToLower() == testWords[currentIndex].English.ToLower())
            {
                correct++;
                if (!testWords[currentIndex].IsLearned)
                {
                    testWords[currentIndex].IsLearned = true;
                    DataManager.MarkWordAsLearned(testWords[currentIndex].English);
                }
                MessageBox.Show("✅ ПРАВИЛЬНО!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                wrong++;
                MessageBox.Show($"❌ НЕПРАВИЛЬНО!\nПравильный ответ: {testWords[currentIndex].English}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            currentIndex++;
            ScoreText.Text = $"✅ Правильно: {correct} | ❌ Ошибок: {wrong}";
            ShowNextWord();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer();
        }

        private void FinishTest()
        {
            if (correct >= requiredCorrect)
            {
                MessageBox.Show($"🎉 ТЕСТ ПРОЙДЕН! 🎉\nПравильно: {correct} из {testWords.Count}\nУровень открыт!",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                var result = MessageBox.Show($"❌ ТЕСТ НЕ ПРОЙДЕН ❌\nПравильно: {correct} из {testWords.Count}\nНужно: {requiredCorrect}\n\nПовторить слова?",
                    "Неудача", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    currentIndex = 0;
                    correct = 0;
                    wrong = 0;
                    ShowNextWord();
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}