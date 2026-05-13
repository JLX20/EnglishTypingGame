using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace EnglishTypingGame
{
    public partial class CalibrationWindow : Window
    {
        private string[] calibrateWords = { "cat", "dog", "sun", "bird", "fish" };
        private string currentWord;
        private DispatcherTimer timer;
        private DateTime wordStartTime;
        private double totalTime = 0;
        private int attempts = 0;
        private const int requiredAttempts = 3;

        public CalibrationWindow()
        {
            InitializeComponent();
            Loaded += CalibrationWindow_Loaded;
        }

        private void CalibrationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StartNewWord();
        }

        private void StartNewWord()
        {
            Random rand = new Random();
            currentWord = calibrateWords[rand.Next(calibrateWords.Length)];

            Dispatcher.Invoke(() =>
            {
                WordToType.Text = currentWord;
                InputBox.Text = "";
                InputBox.Focus();
            });

            wordStartTime = DateTime.Now;

            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(50);
                timer.Tick += Timer_Tick;
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var elapsed = (DateTime.Now - wordStartTime).TotalMilliseconds;

            Dispatcher.Invoke(() =>
            {
                CalibProgress.Value = Math.Min(100, elapsed / 30);
            });
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var elapsed = (DateTime.Now - wordStartTime).TotalMilliseconds;

                if (InputBox.Text.Trim().ToLower() == currentWord)
                {
                    totalTime += elapsed;
                    attempts++;

                    Dispatcher.Invoke(() =>
                    {
                        StatusText.Text = $"✅ Отлично! Попытка {attempts} из {requiredAttempts}";
                    });

                    if (attempts >= requiredAttempts)
                    {
                        double avgTime = totalTime / requiredAttempts;
                        int newSpeed = (int)(avgTime / currentWord.Length);
                        DataManager.CalibrationSpeed = Math.Max(300, Math.Min(1000, newSpeed));

                        timer.Stop();

                        Dispatcher.Invoke(() =>
                        {
                            MessageBox.Show($"🎯 Настройка завершена!\nСкорость: {DataManager.CalibrationSpeed} мс/букву",
                                "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                        });

                        this.Close();
                    }
                    else
                    {
                        StartNewWord();
                    }
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        StatusText.Text = $"❌ Ошибка! Правильно: {currentWord}. Попробуйте ещё раз";
                        InputBox.Text = "";
                        InputBox.Focus();
                    });
                }
            }
        }
    }
}