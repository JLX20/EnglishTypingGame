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
        private readonly List<MiniGameInfo> _allGames;
        private List<MiniGameInfo> _filteredGames;
        private bool _isLoaded;

        public MiniGamesMenuWindow(string topic)
        {
            InitializeComponent();

            _topic = topic;
            _allGames = MiniGameRepository.GetMiniGames();
            _filteredGames = new List<MiniGameInfo>();

            TopicText.Text = "Тема слов: " + _topic;

            _isLoaded = true;

            SectionFilterComboBox.SelectedIndex = 0;
            ApplyFilter("Все");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            WindowNavigationService.HandleCloseToMain(this, e);
        }

        private void SectionFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isLoaded)
                return;

            ComboBoxItem selectedItem = SectionFilterComboBox.SelectedItem as ComboBoxItem;

            if (selectedItem == null || selectedItem.Tag == null)
                return;

            string filter = selectedItem.Tag.ToString();
            ApplyFilter(filter);
        }

        private void ApplyFilter(string filter)
        {
            if (_allGames == null)
                return;

            if (string.IsNullOrWhiteSpace(filter) || filter == "Все")
            {
                _filteredGames = _allGames.ToList();
            }
            else
            {
                _filteredGames = _allGames
                    .Where(game => GetFilterGroup(game) == filter)
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
                ClearSelectedInfo();
            }
        }

        private string GetFilterGroup(MiniGameInfo game)
        {
            if (game == null || string.IsNullOrWhiteSpace(game.Group))
                return "Слова";

            if (game.Group == "Слова")
                return "Слова";

            if (game.Group == "Грамматика")
                return "Грамматика";

            if (game.Group == "Предложения")
                return "Предложения";

            if (game.Group == "Диалоги")
                return "Диалоги";

            if (game.Group == "Числа" || game.Group == "Числа и даты")
                return "Числа и даты";

            return "Слова";
        }

        private void GamesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MiniGameInfo info = GamesListBox.SelectedItem as MiniGameInfo;

            if (info == null)
            {
                ClearSelectedInfo();
                return;
            }

            SelectedTitleText.Text = info.Title;
            SelectedGroupText.Text = "Раздел: " + GetFilterGroup(info);
            SelectedDescriptionText.Text = info.Description;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            MiniGameInfo info = GamesListBox.SelectedItem as MiniGameInfo;

            if (info == null)
            {
                MessageBox.Show(
                    "Выбери мини-игру.",
                    "Мини-игра не выбрана",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return;
            }

            WindowNavigationService.Navigate(this, new MiniGameWindow(_topic, info.Mode));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.NavigateToMain(this);
        }

        private void ClearSelectedInfo()
        {
            SelectedTitleText.Text = "Нет мини-игр";
            SelectedGroupText.Text = "";
            SelectedDescriptionText.Text = "В выбранном разделе пока нет мини-игр.";
        }
    }
}