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

            SelectTheme(settings.ThemeName);
            SelectBackground(settings.BackgroundName);
            SelectTextSize(settings.TextSizeName);

            SlowModeCheckBox.IsChecked = settings.SlowMode;
            SoundCheckBox.IsChecked = settings.SoundEnabled;
            ExamplesCheckBox.IsChecked = settings.ShowExamples;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsData settings = SettingsService.Load();

            settings.WordsPerRound = GetComboBoxInt(WordsPerRoundComboBox, 10);

            settings.SlowMode = SlowModeCheckBox.IsChecked == true;
            settings.SoundEnabled = SoundCheckBox.IsChecked == true;
            settings.ShowExamples = ExamplesCheckBox.IsChecked == true;

            settings.ThemeName = GetSelectedTheme();
            settings.BackgroundName = GetSelectedBackground();
            settings.TextSizeName = GetSelectedTextSize();

            SettingsService.Save(settings);
            ThemeService.ApplyTheme(settings.ThemeName, settings.BackgroundName, settings.TextSizeName);

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

        private string GetSelectedTextSize()
        {
            ComboBoxItem item = TextSizeComboBox.SelectedItem as ComboBoxItem;

            if (item == null || item.Tag == null)
                return "Normal";

            return item.Tag.ToString();
        }

        private void SelectTextSize(string textSizeName)
        {
            if (string.IsNullOrWhiteSpace(textSizeName))
                textSizeName = "Normal";

            foreach (object obj in TextSizeComboBox.Items)
            {
                ComboBoxItem item = obj as ComboBoxItem;

                if (item != null && item.Tag != null && item.Tag.ToString() == textSizeName)
                {
                    TextSizeComboBox.SelectedItem = item;
                    return;
                }
            }

            TextSizeComboBox.SelectedIndex = 1;
        }
    }
}