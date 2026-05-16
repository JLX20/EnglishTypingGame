using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace EnglishTypingGame
{
    public partial class GameWindow : Window
    {
        private readonly string _topic;
        private readonly string _mode;
        private readonly string _level;
        private readonly GameInputMode _inputMode;
        private readonly SettingsData _settings;
        private readonly Stopwatch _stopwatch;
        private readonly Random _random;

        private List<WordItem> _words;
        private List<MistakeRecord> _mistakes;

        private DispatcherTimer _timer;

        private int _index;
        private int _correct;
        private int _wrong;
        private int _typedChars;

        private bool _answerLocked;
        private GameInputMode _currentQuestionMode;

        public GameWindow(string topic, string mode)
            : this(topic, mode, "Все уровни", GameInputMode.RussianToEnglish)
        {
        }

        public GameWindow(string topic, string mode, string level, GameInputMode inputMode)
        {
            InitializeComponent();

            _topic = topic;
            _mode = mode;
            _level = level;
            _inputMode = inputMode;

            _settings = SettingsService.Load();
            _stopwatch = new Stopwatch();
            _random = new Random(Guid.NewGuid().GetHashCode());

            _words = new List<WordItem>();
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
            LoadWords();

            if (_words.Count == 0)
            {
                MessageBox.Show("Нет слов для выбранной темы и сложности.");
                WindowNavigationService.NavigateToMain(this);
                return;
            }

            ModeTitleText.Text = BuildWindowTitle();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(300);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            _stopwatch.Start();

            ShowQuestion();
        }

        private string BuildWindowTitle()
        {
            if (_mode == "Mistakes")
                return "Тренировка ошибок";

            if (_inputMode == GameInputMode.EnglishToRussian)
                return "Обычная игра: английский → русский";

            if (_inputMode == GameInputMode.ExampleToWord)
                return "Обычная игра: пример → слово";

            if (_inputMode == GameInputMode.Mixed)
                return "Обычная игра: смешанный режим";

            return "Обычная игра: русский → английский";
        }

        private void LoadWords()
        {
            if (_mode == "Mistakes")
            {
                ProgressData progress = ProgressService.Load();

                if (progress.Mistakes != null)
                {
                    _words = progress.Mistakes
                        .Select(m => new WordItem(
                            m.English,
                            m.Russian,
                            "Mistakes",
                            "Review",
                            "Повтори это слово."))
                        .GroupBy(w => w.English.ToLower())
                        .Select(g => g.First())
                        .OrderBy(w => Guid.NewGuid())
                        .ToList();
                }
            }
            else
            {
                _words = LessonQueryService.GetWords(_topic, _level)
                    .OrderBy(w => Guid.NewGuid())
                    .Take(_settings.WordsPerRound)
                    .ToList();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerText.Text = "Время: " + _stopwatch.Elapsed.ToString(@"mm\:ss");
            ScoreText.Text = "Верно: " + _correct + " | Ошибок: " + _wrong;
        }

        private void ShowQuestion()
        {
            _answerLocked = false;

            if (_index >= _words.Count)
            {
                FinishGame();
                return;
            }

            WordItem word = _words[_index];

            _currentQuestionMode = GetQuestionMode();

            ProgressText.Text =
                "Слово " + (_index + 1) + " из " + _words.Count +
                " | Тема: " + _topic +
                " | Сложность: " + _level;

            RoundProgressBar.Value = _index * 100.0 / _words.Count;

            PromptTitleText.Text = BuildPromptTitle();
            BigPromptText.Text = BuildPrompt(word);

            InputBox.IsEnabled = true;
            InputBox.Clear();
            InputBox.Background = GetBrush("InputBgBrush", Brushes.White);
            InputBox.Foreground = GetBrush("InputTextBrush", Brushes.Black);
            InputBox.CaretBrush = GetBrush("InputTextBrush", Brushes.Black);
            InputBox.BorderBrush = GetBrush("InputBorderBrush", Brushes.LightGray);

            FeedbackText.Text = "";
            WordPreviewText.Inlines.Clear();

            BuildPreview("", GetMainCorrectAnswer(word));

            Dispatcher.BeginInvoke(new Action(delegate
            {
                InputBox.Focus();
                Keyboard.Focus(InputBox);
            }), DispatcherPriority.Input);
        }

        private GameInputMode GetQuestionMode()
        {
            if (_inputMode != GameInputMode.Mixed)
                return _inputMode;

            int value = _random.Next(0, 3);

            if (value == 0)
                return GameInputMode.RussianToEnglish;

            if (value == 1)
                return GameInputMode.EnglishToRussian;

            return GameInputMode.ExampleToWord;
        }

        private string BuildPromptTitle()
        {
            if (_currentQuestionMode == GameInputMode.EnglishToRussian)
                return "Напиши русский перевод:";

            if (_currentQuestionMode == GameInputMode.ExampleToWord)
                return "Вставь пропущенное английское слово:";

            return "Напиши английский перевод:";
        }

        private string BuildPrompt(WordItem word)
        {
            if (_currentQuestionMode == GameInputMode.EnglishToRussian)
                return word.English;

            if (_currentQuestionMode == GameInputMode.ExampleToWord)
            {
                if (!string.IsNullOrWhiteSpace(word.Example) &&
                    word.Example.ToLower().Contains(word.English.ToLower()))
                {
                    return ReplaceIgnoreCase(word.Example, word.English, "_____");
                }

                return "_____ = " + word.Russian;
            }

            return word.Russian;
        }

        private string GetMainCorrectAnswer(WordItem word)
        {
            if (_currentQuestionMode == GameInputMode.EnglishToRussian)
                return word.Russian;

            return word.English;
        }

        private List<string> GetCorrectAnswers(WordItem word)
        {
            if (_currentQuestionMode == GameInputMode.EnglishToRussian)
                return SoftAnswerComparer.SplitPossibleAnswers(word.Russian);

            return SoftAnswerComparer.SplitPossibleAnswers(word.English);
        }

        private string ReplaceIgnoreCase(string source, string oldValue, string newValue)
        {
            int index = source.ToLower().IndexOf(oldValue.ToLower());

            if (index < 0)
                return source;

            return source.Substring(0, index) + newValue + source.Substring(index + oldValue.Length);
        }

        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_index >= _words.Count)
                return;

            WordItem word = _words[_index];
            string answer = InputBox.Text;

            BuildPreview(answer, GetMainCorrectAnswer(word));
        }

        private void BuildPreview(string typedText, string correctText)
        {
            WordPreviewText.Inlines.Clear();

            if (correctText == null)
                correctText = "";

            if (typedText == null)
                typedText = "";

            int maxLength = Math.Max(typedText.Length, correctText.Length);

            if (maxLength == 0)
                return;

            for (int i = 0; i < maxLength; i++)
            {
                if (i < typedText.Length)
                {
                    char typedChar = typedText[i];

                    bool isCorrect =
                        i < correctText.Length &&
                        char.ToLowerInvariant(typedChar) == char.ToLowerInvariant(correctText[i]);

                    Run run = new Run(typedChar.ToString());

                    run.Foreground = isCorrect ? Brushes.ForestGreen : Brushes.Firebrick;

                    WordPreviewText.Inlines.Add(run);
                }
                else
                {
                    Run run = new Run("_");
                    run.Foreground = Brushes.Gray;
                    WordPreviewText.Inlines.Add(run);
                }

                WordPreviewText.Inlines.Add(new Run(" "));
            }
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                CheckAnswer();
            }
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer();
        }

        private void DontKnowButton_Click(object sender, RoutedEventArgs e)
        {
            if (_answerLocked)
                return;

            if (_index >= _words.Count)
                return;

            WordItem word = _words[_index];

            _answerLocked = true;
            InputBox.IsEnabled = false;

            _wrong++;

            string correct = GetMainCorrectAnswer(word);

            FeedbackText.Foreground = Brushes.Firebrick;
            FeedbackText.Text = "Правильный ответ: " + correct;

            AddMistake(word, "Не знаю");

            if (_settings.SoundEnabled)
                SystemSounds.Hand.Play();

            _index++;
            GoNextWithDelay(1200);
        }

        private void CheckAnswer()
        {
            if (_answerLocked)
                return;

            if (_index >= _words.Count)
                return;

            WordItem word = _words[_index];
            string answer = InputBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(answer))
            {
                FeedbackText.Foreground = Brushes.Firebrick;
                FeedbackText.Text = "Сначала напиши ответ или нажми “Не знаю”.";
                InputBox.Focus();
                return;
            }

            _answerLocked = true;
            InputBox.IsEnabled = false;

            _typedChars += answer.Length;

            bool isCorrect = SoftAnswerComparer.IsCorrect(answer, GetCorrectAnswers(word));

            if (isCorrect)
            {
                _correct++;

                FeedbackText.Foreground = Brushes.ForestGreen;
                FeedbackText.Text = "Правильно!";

                ProgressService.MarkWordAsLearned(word.English);

                if (_settings.SoundEnabled)
                    SystemSounds.Asterisk.Play();

                _index++;
                GoNextWithDelay(500);
            }
            else
            {
                _wrong++;

                string correct = GetMainCorrectAnswer(word);

                FeedbackText.Foreground = Brushes.Firebrick;
                FeedbackText.Text = "Ошибка. Правильно: " + correct;

                BuildPreview(answer, correct);
                AddMistake(word, answer);

                if (_settings.SoundEnabled)
                    SystemSounds.Hand.Play();

                _index++;
                GoNextWithDelay(1200);
            }
        }

        private void AddMistake(WordItem word, string answer)
        {
            _mistakes.Add(new MistakeRecord
            {
                English = word.English,
                Russian = word.Russian,
                Count = 1,
                LastWrongAnswer = answer,
                LastPracticed = DateTime.Now
            });
        }

        private void GoNextWithDelay(int milliseconds)
        {
            DispatcherTimer delayTimer = new DispatcherTimer();
            delayTimer.Interval = TimeSpan.FromMilliseconds(milliseconds);
            delayTimer.Tick += delegate
            {
                delayTimer.Stop();
                ShowQuestion();
            };
            delayTimer.Start();
        }

        private void FinishGame()
        {
            StopTimer();

            _stopwatch.Stop();

            int total = _correct + _wrong;

            if (total <= 0)
                total = 1;

            GameResult result = new GameResult();
            result.GameName = BuildWindowTitle();
            result.Topic = _topic;
            result.Level = _level;
            result.Mode = _inputMode.ToString();
            result.TotalWords = total;
            result.CorrectWords = _correct;
            result.WrongWords = _wrong;
            result.Duration = _stopwatch.Elapsed;
            result.Mistakes = _mistakes;
            result.Accuracy = _correct * 100.0 / total;

            double minutes = Math.Max(_stopwatch.Elapsed.TotalMinutes, 0.1);
            result.Wpm = (_typedChars / 5.0) / minutes;

            ProgressService.ApplyResult(result);

            if (_mode != "Mistakes" && result.Accuracy >= 70)
                ProgressService.MarkPathStepCompleted(_topic, "Game");

            WindowNavigationService.Navigate(this, new ResultsWindow(result));
        }

        private void StopTimer()
        {
            if (_timer != null)
                _timer.Stop();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            StopTimer();
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
    }
}