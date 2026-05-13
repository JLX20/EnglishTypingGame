using EnglishTypingGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EnglishTypingGame
{
    public partial class RhythmGameWindow : Window
    {
        private List<Word> levelWords;
        private List<FallingLetter> fallingLetters = new List<FallingLetter>();
        private DispatcherTimer gameTimer;
        private DispatcherTimer rhythmTimer;
        private string currentWord = "";
        private int wordIndex = 0;
        private int score = 0;
        private int health = 10;
        private int combo = 0;
        private bool isGameOver = false;
        private bool isGameInitialized = false;
        private Random random = new Random();
        private bool isWaitingForBeat = false;

        public RhythmGameWindow()
        {
            InitializeComponent();

            levelWords = DataManager.GetWordsByLevel(DataManager.CurrentLevel).Where(w => w.IsLearned).ToList();

            if (levelWords.Count == 0)
            {
                MessageBox.Show("Нет выученных слов для этого уровня! Сначала выучите слова в обучении.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
                return;
            }

            NextWord();
        }

        private void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            isGameInitialized = true;
            GameCanvas.Focus();
            StartGameLoop();
        }

        private void StartGameLoop()
        {
            if (!isGameInitialized) return;

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(50);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            rhythmTimer = new DispatcherTimer();
            rhythmTimer.Interval = TimeSpan.FromMilliseconds(DataManager.CalibrationSpeed);
            rhythmTimer.Tick += RhythmTimer_Tick;
            rhythmTimer.Start();
        }

        private void RhythmTimer_Tick(object sender, EventArgs e)
        {
            if (isGameOver || !isGameInitialized) return;

            // Анимация пульса
            try
            {
                var pulseAnim = new DoubleAnimation
                {
                    From = 20,
                    To = 30,
                    Duration = TimeSpan.FromMilliseconds(100),
                    AutoReverse = true
                };
                RhythmPulse.BeginAnimation(Ellipse.WidthProperty, pulseAnim);
                RhythmPulse.BeginAnimation(Ellipse.HeightProperty, pulseAnim);
            }
            catch { }

            // Публикуем следующую букву текущего слова
            if (wordIndex < currentWord.Length)
            {
                char letter = currentWord[wordIndex];
                AddFallingLetter(letter);
                wordIndex++;
                isWaitingForBeat = true;
            }
        }

        private void AddFallingLetter(char letter)
        {
            if (!isGameInitialized) return;

            var textBlock = new TextBlock
            {
                Text = letter.ToString(),
                FontSize = 28,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),
                Width = 40,
                Height = 40,
                TextAlignment = TextAlignment.Center
            };

            // Безопасное получение ширины Canvas
            double canvasWidth = GameCanvas.ActualWidth;
            if (canvasWidth <= 0) canvasWidth = 800;

            double x = random.Next(50, (int)canvasWidth - 90);
            if (x < 50) x = 50;
            if (x > canvasWidth - 90) x = canvasWidth - 100;

            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, -40);

            GameCanvas.Dispatcher.Invoke(() =>
            {
                GameCanvas.Children.Add(textBlock);
            });

            fallingLetters.Add(new FallingLetter
            {
                Element = textBlock,
                Letter = letter,
                Y = -40,
                X = x
            });
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (isGameOver || !isGameInitialized) return;

            double canvasHeight = GameCanvas.ActualHeight;
            if (canvasHeight <= 0) canvasHeight = 500;

            // Обновляем падение букв
            for (int i = fallingLetters.Count - 1; i >= 0; i--)
            {
                var fl = fallingLetters[i];
                if (fl.Element == null || !GameCanvas.Children.Contains(fl.Element))
                {
                    fallingLetters.RemoveAt(i);
                    continue;
                }

                fl.Y += 4 + (combo / 40);

                GameCanvas.Dispatcher.Invoke(() =>
                {
                    Canvas.SetTop(fl.Element, fl.Y);
                });

                // Проверка касания ритмической линии
                if (fl.Y >= canvasHeight - 100 && fl.Y <= canvasHeight - 90)
                {
                    if (isWaitingForBeat)
                    {
                        GameCanvas.Dispatcher.Invoke(() =>
                        {
                            GameCanvas.Children.Remove(fl.Element);
                        });
                        fallingLetters.RemoveAt(i);
                        isWaitingForBeat = false;

                        // Бонус за попадание в ритм
                        combo++;
                        UpdateUI();
                    }
                }

                // Буква упала на дно
                if (fl.Y > canvasHeight - 60)
                {
                    GameCanvas.Dispatcher.Invoke(() =>
                    {
                        if (GameCanvas.Children.Contains(fl.Element))
                            GameCanvas.Children.Remove(fl.Element);
                    });
                    fallingLetters.RemoveAt(i);
                    TakeDamage();
                }
            }

            // Обновляем подсказку
            try
            {
                CurrentWordHint.Text = $"Слово: {currentWord}";
            }
            catch { }
        }

        private void TakeDamage()
        {
            health--;
            combo = 0;
            UpdateUI();

            // Визуальный эффект урона
            try
            {
                var damageAnim = new DoubleAnimation
                {
                    From = 1,
                    To = 0.3,
                    Duration = TimeSpan.FromMilliseconds(100),
                    AutoReverse = true
                };
                RhythmPulse.BeginAnimation(Ellipse.OpacityProperty, damageAnim);
            }
            catch { }

            if (health <= 0)
            {
                GameOver();
            }
        }

        private void GameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (isGameOver || !isGameInitialized) return;

            if (e.Key >= Key.A && e.Key <= Key.Z)
            {
                InputTextBox.Text += e.Key.ToString().ToLower();
                InputTextBox.CaretIndex = InputTextBox.Text.Length;
            }
            else if (e.Key == Key.Back)
            {
                if (InputTextBox.Text.Length > 0)
                    InputTextBox.Text = InputTextBox.Text.Substring(0, InputTextBox.Text.Length - 1);
            }
            else if (e.Key == Key.Enter)
            {
                CheckWord();
            }
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CheckWord();
        }

        private void CheckWord()
        {
            if (string.IsNullOrWhiteSpace(InputTextBox.Text)) return;

            if (InputTextBox.Text.Trim().ToLower() == currentWord.ToLower())
            {
                // Правильное слово!
                score += 100 + combo * 10;

                // Визуальный эффект
                try
                {
                    var pulse = new DoubleAnimation
                    {
                        From = 0.5,
                        To = 1,
                        Duration = TimeSpan.FromMilliseconds(100),
                        AutoReverse = true
                    };
                    RhythmPulse.BeginAnimation(Ellipse.OpacityProperty, pulse);
                }
                catch { }

                NextWord();
                UpdateUI();
            }
            else if (InputTextBox.Text.Length >= currentWord.Length)
            {
                // Ошибка
                combo = 0;
                TakeDamage();
                InputTextBox.Text = "";
                InputTextBox.Focus();
            }
        }

        private void NextWord()
        {
            if (levelWords == null || levelWords.Count == 0) return;

            var randomWord = levelWords[random.Next(levelWords.Count)];
            currentWord = randomWord.English;
            wordIndex = 0;

            InputTextBox.Dispatcher.Invoke(() =>
            {
                InputTextBox.Text = "";
                InputTextBox.Focus();
            });

            isWaitingForBeat = false;

            // Очищаем все падающие буквы
            foreach (var fl in fallingLetters)
            {
                GameCanvas.Dispatcher.Invoke(() =>
                {
                    if (GameCanvas.Children.Contains(fl.Element))
                        GameCanvas.Children.Remove(fl.Element);
                });
            }
            fallingLetters.Clear();
        }

        private void UpdateUI()
        {
            Dispatcher.Invoke(() =>
            {
                HealthText.Text = health.ToString();
                ScoreText.Text = score.ToString();
                ComboText.Text = combo.ToString();

                if (health < 5)
                    HealthText.Foreground = new SolidColorBrush(Colors.Red);
                else if (health < 8)
                    HealthText.Foreground = new SolidColorBrush(Colors.Orange);
                else
                    HealthText.Foreground = new SolidColorBrush(Colors.White);
            });
        }

        private void GameOver()
        {
            isGameOver = true;

            gameTimer?.Stop();
            rhythmTimer?.Stop();

            Dispatcher.Invoke(() =>
            {
                MessageBox.Show($"💀 ИГРА ОКОНЧЕНА 💀\nВаш счёт: {score}\nПопробуйте ещё раз!",
                    "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);

                var levelsWindow = new LevelsWindow();
                levelsWindow.Show();
                this.Close();
            });
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            gameTimer?.Stop();
            rhythmTimer?.Stop();

            var levelsWindow = new LevelsWindow();
            levelsWindow.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameTimer?.Stop();
            rhythmTimer?.Stop();
        }
    }

    public class FallingLetter
    {
        public TextBlock Element { get; set; }
        public char Letter { get; set; }
        public double Y { get; set; }
        public double X { get; set; }
    }
}