using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace EnglishTypingGame
{
    public partial class MiniGameWindow : Window
    {
        private readonly string _topic;
        private readonly MiniGameMode _mode;
        private readonly SettingsData _settings;
        private readonly Stopwatch _stopwatch;
        private readonly Random _random;

        private List<MiniGameExercise> _exercises;
        private List<MistakeRecord> _mistakes;

        private int _index;
        private int _correct;
        private int _wrong;
        private int _submittedChars;
        private bool _isFinishing;
        private bool _answerLocked;

        private TextBox _answerBox;
        private TextBlock _answerPreview;
        private WrapPanel _dynamicPanel;
        private List<string> _selectedWords;

        private DispatcherTimer _timer;

        private List<Button> _memoryButtons;
        private Button _memoryFirstButton;
        private Button _memorySecondButton;
        private int _memoryPairsFound;
        private bool _memoryLocked;

        private Button _matchEnglishButton;
        private Button _matchRussianButton;
        private int _matchFound;

        private Canvas _letterCanvas;
        private DispatcherTimer _letterMoveTimer;
        private DispatcherTimer _letterSpawnTimer;
        private WordItem _letterCurrentWord;
        private int _letterIndex;
        private int _letterLives;
        private int _letterWordsDone;
        private List<Button> _fallingLetters;

        private bool _speedMode;
        private DispatcherTimer _speedTimer;
        private int _speedSecondsLeft;
        private List<WordItem> _speedWords;
        private WordItem _speedCurrentWord;
        private TextBlock _speedPromptText;
        private TextBlock _speedHintText;
        private TextBox _speedAnswerBox;
        private bool _speedAnswerLocked;

        public MiniGameWindow(string topic, MiniGameMode mode)
        {
            InitializeComponent();

            _topic = topic;
            _mode = mode;
            _settings = SettingsService.Load();
            _stopwatch = new Stopwatch();
            _random = new Random(Guid.NewGuid().GetHashCode());

            _mistakes = new List<MistakeRecord>();
            _selectedWords = new List<string>();
            _fallingLetters = new List<Button>();
            _speedWords = new List<WordItem>();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            StopSpecialTimers();

            base.OnClosing(e);

            WindowNavigationService.HandleCloseToMain(this, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ThemeService.ApplyTheme(_settings.ThemeName, _settings.BackgroundName, _settings.TextSizeName);

            MiniGameInfo info = MiniGameRepository.GetInfo(_mode);

            if (info != null)
            {
                TitleText.Text = info.Title;
                DescriptionText.Text = info.Description;
            }
            else
            {
                TitleText.Text = "Мини-игра";
                DescriptionText.Text = "";
            }

            _stopwatch.Start();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(300);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            if (_mode == MiniGameMode.MemoryPairs)
            {
                StartMemoryPairs();
                return;
            }

            if (_mode == MiniGameMode.TranslationMatch)
            {
                StartTranslationMatch();
                return;
            }

            if (_mode == MiniGameMode.LetterRain)
            {
                StartLetterRain();
                return;
            }

            if (_mode == MiniGameMode.SpeedTranslation)
            {
                StartSpeedTranslation();
                return;
            }

            _exercises = MiniGameRepository.GetExercises(_mode, _topic);

            if (_exercises == null || _exercises.Count == 0)
            {
                MessageBox.Show("Для этой мини-игры нет заданий.");
                WindowNavigationService.NavigateToMain(this);
                return;
            }

            ShowCurrentExercise();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_speedMode)
            {
                TimerText.Text = "Осталось: " + _speedSecondsLeft + " сек.";
            }
            else
            {
                TimerText.Text = "Время: " + _stopwatch.Elapsed.ToString(@"mm\:ss");
            }

            ScoreText.Text = "Верно: " + _correct + " | Ошибок: " + _wrong;
        }

        private void ShowCurrentExercise()
        {
            _answerLocked = false;

            if (_index >= _exercises.Count)
            {
                FinishGame();
                return;
            }

            GamePanel.Children.Clear();
            FeedbackText.Text = "";
            _selectedWords.Clear();

            MiniGameExercise exercise = _exercises[_index];

            ProgressBar.Value = _index * 100.0 / _exercises.Count;

            TextBlock counter = CreateSmallText("Задание " + (_index + 1) + " из " + _exercises.Count);
            counter.HorizontalAlignment = HorizontalAlignment.Center;
            counter.Margin = new Thickness(0, 0, 0, 14);
            GamePanel.Children.Add(counter);

            if (_mode == MiniGameMode.WordCards)
            {
                BuildWordCard(exercise);
                return;
            }

            TextBlock prompt = CreateBigText(exercise.Prompt);
            prompt.HorizontalAlignment = HorizontalAlignment.Center;
            prompt.TextAlignment = TextAlignment.Center;
            prompt.Margin = new Thickness(0, 0, 0, 24);
            GamePanel.Children.Add(prompt);

            if (exercise.Options != null && exercise.Options.Count > 0)
            {
                BuildOptionsExercise(exercise);
                return;
            }

            if (exercise.WordsToArrange != null && exercise.WordsToArrange.Count > 0)
            {
                BuildArrangeExercise(exercise);
                return;
            }

            BuildInputExercise(exercise);
        }

        private void BuildWordCard(MiniGameExercise exercise)
        {
            TextBlock english = CreateBigText(exercise.Prompt);
            english.FontSize = GetFontSize("WordFontSize", 54);
            english.HorizontalAlignment = HorizontalAlignment.Center;
            english.Margin = new Thickness(0, 16, 0, 8);
            GamePanel.Children.Add(english);

            TextBlock russian = CreateBigText(exercise.RussianPrompt);
            russian.FontSize = GetFontSize("PromptFontSize", 34);
            russian.Foreground = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
            russian.HorizontalAlignment = HorizontalAlignment.Center;
            russian.Margin = new Thickness(0, 0, 0, 18);
            GamePanel.Children.Add(russian);

            Border exampleBox = new Border();
            exampleBox.Background = GetBrush("SoftBgBrush", Brushes.LightGray);
            exampleBox.CornerRadius = new CornerRadius(24);
            exampleBox.Padding = new Thickness(20);
            exampleBox.Margin = new Thickness(0, 0, 0, 24);
            exampleBox.MaxWidth = 820;

            TextBlock example = CreateNormalText("Пример: " + exercise.Explanation);
            example.TextAlignment = TextAlignment.Center;
            exampleBox.Child = example;

            GamePanel.Children.Add(exampleBox);

            WrapPanel buttons = new WrapPanel();
            buttons.HorizontalAlignment = HorizontalAlignment.Center;

            Button knowButton = CreateButton("Знаю", 150);
            knowButton.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
            knowButton.Click += delegate
            {
                ProgressService.MarkWordAsLearned(exercise.Answer);
                RegisterAnswer(true, exercise, exercise.Answer);
            };

            Button repeatButton = CreateButton("Повторить позже", 210);
            repeatButton.Background = GetBrush("ButtonNeutralBrush", Brushes.Gray);
            repeatButton.Click += delegate
            {
                RegisterAnswer(false, exercise, "");
            };

            buttons.Children.Add(knowButton);
            buttons.Children.Add(repeatButton);

            GamePanel.Children.Add(buttons);
        }

        private void BuildOptionsExercise(MiniGameExercise exercise)
        {
            WrapPanel panel = new WrapPanel();
            panel.HorizontalAlignment = HorizontalAlignment.Center;
            panel.Margin = new Thickness(0, 0, 0, 20);
            panel.MaxWidth = 840;

            foreach (string option in exercise.Options)
            {
                string selectedOption = option;

                Button button = CreateButton(selectedOption, 250);
                button.MinHeight = 62;
                button.Margin = new Thickness(8);
                button.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);

                button.Click += delegate
                {
                    if (_answerLocked)
                        return;

                    _answerLocked = true;

                    bool isCorrect = Normalize(selectedOption) == Normalize(exercise.Answer);
                    RegisterAnswer(isCorrect, exercise, selectedOption);
                };

                panel.Children.Add(button);
            }

            GamePanel.Children.Add(panel);
        }

        private void BuildArrangeExercise(MiniGameExercise exercise)
        {
            _answerPreview = CreateBigText("");
            _answerPreview.FontSize = GetFontSize("PreviewFontSize", 30);
            _answerPreview.HorizontalAlignment = HorizontalAlignment.Center;
            _answerPreview.TextAlignment = TextAlignment.Center;
            _answerPreview.TextWrapping = TextWrapping.Wrap;
            _answerPreview.MaxWidth = 800;
            _answerPreview.Margin = new Thickness(0, 0, 0, 20);

            Border answerBox = new Border();
            answerBox.Background = GetBrush("SoftBgBrush", Brushes.LightGray);
            answerBox.CornerRadius = new CornerRadius(24);
            answerBox.Padding = new Thickness(18);
            answerBox.MinHeight = 78;
            answerBox.MaxWidth = 840;
            answerBox.Child = _answerPreview;

            GamePanel.Children.Add(answerBox);

            TextBlock hint = CreateSmallText(
                _mode == MiniGameMode.WordBuilder
                    ? "Нажимай буквы в правильном порядке."
                    : "Нажимай слова в правильном порядке.");

            hint.HorizontalAlignment = HorizontalAlignment.Center;
            hint.TextAlignment = TextAlignment.Center;
            hint.TextWrapping = TextWrapping.Wrap;
            hint.Margin = new Thickness(0, 8, 0, 12);
            GamePanel.Children.Add(hint);

            _dynamicPanel = new WrapPanel();
            _dynamicPanel.HorizontalAlignment = HorizontalAlignment.Center;
            _dynamicPanel.MaxWidth = 850;
            _dynamicPanel.Margin = new Thickness(0, 10, 0, 20);

            foreach (string part in exercise.WordsToArrange)
            {
                string selectedPart = part;

                double buttonWidth = _mode == MiniGameMode.WordBuilder ? 66 : 160;

                if (selectedPart.Length > 10 && _mode != MiniGameMode.WordBuilder)
                    buttonWidth = 220;

                if (selectedPart.Length > 18 && _mode != MiniGameMode.WordBuilder)
                    buttonWidth = 280;

                Button button = CreateButton(selectedPart, buttonWidth);
                button.Height = _mode == MiniGameMode.WordBuilder ? 58 : 62;
                button.FontSize = _mode == MiniGameMode.WordBuilder
                    ? GetFontSize("InputFontSize", 22)
                    : GetFontSize("ButtonFontSize", 15);

                button.Margin = new Thickness(6);
                button.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);

                button.Click += delegate
                {
                    _selectedWords.Add(selectedPart);
                    button.IsEnabled = false;
                    UpdateArrangePreview();
                };

                _dynamicPanel.Children.Add(button);
            }

            GamePanel.Children.Add(_dynamicPanel);

            WrapPanel buttons = new WrapPanel();
            buttons.HorizontalAlignment = HorizontalAlignment.Center;

            Button check = CreateButton("Проверить", 160);
            check.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
            check.Click += delegate
            {
                if (_answerLocked)
                    return;

                _answerLocked = true;

                string answer;

                if (_mode == MiniGameMode.WordBuilder)
                {
                    answer = string.Join("", _selectedWords);
                    bool isCorrect = Normalize(answer) == Normalize(exercise.Answer);
                    RegisterAnswer(isCorrect, exercise, answer);
                }
                else
                {
                    answer = string.Join(" ", _selectedWords);
                    bool isCorrect = NormalizeSentence(answer) == NormalizeSentence(exercise.Answer);
                    RegisterAnswer(isCorrect, exercise, answer);
                }
            };

            Button reset = CreateButton("Сбросить", 140);
            reset.Background = GetBrush("ButtonNeutralBrush", Brushes.Gray);
            reset.Click += delegate
            {
                foreach (object child in _dynamicPanel.Children)
                {
                    Button b = child as Button;

                    if (b != null)
                        b.IsEnabled = true;
                }

                _answerLocked = false;
                _selectedWords.Clear();
                UpdateArrangePreview();
            };

            buttons.Children.Add(check);
            buttons.Children.Add(reset);

            GamePanel.Children.Add(buttons);
        }

        private void UpdateArrangePreview()
        {
            if (_answerPreview == null)
                return;

            if (_mode == MiniGameMode.WordBuilder)
            {
                _answerPreview.Text = string.Join(" ", _selectedWords);
            }
            else
            {
                _answerPreview.Text = string.Join(" ", _selectedWords);
            }
        }

        private void BuildInputExercise(MiniGameExercise exercise)
        {
            _answerBox = new TextBox();
            _answerBox.Width = 520;
            _answerBox.MaxWidth = 780;
            _answerBox.Height = 64;
            _answerBox.FontSize = GetFontSize("InputFontSize", 24);
            _answerBox.FontWeight = FontWeights.SemiBold;
            _answerBox.HorizontalAlignment = HorizontalAlignment.Center;
            _answerBox.Margin = new Thickness(0, 0, 0, 18);
            _answerBox.Background = GetBrush("InputBgBrush", Brushes.White);
            _answerBox.Foreground = GetBrush("InputTextBrush", Brushes.Black);
            _answerBox.CaretBrush = GetBrush("InputTextBrush", Brushes.Black);
            _answerBox.BorderBrush = GetBrush("InputBorderBrush", Brushes.LightGray);
            _answerBox.BorderThickness = new Thickness(2);
            _answerBox.Padding = new Thickness(14, 8, 14, 8);
            _answerBox.KeyDown += AnswerBox_KeyDown;

            GamePanel.Children.Add(_answerBox);

            Button check = CreateButton("Проверить", 170);
            check.HorizontalAlignment = HorizontalAlignment.Center;
            check.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
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

        private void AnswerBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && _index < _exercises.Count)
            {
                e.Handled = true;
                CheckInputAnswer(_exercises[_index]);
            }
        }

        private void CheckInputAnswer(MiniGameExercise exercise)
        {
            if (_answerLocked)
                return;

            if (_answerBox == null)
                return;

            string answer = _answerBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(answer))
            {
                FeedbackText.Foreground = Brushes.Firebrick;
                FeedbackText.Text = "Сначала напиши ответ.";
                _answerBox.Focus();
                return;
            }

            _answerLocked = true;
            _answerBox.IsEnabled = false;

            bool isCorrect = Normalize(answer) == Normalize(exercise.Answer);

            RegisterAnswer(isCorrect, exercise, answer);
        }

        private async void RegisterAnswer(bool isCorrect, MiniGameExercise exercise, string userAnswer)
        {
            if (isCorrect)
            {
                _correct++;
                FeedbackText.Foreground = Brushes.ForestGreen;
                FeedbackText.Text = "Правильно!";

                if (_settings.SoundEnabled)
                    SystemSounds.Asterisk.Play();
            }
            else
            {
                _wrong++;
                FeedbackText.Foreground = Brushes.Firebrick;
                FeedbackText.Text = "Ошибка. " + exercise.Explanation;

                AddMistakeFromExercise(exercise, userAnswer);

                if (_settings.SoundEnabled)
                    SystemSounds.Hand.Play();
            }

            if (userAnswer != null)
                _submittedChars += userAnswer.Length;

            await Task.Delay(isCorrect ? 550 : 1300);

            _index++;
            ShowCurrentExercise();
        }

        private void AddMistakeFromExercise(MiniGameExercise exercise, string userAnswer)
        {
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

        // =========================================================
        // SPEED TRANSLATION
        // =========================================================

        private void StartSpeedTranslation()
        {
            _speedMode = true;
            _speedSecondsLeft = 60;
            _speedAnswerLocked = false;

            _speedWords = MiniGameRepository.GetRandomWords(_topic, 50);

            if (_speedWords == null || _speedWords.Count == 0)
            {
                MessageBox.Show("Нет слов для Speed Translation.");
                WindowNavigationService.NavigateToMain(this);
                return;
            }

            GamePanel.Children.Clear();
            FeedbackText.Text = "";
            ProgressBar.Value = 0;

            TextBlock title = CreateBigText("Speed Translation");
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.TextAlignment = TextAlignment.Center;
            title.TextWrapping = TextWrapping.Wrap;
            title.MaxWidth = 820;
            title.Margin = new Thickness(0, 0, 0, 14);
            GamePanel.Children.Add(title);

            TextBlock instruction = CreateNormalText("Переводи русское слово на английский. Нажимай Enter после ответа.");
            instruction.HorizontalAlignment = HorizontalAlignment.Center;
            instruction.TextAlignment = TextAlignment.Center;
            instruction.TextWrapping = TextWrapping.Wrap;
            instruction.MaxWidth = 820;
            instruction.Margin = new Thickness(0, 0, 0, 24);
            GamePanel.Children.Add(instruction);

            Border promptBox = new Border();
            promptBox.Background = GetBrush("SoftBgBrush", Brushes.LightGray);
            promptBox.CornerRadius = new CornerRadius(26);
            promptBox.Padding = new Thickness(24);
            promptBox.Margin = new Thickness(0, 0, 0, 22);
            promptBox.MaxWidth = 780;
            promptBox.HorizontalAlignment = HorizontalAlignment.Center;

            _speedPromptText = CreateBigText("");
            _speedPromptText.FontSize = GetFontSize("PromptFontSize", 42);
            _speedPromptText.HorizontalAlignment = HorizontalAlignment.Center;
            _speedPromptText.TextAlignment = TextAlignment.Center;
            _speedPromptText.TextWrapping = TextWrapping.Wrap;
            _speedPromptText.MaxWidth = 740;

            promptBox.Child = _speedPromptText;
            GamePanel.Children.Add(promptBox);

            _speedHintText = CreateSmallText("");
            _speedHintText.HorizontalAlignment = HorizontalAlignment.Center;
            _speedHintText.TextAlignment = TextAlignment.Center;
            _speedHintText.TextWrapping = TextWrapping.Wrap;
            _speedHintText.MaxWidth = 760;
            _speedHintText.Margin = new Thickness(0, 0, 0, 12);
            GamePanel.Children.Add(_speedHintText);

            _speedAnswerBox = new TextBox();
            _speedAnswerBox.Width = 520;
            _speedAnswerBox.MaxWidth = 780;
            _speedAnswerBox.Height = 64;
            _speedAnswerBox.FontSize = GetFontSize("InputFontSize", 24);
            _speedAnswerBox.FontWeight = FontWeights.SemiBold;
            _speedAnswerBox.HorizontalAlignment = HorizontalAlignment.Center;
            _speedAnswerBox.Background = GetBrush("InputBgBrush", Brushes.White);
            _speedAnswerBox.Foreground = GetBrush("InputTextBrush", Brushes.Black);
            _speedAnswerBox.CaretBrush = GetBrush("InputTextBrush", Brushes.Black);
            _speedAnswerBox.BorderBrush = GetBrush("InputBorderBrush", Brushes.LightGray);
            _speedAnswerBox.BorderThickness = new Thickness(2);
            _speedAnswerBox.Padding = new Thickness(14, 8, 14, 8);
            _speedAnswerBox.KeyDown += SpeedAnswerBox_KeyDown;

            GamePanel.Children.Add(_speedAnswerBox);

            Button skipButton = CreateButton("Пропустить", 160);
            skipButton.Background = GetBrush("ButtonNeutralBrush", Brushes.Gray);
            skipButton.HorizontalAlignment = HorizontalAlignment.Center;
            skipButton.Margin = new Thickness(0, 18, 0, 0);
            skipButton.Click += delegate
            {
                CheckSpeedAnswer(true);
            };

            GamePanel.Children.Add(skipButton);

            _speedTimer = new DispatcherTimer();
            _speedTimer.Interval = TimeSpan.FromSeconds(1);
            _speedTimer.Tick += SpeedTimer_Tick;
            _speedTimer.Start();

            NextSpeedWord();

            Dispatcher.BeginInvoke(new Action(delegate
            {
                _speedAnswerBox.Focus();
                Keyboard.Focus(_speedAnswerBox);
            }), DispatcherPriority.Input);
        }

        private void SpeedTimer_Tick(object sender, EventArgs e)
        {
            _speedSecondsLeft--;

            if (_speedSecondsLeft <= 0)
            {
                FinishGame();
                return;
            }

            double passed = 60 - _speedSecondsLeft;
            ProgressBar.Value = passed * 100.0 / 60.0;
        }

        private void NextSpeedWord()
        {
            _speedAnswerLocked = false;

            if (_speedWords == null || _speedWords.Count == 0)
                return;

            _speedCurrentWord = _speedWords[_random.Next(_speedWords.Count)];

            _speedPromptText.Text = _speedCurrentWord.Russian;
            _speedHintText.Text = "Тема: " + _speedCurrentWord.Topic + " | Букв: " + _speedCurrentWord.English.Length;

            _speedAnswerBox.IsEnabled = true;
            _speedAnswerBox.Clear();

            Dispatcher.BeginInvoke(new Action(delegate
            {
                _speedAnswerBox.Focus();
                Keyboard.Focus(_speedAnswerBox);
            }), DispatcherPriority.Input);
        }

        private void SpeedAnswerBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                CheckSpeedAnswer(false);
            }
        }

        private async void CheckSpeedAnswer(bool skipped)
        {
            if (_speedAnswerLocked)
                return;

            if (_speedCurrentWord == null)
                return;

            _speedAnswerLocked = true;

            string answer = _speedAnswerBox.Text.Trim();

            if (skipped)
                answer = "";

            _speedAnswerBox.IsEnabled = false;

            bool isCorrect = !skipped &&
                             Normalize(answer) == Normalize(_speedCurrentWord.English);

            if (isCorrect)
            {
                _correct++;
                _submittedChars += answer.Length;

                FeedbackText.Foreground = Brushes.ForestGreen;
                FeedbackText.Text = "Правильно!";

                if (_settings.SoundEnabled)
                    SystemSounds.Asterisk.Play();

                await Task.Delay(250);
                NextSpeedWord();
            }
            else
            {
                _wrong++;

                _mistakes.Add(new MistakeRecord
                {
                    English = _speedCurrentWord.English,
                    Russian = _speedCurrentWord.Russian,
                    Count = 1,
                    LastWrongAnswer = answer,
                    LastPracticed = DateTime.Now
                });

                FeedbackText.Foreground = Brushes.Firebrick;
                FeedbackText.Text = "Правильно: " + _speedCurrentWord.English;

                if (_settings.SoundEnabled)
                    SystemSounds.Hand.Play();

                await Task.Delay(650);
                NextSpeedWord();
            }
        }

        // =========================================================
        // MEMORY PAIRS
        // =========================================================

        private void StartMemoryPairs()
        {
            GamePanel.Children.Clear();
            FeedbackText.Text = "";

            List<WordItem> words = MiniGameRepository.GetRandomWords(_topic, 6);

            _memoryButtons = new List<Button>();
            _memoryFirstButton = null;
            _memorySecondButton = null;
            _memoryPairsFound = 0;
            _memoryLocked = false;

            ProgressBar.Value = 0;

            TextBlock title = CreateBigText("Найди пары: English + Russian");
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.TextAlignment = TextAlignment.Center;
            title.Margin = new Thickness(0, 0, 0, 20);
            GamePanel.Children.Add(title);

            UniformGrid grid = new UniformGrid();
            grid.Columns = 3;
            grid.Rows = 4;
            grid.MaxWidth = 820;
            grid.HorizontalAlignment = HorizontalAlignment.Center;

            List<Tuple<string, string, WordItem>> cards = new List<Tuple<string, string, WordItem>>();

            foreach (WordItem word in words)
            {
                cards.Add(new Tuple<string, string, WordItem>(word.English, word.English, word));
                cards.Add(new Tuple<string, string, WordItem>(word.Russian, word.English, word));
            }

            cards = cards.OrderBy(c => _random.Next()).ToList();

            foreach (Tuple<string, string, WordItem> card in cards)
            {
                Button button = CreateButton("?", 240);
                button.Height = 92;
                button.Margin = new Thickness(8);
                button.Tag = card;
                button.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
                button.Click += MemoryButton_Click;

                _memoryButtons.Add(button);
                grid.Children.Add(button);
            }

            GamePanel.Children.Add(grid);
        }

        private async void MemoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (_memoryLocked)
                return;

            Button button = sender as Button;

            if (button == null || !button.IsEnabled)
                return;

            Tuple<string, string, WordItem> card = button.Tag as Tuple<string, string, WordItem>;

            if (card == null)
                return;

            SetButtonText(button, card.Item1);
            button.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);

            if (_memoryFirstButton == null)
            {
                _memoryFirstButton = button;
                return;
            }

            if (_memoryFirstButton == button)
                return;

            _memorySecondButton = button;
            _memoryLocked = true;

            Tuple<string, string, WordItem> first = _memoryFirstButton.Tag as Tuple<string, string, WordItem>;
            Tuple<string, string, WordItem> second = _memorySecondButton.Tag as Tuple<string, string, WordItem>;

            bool isPair = first != null && second != null && first.Item2 == second.Item2;

            if (isPair)
            {
                _correct++;
                _memoryPairsFound++;

                _memoryFirstButton.IsEnabled = false;
                _memorySecondButton.IsEnabled = false;

                _memoryFirstButton.Background = GetBrush("ButtonSuccessBrush", Brushes.ForestGreen);
                _memorySecondButton.Background = GetBrush("ButtonSuccessBrush", Brushes.ForestGreen);

                FeedbackText.Foreground = Brushes.ForestGreen;
                FeedbackText.Text = "Пара найдена!";

                if (_settings.SoundEnabled)
                    SystemSounds.Asterisk.Play();
            }
            else
            {
                _wrong++;

                FeedbackText.Foreground = Brushes.Firebrick;
                FeedbackText.Text = "Не пара. Попробуй ещё.";

                if (first != null)
                {
                    _mistakes.Add(new MistakeRecord
                    {
                        English = first.Item3.English,
                        Russian = first.Item3.Russian,
                        Count = 1,
                        LastWrongAnswer = second == null ? "" : second.Item1,
                        LastPracticed = DateTime.Now
                    });
                }

                if (_settings.SoundEnabled)
                    SystemSounds.Hand.Play();

                await Task.Delay(900);

                SetButtonText(_memoryFirstButton, "?");
                SetButtonText(_memorySecondButton, "?");

                _memoryFirstButton.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
                _memorySecondButton.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
            }

            _memoryFirstButton = null;
            _memorySecondButton = null;
            _memoryLocked = false;

            ProgressBar.Value = _memoryPairsFound * 100.0 / 6.0;

            if (_memoryPairsFound >= 6)
            {
                await Task.Delay(700);
                FinishGame();
            }
        }

        // =========================================================
        // TRANSLATION MATCH
        // =========================================================

        private void StartTranslationMatch()
        {
            GamePanel.Children.Clear();
            FeedbackText.Text = "";

            List<WordItem> words = MiniGameRepository.GetRandomWords(_topic, 6);

            _matchEnglishButton = null;
            _matchRussianButton = null;
            _matchFound = 0;

            TextBlock title = CreateBigText("Соедини английское слово с переводом");
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.TextAlignment = TextAlignment.Center;
            title.Margin = new Thickness(0, 0, 0, 20);
            GamePanel.Children.Add(title);

            Grid grid = new Grid();
            grid.MaxWidth = 860;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            StackPanel englishPanel = new StackPanel();
            StackPanel russianPanel = new StackPanel();

            Grid.SetColumn(englishPanel, 0);
            Grid.SetColumn(russianPanel, 1);

            foreach (WordItem word in words.OrderBy(w => _random.Next()))
            {
                Button button = CreateButton(word.English, 280);
                button.MinHeight = 64;
                button.Margin = new Thickness(8);
                button.Tag = word;
                button.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
                button.Click += MatchEnglish_Click;
                englishPanel.Children.Add(button);
            }

            foreach (WordItem word in words.OrderBy(w => _random.Next()))
            {
                Button button = CreateButton(word.Russian, 280);
                button.MinHeight = 64;
                button.Margin = new Thickness(8);
                button.Tag = word;
                button.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
                button.Click += MatchRussian_Click;
                russianPanel.Children.Add(button);
            }

            grid.Children.Add(englishPanel);
            grid.Children.Add(russianPanel);

            GamePanel.Children.Add(grid);
        }

        private void MatchEnglish_Click(object sender, RoutedEventArgs e)
        {
            ResetMatchSelectionColors();

            _matchEnglishButton = sender as Button;
            HighlightSelectedMatchButtons();
            TryCheckMatch();
        }

        private void MatchRussian_Click(object sender, RoutedEventArgs e)
        {
            ResetMatchSelectionColors();

            _matchRussianButton = sender as Button;
            HighlightSelectedMatchButtons();
            TryCheckMatch();
        }

        private async void TryCheckMatch()
        {
            if (_matchEnglishButton == null || _matchRussianButton == null)
                return;

            WordItem englishWord = _matchEnglishButton.Tag as WordItem;
            WordItem russianWord = _matchRussianButton.Tag as WordItem;

            if (englishWord != null && russianWord != null && englishWord.English == russianWord.English)
            {
                _correct++;
                _matchFound++;

                _matchEnglishButton.IsEnabled = false;
                _matchRussianButton.IsEnabled = false;

                _matchEnglishButton.Background = GetBrush("ButtonSuccessBrush", Brushes.ForestGreen);
                _matchRussianButton.Background = GetBrush("ButtonSuccessBrush", Brushes.ForestGreen);

                FeedbackText.Foreground = Brushes.ForestGreen;
                FeedbackText.Text = "Правильно!";

                if (_settings.SoundEnabled)
                    SystemSounds.Asterisk.Play();
            }
            else
            {
                _wrong++;

                FeedbackText.Foreground = Brushes.Firebrick;
                FeedbackText.Text = "Неверная пара.";

                if (englishWord != null)
                {
                    _mistakes.Add(new MistakeRecord
                    {
                        English = englishWord.English,
                        Russian = englishWord.Russian,
                        Count = 1,
                        LastWrongAnswer = russianWord == null ? "" : russianWord.Russian,
                        LastPracticed = DateTime.Now
                    });
                }

                if (_settings.SoundEnabled)
                    SystemSounds.Hand.Play();

                await Task.Delay(550);

                if (_matchEnglishButton != null && _matchEnglishButton.IsEnabled)
                    _matchEnglishButton.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);

                if (_matchRussianButton != null && _matchRussianButton.IsEnabled)
                    _matchRussianButton.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
            }

            _matchEnglishButton = null;
            _matchRussianButton = null;

            ProgressBar.Value = _matchFound * 100.0 / 6.0;

            if (_matchFound >= 6)
            {
                await Task.Delay(700);
                FinishGame();
            }
        }

        private void HighlightSelectedMatchButtons()
        {
            if (_matchEnglishButton != null && _matchEnglishButton.IsEnabled)
                _matchEnglishButton.Background = GetBrush("ButtonAccentBrush", Brushes.Purple);

            if (_matchRussianButton != null && _matchRussianButton.IsEnabled)
                _matchRussianButton.Background = GetBrush("ButtonAccentBrush", Brushes.Purple);
        }

        private void ResetMatchSelectionColors()
        {
            if (_matchEnglishButton != null && _matchEnglishButton.IsEnabled)
                _matchEnglishButton.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);

            if (_matchRussianButton != null && _matchRussianButton.IsEnabled)
                _matchRussianButton.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
        }

        // =========================================================
        // LETTER RAIN
        // =========================================================

        private void StartLetterRain()
        {
            GamePanel.Children.Clear();
            FeedbackText.Text = "";

            _letterLives = 3;
            _letterWordsDone = 0;
            _correct = 0;
            _wrong = 0;

            TextBlock instruction = CreateNormalText("Лови буквы по порядку. Нужное слово показано сверху.");
            instruction.HorizontalAlignment = HorizontalAlignment.Center;
            instruction.TextAlignment = TextAlignment.Center;
            instruction.Margin = new Thickness(0, 0, 0, 14);
            GamePanel.Children.Add(instruction);

            _answerPreview = CreateBigText("");
            _answerPreview.FontSize = GetFontSize("PreviewFontSize", 30);
            _answerPreview.HorizontalAlignment = HorizontalAlignment.Center;
            _answerPreview.Margin = new Thickness(0, 0, 0, 14);
            GamePanel.Children.Add(_answerPreview);

            _letterCanvas = new Canvas();
            _letterCanvas.Width = 780;
            _letterCanvas.Height = 380;
            _letterCanvas.Background = GetBrush("SoftBgBrush", Brushes.LightBlue);
            _letterCanvas.ClipToBounds = true;

            Border canvasBorder = new Border();
            canvasBorder.Width = 800;
            canvasBorder.Height = 400;
            canvasBorder.Background = GetBrush("SoftBgBrush", Brushes.LightBlue);
            canvasBorder.CornerRadius = new CornerRadius(30);
            canvasBorder.Padding = new Thickness(10);
            canvasBorder.Child = _letterCanvas;

            GamePanel.Children.Add(canvasBorder);

            _fallingLetters = new List<Button>();

            NextLetterRainWord();

            _letterMoveTimer = new DispatcherTimer();
            _letterMoveTimer.Interval = TimeSpan.FromMilliseconds(35);
            _letterMoveTimer.Tick += LetterMoveTimer_Tick;

            _letterSpawnTimer = new DispatcherTimer();
            _letterSpawnTimer.Interval = TimeSpan.FromMilliseconds(850);
            _letterSpawnTimer.Tick += LetterSpawnTimer_Tick;

            _letterMoveTimer.Start();
            _letterSpawnTimer.Start();
        }

        private void NextLetterRainWord()
        {
            if (_letterWordsDone >= 6)
            {
                FinishGame();
                return;
            }

            List<WordItem> words = MiniGameRepository.GetRandomWords(_topic, 1);

            if (words.Count == 0)
            {
                FinishGame();
                return;
            }

            _letterCurrentWord = words[0];
            _letterIndex = 0;

            ClearLetterCanvas();

            UpdateLetterRainText();
        }

        private void UpdateLetterRainText()
        {
            if (_letterCurrentWord == null)
                return;

            string typed = _letterCurrentWord.English.Substring(0, _letterIndex);
            string left = new string('_', _letterCurrentWord.English.Length - _letterIndex);

            _answerPreview.Text =
                _letterCurrentWord.Russian + " → " + typed + left +
                "     Жизни: " + _letterLives;
        }

        private void LetterSpawnTimer_Tick(object sender, EventArgs e)
        {
            SpawnLetterButton(true);

            if (_random.Next(0, 100) < 65)
                SpawnLetterButton(false);
        }

        private void SpawnLetterButton(bool needed)
        {
            if (_letterCurrentWord == null || _letterCanvas == null)
                return;

            char letter;

            if (needed && _letterIndex < _letterCurrentWord.English.Length)
            {
                letter = _letterCurrentWord.English[_letterIndex];
            }
            else
            {
                string alphabet = "abcdefghijklmnopqrstuvwxyz";
                letter = alphabet[_random.Next(alphabet.Length)];
            }

            Button button = CreateButton(letter.ToString(), 58);
            button.Height = 52;
            button.FontSize = GetFontSize("ButtonFontSize", 15);
            button.Tag = letter.ToString();
            button.Background = GetBrush("ButtonMainBrush", Brushes.DodgerBlue);
            button.Click += LetterButton_Click;

            _letterCanvas.Children.Add(button);
            _fallingLetters.Add(button);

            double x = _random.Next(10, 720);
            Canvas.SetLeft(button, x);
            Canvas.SetTop(button, 0);
        }

        private void LetterMoveTimer_Tick(object sender, EventArgs e)
        {
            List<Button> missed = new List<Button>();

            foreach (Button button in _fallingLetters)
            {
                double y = Canvas.GetTop(button);
                Canvas.SetTop(button, y + 2.8);

                if (y > _letterCanvas.Height - 50)
                    missed.Add(button);
            }

            foreach (Button button in missed)
            {
                string letter = button.Tag == null ? "" : button.Tag.ToString();
                RemoveLetterButton(button);

                if (_letterCurrentWord != null &&
                    _letterIndex < _letterCurrentWord.English.Length &&
                    letter == _letterCurrentWord.English[_letterIndex].ToString())
                {
                    _letterLives--;
                    _wrong++;

                    if (_settings.SoundEnabled)
                        SystemSounds.Hand.Play();

                    if (_letterLives <= 0)
                    {
                        FinishGame();
                        return;
                    }
                }
            }

            UpdateLetterRainText();
        }

        private void LetterButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button == null || _letterCurrentWord == null)
                return;

            string letter = button.Tag == null ? "" : button.Tag.ToString();

            if (_letterIndex < _letterCurrentWord.English.Length &&
                letter == _letterCurrentWord.English[_letterIndex].ToString())
            {
                _letterIndex++;
                _submittedChars++;

                RemoveLetterButton(button);

                if (_settings.SoundEnabled)
                    SystemSounds.Asterisk.Play();

                if (_letterIndex >= _letterCurrentWord.English.Length)
                {
                    _correct++;
                    _letterWordsDone++;
                    ProgressService.MarkWordAsLearned(_letterCurrentWord.English);

                    ProgressBar.Value = _letterWordsDone * 100.0 / 6.0;

                    NextLetterRainWord();
                }
            }
            else
            {
                _wrong++;
                _letterLives--;

                RemoveLetterButton(button);

                if (_settings.SoundEnabled)
                    SystemSounds.Hand.Play();

                if (_letterLives <= 0)
                {
                    FinishGame();
                    return;
                }
            }

            UpdateLetterRainText();
        }

        private void RemoveLetterButton(Button button)
        {
            if (button == null)
                return;

            if (_letterCanvas.Children.Contains(button))
                _letterCanvas.Children.Remove(button);

            _fallingLetters.Remove(button);
        }

        private void ClearLetterCanvas()
        {
            if (_letterCanvas == null)
                return;

            foreach (Button button in _fallingLetters.ToList())
            {
                if (_letterCanvas.Children.Contains(button))
                    _letterCanvas.Children.Remove(button);
            }

            _fallingLetters.Clear();
        }

        // =========================================================
        // HELPERS
        // =========================================================

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
                MinWidth = Math.Min(width, 80),
                MaxWidth = width,
                MinHeight = 46,
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
            if (button == null)
                return;

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

        private string Normalize(string value)
        {
            if (value == null)
                return "";

            return value
                .Trim()
                .ToLowerInvariant()
                .Replace("’", "'")
                .Replace("-", " ")
                .Replace("'", "")
                .Replace(".", "")
                .Replace("?", "")
                .Replace("!", "")
                .Replace(",", "")
                .Replace("  ", " ");
        }

        private string NormalizeSentence(string value)
        {
            if (value == null)
                return "";

            return value
                .Trim()
                .ToLowerInvariant()
                .Replace("’", "'")
                .Replace("-", " ")
                .Replace("'", "")
                .Replace(".", "")
                .Replace("?", "")
                .Replace("!", "")
                .Replace(",", "")
                .Replace("  ", " ");
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            FinishGame();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            StopSpecialTimers();
            WindowNavigationService.NavigateToMain(this);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                StopSpecialTimers();
                WindowNavigationService.NavigateToMain(this);
            }
        }

        private void StopSpecialTimers()
        {
            if (_timer != null)
                _timer.Stop();

            if (_letterMoveTimer != null)
                _letterMoveTimer.Stop();

            if (_letterSpawnTimer != null)
                _letterSpawnTimer.Stop();

            if (_speedTimer != null)
                _speedTimer.Stop();
        }

        private void FinishGame()
        {
            if (_isFinishing)
                return;

            _isFinishing = true;

            StopSpecialTimers();
            _stopwatch.Stop();

            MiniGameInfo info = MiniGameRepository.GetInfo(_mode);

            int total = _correct + _wrong;

            if (total <= 0)
                total = 1;

            GameResult result = new GameResult();
            result.GameName = info == null ? "Мини-игра" : info.Title;
            result.TotalWords = total;
            result.CorrectWords = _correct;
            result.WrongWords = _wrong;
            result.Duration = _stopwatch.Elapsed;
            result.Mistakes = _mistakes;
            result.Accuracy = _correct * 100.0 / total;

            double minutes = Math.Max(_stopwatch.Elapsed.TotalMinutes, 0.1);
            result.Wpm = (_submittedChars / 5.0) / minutes;

            ProgressService.ApplyResult(result);

            WindowNavigationService.Navigate(this, new ResultsWindow(result));
        }
    }
}