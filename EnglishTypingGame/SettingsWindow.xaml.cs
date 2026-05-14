using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace EnglishTypingGame
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            LoadSettings();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            WindowNavigationService.HandleCloseToMain(this, e);
        }

        private void LoadSettings()
        {
            SettingsData settings = SettingsService.Load();

            SelectComboBoxValue(WordsPerRoundComboBox, settings.WordsPerRound.ToString());
            SelectComboBoxValue(RainSecondsComboBox, settings.WordRainSeconds.ToString());

            SelectTheme(settings.ThemeName);
            SelectBackground(settings.BackgroundName);

            FallSpeedSlider.Value = settings.WordRainFallSpeedMultiplier;
            SpawnSpeedSlider.Value = settings.WordRainSpawnIntervalMilliseconds;

            CloudModeCheckBox.IsChecked = settings.WordRainCloudMode;
            VisualEffectsCheckBox.IsChecked = settings.WordRainVisualEffects;

            SlowModeCheckBox.IsChecked = settings.SlowMode;
            SoundCheckBox.IsChecked = settings.SoundEnabled;
            ExamplesCheckBox.IsChecked = settings.ShowExamples;

            UpdateFallSpeedText();
            UpdateSpawnSpeedText();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsData settings = new SettingsData();

            settings.WordsPerRound = GetComboBoxInt(WordsPerRoundComboBox, 10);
            settings.WordRainSeconds = GetComboBoxInt(RainSecondsComboBox, 60);

            settings.WordRainFallSpeedMultiplier = FallSpeedSlider.Value;
            settings.WordRainSpawnIntervalMilliseconds = (int)SpawnSpeedSlider.Value;

            settings.WordRainCloudMode = CloudModeCheckBox.IsChecked == true;
            settings.WordRainVisualEffects = VisualEffectsCheckBox.IsChecked == true;

            settings.SlowMode = SlowModeCheckBox.IsChecked == true;
            settings.SoundEnabled = SoundCheckBox.IsChecked == true;
            settings.ShowExamples = ExamplesCheckBox.IsChecked == true;

            settings.ThemeName = GetSelectedTheme();
            settings.BackgroundName = GetSelectedBackground();

            SettingsService.Save(settings);
            ThemeService.ApplyTheme(settings.ThemeName, settings.BackgroundName);

            MessageBox.Show(
                "Настройки сохранены.",
                "Готово",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            WindowNavigationService.NavigateToMain(this);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.NavigateToMain(this);
        }

        private int GetComboBoxInt(ComboBox comboBox, int defaultValue)
        {
            ComboBoxItem item = comboBox.SelectedItem as ComboBoxItem;

            if (item == null)
                return defaultValue;

            int value;

            if (int.TryParse(item.Content.ToString(), out value))
                return value;

            return defaultValue;
        }

        private void SelectComboBoxValue(ComboBox comboBox, string value)
        {
            foreach (object obj in comboBox.Items)
            {
                ComboBoxItem item = obj as ComboBoxItem;

                if (item != null && item.Content.ToString() == value)
                {
                    comboBox.SelectedItem = item;
                    return;
                }
            }

            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;
        }

        private string GetSelectedTheme()
        {
            ComboBoxItem item = ThemeComboBox.SelectedItem as ComboBoxItem;

            if (item == null || item.Tag == null)
                return "Blue";

            return item.Tag.ToString();
        }

        private void SelectTheme(string themeName)
        {
            foreach (object obj in ThemeComboBox.Items)
            {
                ComboBoxItem item = obj as ComboBoxItem;

                if (item != null && item.Tag != null && item.Tag.ToString() == themeName)
                {
                    ThemeComboBox.SelectedItem = item;
                    return;
                }
            }

            ThemeComboBox.SelectedIndex = 0;
        }

        private string GetSelectedBackground()
        {
            ComboBoxItem item = BackgroundComboBox.SelectedItem as ComboBoxItem;

            if (item == null || item.Tag == null)
                return "Sky";

            return item.Tag.ToString();
        }

        private void SelectBackground(string backgroundName)
        {
            foreach (object obj in BackgroundComboBox.Items)
            {
                ComboBoxItem item = obj as ComboBoxItem;

                if (item != null && item.Tag != null && item.Tag.ToString() == backgroundName)
                {
                    BackgroundComboBox.SelectedItem = item;
                    return;
                }
            }

            BackgroundComboBox.SelectedIndex = 0;
        }

        private void FallSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateFallSpeedText();
        }

        private void SpawnSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSpawnSpeedText();
        }

        private void UpdateFallSpeedText()
        {
            if (FallSpeedValueText == null)
                return;

            FallSpeedValueText.Text =
                "Скорость движения слов: x" + FallSpeedSlider.Value.ToString("0.0");
        }

        private void UpdateSpawnSpeedText()
        {
            if (SpawnSpeedValueText == null)
                return;

            SpawnSpeedValueText.Text =
                "Частота появления слов: каждые " + ((int)SpawnSpeedSlider.Value) + " мс";
        }
    }
}