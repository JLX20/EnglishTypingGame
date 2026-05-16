using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace EnglishTypingGame
{
    public partial class MixedTrainingWindow : Window
    {
        private readonly string _topic;
        private readonly string _level;
        private readonly Stopwatch _stopwatch;
        private readonly SettingsData _settings;

        private List<MiniGameExercise> _exercises;
        private List<MistakeRecord> _mistakes;

        private int _index;
        private int _correct;
        private int _wrong;
        private int _typedChars;

        private TextBox _answerBox;
        private bool _locked;

        public MixedTrainingWindow(string topic, string level)
        {
            InitializeComponent();

            _topic = topic;
            _level = level;
            _stopwatch = new Stopwatch();
            _settings = SettingsService.Load();

            _exercises = new List<MiniGameExercise>();
            _mistakes = new List<MistakeRecord>();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            WindowNavigationService.HandleCloseToMain(this, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TopicText.Text = "Тема: " + _topic + " | Сложность: " + _level;

            _exercises = MixedTrainingRepository.BuildExercises(_topic, _level);

            if (_exercises.Count == 0)
            {
                MessageBox.Show("Нет заданий для смешанной тренировки.");
                WindowNavigationService.NavigateToMain(this);
                return;
            }

            _stopwatch.Start();
            ShowExercise();
        }

        private void ShowExercise()
        {
            _locked = false;

            if (_index >= _exercises.Count)
            {
                FinishTraining();
                return;
            }

            MiniGameExercise exercise = _exercises[_index];

            ProgressBar.Value = _index * 100.0 / _exercises.Count;
            ScoreText.Text = "Верно: " + _correct + " | Ошибок: " + _wrong;

            PromptText.Text = exercise.Prompt;
            FeedbackText.Text = "";

            AnswerPanel.Children.Clear();

            if (exercise.Options != null && exercise.Options.Count > 0)
                BuildOptions(exercise);
            else
                BuildInput(exercise);
        }

        private void BuildOptions(MiniGameExercise exercise)
        {
            WrapPanel panel = new WrapPanel();
            panel.HorizontalAlignment = HorizontalAlignment.Center;

            foreach (string option in exercise.Options)
            {
                string selected = option;

                Button button = new Button();
                button.Content = selected;
                button.Width = 230;
                button.MinHeight = 54;
                button.Margin = new Thickness(8);
                button.FontSize = GetFontSize("ButtonFontSize", 15);
                button.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);

                button.Click += delegate
                {
                    CheckAnswer(exercise, selected);
                };

                panel.Children.Add(button);
            }

            AnswerPanel.Children.Add(panel);
        }

        private void BuildInput(MiniGameExercise exercise)
        {
            _answerBox = new TextBox();
            _answerBox.Width = 520;
            _answerBox.Height = 62;
            _answerBox.FontSize = GetFontSize("InputFontSize", 24);
            _answerBox.FontWeight = FontWeights.SemiBold;
            _answerBox.Background = GetBrush("InputBgBrush", Brushes.White);
            _answerBox.Foreground = GetBrush("InputTextBrush", Brushes.Black);
            _answerBox.CaretBrush = GetBrush("InputTextBrush", Brushes.Black);
            _answerBox.BorderBrush = GetBrush("InputBorderBrush", Brushes.LightGray);
            _answerBox.BorderThickness = new Thickness(2);
            _answerBox.Padding = new Thickness(16, 8, 16, 8);

            _answerBox.KeyDown += delegate (object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Enter)
                {
                    e.Handled = true;
                    CheckAnswer(exercise, _answerBox.Text);
                }
            };

            AnswerPanel.Children.Add(_answerBox);

            Button check = new Button();
            check.Content = "Проверить";
            check.Width = 150;
            check.Height = 48;
            check.Margin = new Thickness(0, 14, 0, 0);
            check.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
            check.Click += delegate
            {
                CheckAnswer(exercise, _answerBox.Text);
            };

            AnswerPanel.Children.Add(check);

            Dispatcher.BeginInvoke(new Action(delegate
            {
                _answerBox.Focus();
                Keyboard.Focus(_answerBox);
            }), DispatcherPriority.Input);
        }

        private void CheckAnswer(MiniGameExercise exercise, string userAnswer)
        {
            if (_locked)
                return;

            if (string.IsNullOrWhiteSpace(userAnswer))
                return;

            _locked = true;
            _typedChars += userAnswer.Length;

            bool correct = SoftAnswerComparer.IsCorrect(userAnswer, exercise.Answer);

            if (correct)
            {
                _correct++;
                FeedbackText.Foreground = Brushes.ForestGreen;
                FeedbackText.Text = "Правильно!";

                if (exercise.Word != null)
                    ProgressService.MarkWordAsLearned(exercise.Word.English);

                if (_settings.SoundEnabled)
                    SystemSounds.Asterisk.Play();

                GoNext(600);
            }
            else
            {
                _wrong++;
                FeedbackText.Foreground = Brushes.Firebrick;
                FeedbackText.Text = "Ошибка. " + exercise.Explanation;

                AddMistake(exercise, userAnswer);

                if (_settings.SoundEnabled)
                    SystemSounds.Hand.Play();

                GoNext(1300);
            }
        }

        private void DontKnowButton_Click(object sender, RoutedEventArgs e)
        {
            if (_locked)
                return;

            if (_index >= _exercises.Count)
                return;

            MiniGameExercise exercise = _exercises[_index];

            _locked = true;
            _wrong++;

            FeedbackText.Foreground = Brushes.Firebrick;
            FeedbackText.Text = "Правильный ответ: " + exercise.Answer;

            AddMistake(exercise, "Не знаю");

            GoNext(1300);
        }

        private void AddMistake(MiniGameExercise exercise, string answer)
        {
            if (exercise.Word != null)
            {
                _mistakes.Add(new MistakeRecord
                {
                    English = exercise.Word.English,
                    Russian = exercise.Word.Russian,
                    Count = 1,
                    LastWrongAnswer = answer,
                    LastPracticed = DateTime.Now
                });
            }
            else
            {
                _mistakes.Add(new MistakeRecord
                {
                    English = exercise.Answer,
                    Russian = exercise.Prompt,
                    Count = 1,
                    LastWrongAnswer = answer,
                    LastPracticed = DateTime.Now
                });
            }
        }

        private void GoNext(int delay)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(delay);
            timer.Tick += delegate
            {
                timer.Stop();
                _index++;
                ShowExercise();
            };
            timer.Start();
        }

        private void FinishTraining()
        {
            _stopwatch.Stop();

            int total = _correct + _wrong;

            if (total <= 0)
                total = 1;

            GameResult result = new GameResult();
            result.GameName = "Смешанная тренировка";
            result.Topic = _topic;
            result.Level = _level;
            result.Mode = "MixedTraining";
            result.TotalWords = total;
            result.CorrectWords = _correct;
            result.WrongWords = _wrong;
            result.Accuracy = _correct * 100.0 / total;
            result.Duration = _stopwatch.Elapsed;
            result.Mistakes = _mistakes;

            double minutes = Math.Max(_stopwatch.Elapsed.TotalMinutes, 0.1);
            result.Wpm = (_typedChars / 5.0) / minutes;

            ProgressService.ApplyResult(result);

            if (result.Accuracy >= 70)
                ProgressService.MarkPathStepCompleted(_topic, "Mixed");

            WindowNavigationService.Navigate(this, new ResultsWindow(result));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.NavigateToMain(this);
        }

        private Brush GetBrush(string key, Brush fallback)
        {
            object resource = Application.Current.Resources[key];
            Brush brush = resource as Brush;

            if (brush == null)
                return fallback;

            return brush;
        }

        private double GetFontSize(string key, double fallback)
        {
            object resource = Application.Current.Resources[key];

            if (resource == null)
                return fallback;

            double value;

            if (double.TryParse(resource.ToString(), out value))
                return value;

            return fallback;
        }
    }
}