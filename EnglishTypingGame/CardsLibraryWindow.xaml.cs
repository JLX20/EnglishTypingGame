// CardsLibraryWindow.xaml.cs (исправленный)
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EnglishTypingGame
{
    public partial class CardsLibraryWindow : Window
    {
        public CardsLibraryWindow()
        {
            InitializeComponent();

            var learnedWords = DataManager.AllWords.Where(w => w.IsLearned).ToList();

            if (learnedWords.Count == 0)
            {
                MessageBox.Show("Пока нет выученных слов. Пройдите обучение!",
                    "Библиотека пуста", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            foreach (var word in learnedWords)
            {
                Border cardBorder = new Border
                {
                    Background = (Brush)new BrushConverter().ConvertFrom("#3A3A4A"),
                    CornerRadius = new CornerRadius(10),
                    Width = 200,
                    Height = 120,
                    Margin = new Thickness(10),
                    BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FFD966"),
                    BorderThickness = new Thickness(1)
                };

                StackPanel panel = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10)
                };

                TextBlock englishText = new TextBlock
                {
                    Text = word.English,
                    FontSize = 22,
                    FontWeight = FontWeights.Bold,
                    Foreground = (Brush)new BrushConverter().ConvertFrom("#FFD966"),
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                TextBlock russianText = new TextBlock
                {
                    Text = word.Russian,
                    FontSize = 16,
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 10, 0, 0)
                };

                TextBlock levelText = new TextBlock
                {
                    Text = $"Уровень {word.Level}",
                    FontSize = 12,
                    Foreground = (Brush)new BrushConverter().ConvertFrom("#AAAAAA"),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                panel.Children.Add(englishText);
                panel.Children.Add(russianText);
                panel.Children.Add(levelText);
                cardBorder.Child = panel;

                CardsList.Children.Add(cardBorder);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}