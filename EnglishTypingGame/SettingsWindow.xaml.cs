// SettingsWindow.xaml.cs
using System.Windows;
using System.Windows.Controls;

namespace EnglishTypingGame
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            VolumeSlider.Value = DataManager.SoundVolume * 100;
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            DataManager.SoundVolume = (float)VolumeSlider.Value / 100;

            var selected = DifficultyCombo.SelectedItem as ComboBoxItem;
            if (selected != null)
            {
                string content = selected.Content.ToString();
                if (content.Contains("Лёгкая"))
                    DataManager.CalibrationSpeed = 700;
                else if (content.Contains("Средняя"))
                    DataManager.CalibrationSpeed = 500;
                else if (content.Contains("Сложная"))
                    DataManager.CalibrationSpeed = 350;
            }

            MessageBox.Show("Настройки сохранены!", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ResetProgress_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("ВНИМАНИЕ! Весь прогресс будет сброшен.\nПродолжить?",
                "Сброс прогресса", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                foreach (var word in DataManager.AllWords)
                    word.IsLearned = false;
                DataManager.SaveData();
                MessageBox.Show("Весь прогресс сброшен!", "Готово",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}