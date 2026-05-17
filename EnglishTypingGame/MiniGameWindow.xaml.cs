using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace EnglishTypingGame
{
    public partial class MiniGameWindow : Window
    {
        private readonly MiniGameMode _mode;
        private readonly string _topic;
        private readonly SettingsData _settings;
        private readonly Stopwatch _stopwatch;

        private List<MiniGameExercise> _exercises;
        private List<MistakeRecord> _mistakes;

        private DispatcherTimer _timer;

        private TextBox _answerBox;
        private TextBox _speedAnswerBox;

        private int _index;
        private int _correct;
        private int _wrong;
        private int _typedChars;

        private bool _locked;

        public MiniGameWindow(MiniGameMode mode)
            : this(mode, "Все темы")
        {
        }

        public MiniGameWindow(MiniGameMode mode, string topic)
        {
            InitializeComponent();

            _mode = mode;
            _topic = string.IsNullOrWhiteSpace(topic) ? "Все темы" : topic;
            _settings = SettingsService.Load();
            _stopwatch = new Stopwatch();

            _exercises = new List<MiniGameExercise>();
            _mistakes = new List<MistakeRecord>();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            StopTimer();

            base.OnClosing(e);

            WindowNavigationService.HandleCloseToMain(this, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyHeaderInfo();

            _exercises = LoadExercises();

            if (_exercises == null || _exercises.Count == 0)
            {
                MessageBox.Show(
                    "Для этой мини-игры пока нет заданий.",
                    "Нет заданий",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                WindowNavigationService.NavigateToMain(this);
                return;
            }

            _exercises = _exercises
                .Where(x => x != null)
                .OrderBy(x => Guid.NewGuid())
                .ToList();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(300);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            _stopwatch.Start();

            ShowExercise();
        }

        private void ApplyHeaderInfo()
        {
            TitleText.Text = "Мини-игра";
            DescriptionText.Text = "Тема: " + _topic;

            try
            {
                MethodInfo method = typeof(MiniGameRepository).GetMethod("GetMiniGames", Type.EmptyTypes);

                if (method == null)
                    return;

                object result = method.Invoke(null, null);
                IEnumerable enumerable = result as IEnumerable;

                if (enumerable == null)
                    return;

                foreach (object item in enumerable)
                {
                    if (item == null)
                        continue;

                    PropertyInfo modeProperty = item.GetType().GetProperty("Mode");
                    PropertyInfo titleProperty = item.GetType().GetProperty("Title");
                    PropertyInfo descriptionProperty = item.GetType().GetProperty("Description");

                    if (modeProperty == null)
                        continue;

                    object modeValue = modeProperty.GetValue(item, null);

                    if (modeValue == null)
                        continue;

                    if (!modeValue.Equals(_mode))
                        continue;

                    if (titleProperty != null)
                    {
                        object title = titleProperty.GetValue(item, null);

                        if (title != null)
                            TitleText.Text = title.ToString();
                    }

                    if (descriptionProperty != null)
                    {
                        object description = descriptionProperty.GetValue(item, null);

                        if (description != null)
                            DescriptionText.Text = description + " | Тема: " + _topic;
                    }

                    return;
                }
            }
            catch
            {
                TitleText.Text = "Мини-игра: " + _mode;
                DescriptionText.Text = "Тема: " + _topic;
            }
        }

        private List<MiniGameExercise> LoadExercises()
        {
            List<MiniGameExercise> exercises = TryLoadExercisesFromRepository();

            if (exercises != null && exercises.Count > 0)
                return exercises;

            return BuildFallbackWordExercises();
        }

        private List<MiniGameExercise> TryLoadExercisesFromRepository()
        {
            string[] possibleMethodNames =
            {
                "GetExercises",
                "BuildExercises",
                "GetMiniGameExercises",
                "CreateExercises",
                "GetExercisesForMode",
                "BuildMiniGame"
            };

            foreach (string methodName in possibleMethodNames)
            {
                MethodInfo[] methods = typeof(MiniGameRepository)
                    .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                    .Where(m => m.Name == methodName)
                    .ToArray();

                foreach (MethodInfo method in methods)
                {
                    try
                    {
                        object[] args = BuildRepositoryArguments(method);

                        if (args == null)
                            continue;

                        object result = method.Invoke(null, args);

                        List<MiniGameExercise> exercises = ConvertToExerciseList(result);

                        if (exercises != null && exercises.Count > 0)
                            return exercises;
                    }
                    catch
                    {
                    }
                }
            }

            return new List<MiniGameExercise>();
        }

        private object[] BuildRepositoryArguments(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            object[] args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                Type type = parameters[i].ParameterType;

                if (type == typeof(MiniGameMode))
                {
                    args[i] = _mode;
                }
                else if (type == typeof(string))
                {
                    args[i] = _topic;
                }
                else if (type == typeof(int))
                {
                    args[i] = 12;
                }
                else
                {
                    return null;
                }
            }

            return args;
        }

        private List<MiniGameExercise> ConvertToExerciseList(object result)
        {
            if (result == null)
                return new List<MiniGameExercise>();

            List<MiniGameExercise> directList = result as List<MiniGameExercise>;

            if (directList != null)
                return directList;

            IEnumerable<MiniGameExercise> typedEnumerable = result as IEnumerable<MiniGameExercise>;

            if (typedEnumerable != null)
                return typedEnumerable.ToList();

            IEnumerable enumerable = result as IEnumerable;

            if (enumerable == null)
                return new List<MiniGameExercise>();

            List<MiniGameExercise> exercises = new List<MiniGameExercise>();

            foreach (object item in enumerable)
            {
                MiniGameExercise exercise = item as MiniGameExercise;

                if (exercise != null)
                    exercises.Add(exercise);
            }

            return exercises;
        }

        private List<MiniGameExercise> BuildFallbackWordExercises()
        {
            List<WordItem> words = MiniGameRepository.GetRandomWords(_topic, 12);

            List<MiniGameExercise> exercises = new List<MiniGameExercise>();

            foreach (WordItem word in words)
            {
                exercises.Add(new MiniGameExercise
                {
                    Prompt = "Напиши английский перевод: " + word.Russian,
                    Answer = word.English,
                    Explanation = "Правильный ответ: " + word.English,
                    Word = word
                });
            }

            return exercises;
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerText.Text = "Время: " + _stopwatch.Elapsed.ToString(@"mm\:ss");
            ScoreText.Text = "Верно: " + _correct + " | Ошибок: " + _wrong;
        }

        private void ShowExercise()
        {
            _locked = false;

            if (_index >= _exercises.Count)
            {
                FinishMiniGame();
                return;
            }

            MiniGameExercise exercise = _exercises[_index];

            ProgressBar.Value = _index * 100.0 / _exercises.Count;

            FeedbackText.Text = "";
            GamePanel.Children.Clear();

            TextBlock counter = CreateSmallText("Задание " + (_index + 1) + " из " + _exercises.Count);
            counter.Margin = new Thickness(0, 0, 0, 10);
            GamePanel.Children.Add(counter);

            TextBlock prompt = CreateBigText(exercise.Prompt);
            prompt.Margin = new Thickness(0, 0, 0, 22);
            GamePanel.Children.Add(prompt);

            if (exercise.Options != null && exercise.Options.Count > 0)
            {
                BuildOptionExercise(exercise);
            }
            else
            {
                BuildInputExercise(exercise);
            }
        }

        private void BuildOptionExercise(MiniGameExercise exercise)
        {
            WrapPanel panel = new WrapPanel();
            panel.HorizontalAlignment = HorizontalAlignment.Center;
            panel.MaxWidth = 860;

            foreach (string option in exercise.Options)
            {
                string selectedOption = option;

                Button button = CreateButton(selectedOption, 250);
                button.MinHeight = 54;
                button.Margin = new Thickness(8);
                button.Click += delegate
                {
                    CheckOptionAnswer(exercise, selectedOption);
                };

                panel.Children.Add(button);
            }

            GamePanel.Children.Add(panel);
        }

        private void BuildInputExercise(MiniGameExercise exercise)
        {
            _answerBox = CreateSafeTextBox();
            _answerBox.KeyDown += AnswerBox_KeyDown;

            GamePanel.Children.Add(_answerBox);

            Button check = CreateButton("Проверить", 170);
            check.HorizontalAlignment = HorizontalAlignment.Center;
            check.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
            check.Margin = new Thickness(0, 12, 0, 0);

            check.Click += delegate
            {
                CheckInputAnswer(exercise);
            };

            GamePanel.Children.Add(check);

            Dispatcher.BeginInvoke(new Action(delegate
            {
                _answerBox.Focus();
                Keyboard.Focus(_answerBox);
            }), DispatcherPriority.Input);
        }

        private void StartSpeedTranslation()
        {
            ShowExercise();
        }

        private void SpeedAnswerBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            e.Handled = true;

            if (_index >= _exercises.Count)
                return;

            MiniGameExercise exercise = _exercises[_index];

            if (_speedAnswerBox == null)
                return;

            CheckAnswer(exercise, _speedAnswerBox.Text);
        }

        private void AnswerBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            e.Handled = true;

            if (_index >= _exercises.Count)
                return;

            CheckInputAnswer(_exercises[_index]);
        }

        private void CheckInputAnswer(MiniGameExercise exercise)
        {
            if (_answerBox == null)
                return;

            CheckAnswer(exercise, _answerBox.Text);
        }

        private void CheckOptionAnswer(MiniGameExercise exercise, string selectedOption)
        {
            CheckAnswer(exercise, selectedOption);
        }

        private void CheckAnswer(MiniGameExercise exercise, string userAnswer)
        {
            if (_locked)
                return;

            if (string.IsNullOrWhiteSpace(userAnswer))
            {
                FeedbackText.Foreground = Brushes.Firebrick;
                FeedbackText.Text = "Сначала напиши ответ.";
                return;
            }

            _locked = true;

            if (_answerBox != null)
                _answerBox.IsEnabled = false;

            if (_speedAnswerBox != null)
                _speedAnswerBox.IsEnabled = false;

            _typedChars += userAnswer.Length;

            bool isCorrect = SoftAnswerComparer.IsCorrect(userAnswer, exercise.Answer);

            if (isCorrect)
            {
                _correct++;

                FeedbackText.Foreground = Brushes.ForestGreen;
                FeedbackText.Text = "Правильно!";

                if (exercise.Word != null)
                    ProgressService.MarkWordAsLearned(exercise.Word.English);

                if (_settings.SoundEnabled)
                    SystemSounds.Asterisk.Play();

                GoNextWithDelay(650);
            }
            else
            {
                _wrong++;

                string explanation = exercise.Explanation;

                if (string.IsNullOrWhiteSpace(explanation))
                    explanation = "Правильный ответ: " + exercise.Answer;

                FeedbackText.Foreground = Brushes.Firebrick;
                FeedbackText.Text = "Ошибка. " + explanation;

                AddMistake(exercise, userAnswer);

                if (_settings.SoundEnabled)
                    SystemSounds.Hand.Play();

                GoNextWithDelay(1300);
            }
        }

        private void AddMistake(MiniGameExercise exercise, string userAnswer)
        {
            if (exercise == null)
                return;

            if (exercise.Word != null)
            {
                _mistakes.Add(new MistakeRecord
                {
                    English = exercise.Word.English,
                    Russian = exercise.Word.Russian,
                    Count = 1,
                    LastWrongAnswer = userAnswer,
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
                    LastWrongAnswer = userAnswer,
                    LastPracticed = DateTime.Now
                });
            }
        }

        private void GoNextWithDelay(int milliseconds)
        {
            DispatcherTimer delayTimer = new DispatcherTimer();
            delayTimer.Interval = TimeSpan.FromMilliseconds(milliseconds);
            delayTimer.Tick += delegate
            {
                delayTimer.Stop();

                _index++;
                ShowExercise();
            };
            delayTimer.Start();
        }

        private void FinishMiniGame()
        {
            StopTimer();

            _stopwatch.Stop();

            int total = _correct + _wrong;

            if (total <= 0)
                total = 1;

            GameResult result = new GameResult();
            result.GameName = TitleText.Text;
            result.Topic = _topic;
            result.Level = "Мини-игра";
            result.Mode = _mode.ToString();
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
                ProgressService.MarkPathStepCompleted(_topic, "MiniGames");

            WindowNavigationService.Navigate(this, new ResultsWindow(result));
        }

        private void StopTimer()
        {
            if (_timer != null)
                _timer.Stop();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                FinishMiniGame();
            }
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            FinishMiniGame();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            StopTimer();
            WindowNavigationService.NavigateToMain(this);
        }

        private TextBlock CreateBigText(string text)
        {
            return new TextBlock
            {
                Text = text,
                FontSize = GetFontSize("PromptFontSize", 34),
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                MaxWidth = 840,
                Foreground = GetBrush("DarkTextBrush", Brushes.Black)
            };
        }

        private TextBlock CreateNormalText(string text)
        {
            return new TextBlock
            {
                Text = text,
                FontSize = GetFontSize("NormalFontSize", 17),
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                MaxWidth = 840,
                Foreground = GetBrush("DarkTextBrush", Brushes.Black)
            };
        }

        private TextBlock CreateSmallText(string text)
        {
            return new TextBlock
            {
                Text = text,
                FontSize = GetFontSize("SmallFontSize", 15),
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                MaxWidth = 840,
                Foreground = GetBrush("SecondaryTextBrush", Brushes.Gray)
            };
        }

        private Button CreateButton(string text, double width)
        {
            Button button = new Button
            {
                Width = width,
                MinWidth = Math.Min(width, 90),
                MaxWidth = width,
                MinHeight = 48,
                Margin = new Thickness(4),
                Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue),
                Foreground = Brushes.White,
                FontWeight = FontWeights.SemiBold,
                FontSize = GetFontSize("ButtonFontSize", 15),
                BorderThickness = new Thickness(0),
                Padding = new Thickness(10, 8, 10, 8)
            };

            SetButtonText(button, text);

            return button;
        }

        private void SetButtonText(Button button, string text)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.White,
                FontWeight = FontWeights.SemiBold,
                FontSize = GetFontSize("ButtonFontSize", 15)
            };

            button.Content = textBlock;
        }

        private TextBox CreateSafeTextBox()
        {
            TextBox textBox = new TextBox();

            textBox.Width = 560;
            textBox.MaxWidth = 760;
            textBox.Height = 64;
            textBox.MinHeight = 64;

            textBox.FontSize = GetFontSize("InputFontSize", 24);
            textBox.FontWeight = FontWeights.SemiBold;

            textBox.HorizontalAlignment = HorizontalAlignment.Center;
            textBox.VerticalAlignment = VerticalAlignment.Center;

            textBox.Background = GetBrush("InputBgBrush", Brushes.White);
            textBox.Foreground = GetBrush("InputTextBrush", Brushes.Black);
            textBox.CaretBrush = GetBrush("InputTextBrush", Brushes.Black);
            textBox.BorderBrush = GetBrush("InputBorderBrush", Brushes.DodgerBlue);
            textBox.BorderThickness = new Thickness(2);

            textBox.Padding = new Thickness(16, 8, 16, 8);
            textBox.Margin = new Thickness(0, 0, 0, 8);

            textBox.TextWrapping = TextWrapping.NoWrap;
            textBox.AcceptsReturn = false;
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.HorizontalContentAlignment = HorizontalAlignment.Left;

            return textBox;
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