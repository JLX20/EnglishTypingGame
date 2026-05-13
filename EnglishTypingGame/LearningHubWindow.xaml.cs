// LearningHubWindow.xaml.cs
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EnglishTypingGame.Models;

namespace EnglishTypingGame
{
    public partial class LearningHubWindow : Window
    {
        private int level;
        private List<Word> levelWords;

        public LearningHubWindow(int levelId)
        {
            InitializeComponent();
            level = levelId;
            LevelTitle.Text = $"УРОВЕНЬ {level}: ИЗУЧЕНИЕ СЛОВ";
            LoadWords();
        }

        private void LoadWords()
        {
            levelWords = DataManager.GetWordsByLevel(level);
            WordsGrid.ItemsSource = levelWords;
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            int learned = levelWords.Count(w => w.IsLearned);
            ProgressText.Text = $"Прогресс: {learned}/{levelWords.Count} слов выучено";
        }

        private void LearnWord_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string english = button.Tag.ToString();
            var word = levelWords.First(w => w.English == english);

            var learnDialog = new WordLearningDialog(word);
            learnDialog.ShowDialog();

            DataManager.SaveData();
            LoadWords();
            UpdateProgress();
        }

        private void StartTest_Click(object sender, RoutedEventArgs e)
        {
            var testWindow = new TestDriveWindow(level);
            testWindow.ShowDialog();

            DataManager.SaveData();
            LoadWords();

            if (DataManager.IsLevelCompleted(level))
            {
                MessageBox.Show($"🎉 Поздравляю! Уровень {level} полностью выучен! 🎉",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}