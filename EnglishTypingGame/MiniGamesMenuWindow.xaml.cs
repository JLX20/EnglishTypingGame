using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EnglishTypingGame
{
    public partial class MiniGamesMenuWindow : Window
    {
        private readonly string _topic;

        private List<MiniGameInfo> _allGames;
        private List<MiniGameInfo> _filteredGames;

        public MiniGamesMenuWindow(string topic)
        {
            InitializeComponent();

            _topic = string.IsNullOrWhiteSpace(topic) ? "Все темы" : topic;

            _allGames = new List<MiniGameInfo>();
            _filteredGames = new List<MiniGameInfo>();

            TopicText.Text = "Тема: " + _topic;

            LoadGames();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            WindowNavigationService.HandleCloseToMain(this, e);
        }

        private void LoadGames()
        {
            _allGames = MiniGameRepository.GetMiniGames();

            if (SectionFilterComboBox != null && SectionFilterComboBox.SelectedIndex < 0)
                SectionFilterComboBox.SelectedIndex = 0;

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string selectedSection = GetSelectedSection();

            if (selectedSection == "Все")
            {
                _filteredGames = _allGames.ToList();
            }
            else
            {
                _filteredGames = _allGames
                    .Where(g => g.Group == selectedSection)
                    .ToList();
            }

            GamesListBox.ItemsSource = null;
            GamesListBox.ItemsSource = _filteredGames;

            GamesCountText.Text = "Мини-игр найдено: " + _filteredGames.Count;

            if (_filteredGames.Count > 0)
            {
                GamesListBox.SelectedIndex = 0;
            }
            else
            {
                ClearSelectedGameInfo();
            }
        }

        private string GetSelectedSection()
        {
            ComboBoxItem item = SectionFilterComboBox.SelectedItem as ComboBoxItem;

            if (item == null || item.Tag == null)
                return "Все";

            return item.Tag.ToString();
        }

        private void SectionFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_allGames == null)
                return;

            ApplyFilter();
        }

        private void GamesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MiniGameInfo selectedGame = GamesListBox.SelectedItem as MiniGameInfo;

            if (selectedGame == null)
            {
                ClearSelectedGameInfo();
                return;
            }

            SelectedTitleText.Text = selectedGame.Title;
            SelectedGroupText.Text = "Раздел: " + selectedGame.Group;
            SelectedDescriptionText.Text = selectedGame.Description;
        }

        private void ClearSelectedGameInfo()
        {
            SelectedTitleText.Text = "Мини-игра не выбрана";
            SelectedGroupText.Text = "";
            SelectedDescriptionText.Text = "Выбери мини-игру из списка слева.";
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            MiniGameInfo selectedGame = GamesListBox.SelectedItem as MiniGameInfo;

            if (selectedGame == null)
            {
                MessageBox.Show(
                    "Сначала выбери мини-игру.",
                    "Мини-игра не выбрана",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return;
            }

            /*
             * ВАЖНО:
             * MiniGameWindow принимает аргументы в таком порядке:
             * 1) MiniGameMode
             * 2) string topic
             *
             * Поэтому правильно:
             * new MiniGameWindow(selectedGame.Mode, _topic)
             *
             * Неправильно:
             * new MiniGameWindow(_topic, selectedGame.Mode)
             */

            WindowNavigationService.Navigate(
                this,
                new MiniGameWindow(selectedGame.Mode, _topic));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.NavigateToMain(this);
        }
    }
}