using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EnglishTypingGame
{
    public partial class GrammarLearnWindow : Window
    {
        private List<GrammarLesson> _lessons;
        private GrammarRule _currentRule;

        public GrammarLearnWindow()
        {
            InitializeComponent();

            _lessons = GrammarLessonRepository.GetLessons();

            LessonComboBox.ItemsSource = _lessons;
            LessonComboBox.SelectedIndex = 0;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            WindowNavigationService.HandleCloseToMain(this, e);
        }

        private void LessonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GrammarLesson lesson = LessonComboBox.SelectedItem as GrammarLesson;

            if (lesson == null)
                return;

            LessonDescriptionText.Text = lesson.Description;

            RulesListBox.ItemsSource = null;
            RulesListBox.ItemsSource = lesson.Rules;

            if (lesson.Rules.Count > 0)
                RulesListBox.SelectedIndex = 0;
        }

        private void RulesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GrammarRule rule = RulesListBox.SelectedItem as GrammarRule;

            if (rule == null)
                return;

            _currentRule = rule;
            ShowRule(rule);
        }

        private void ShowRule(GrammarRule rule)
        {
            RuleTitleText.Text = rule.Title;
            RuleCategoryText.Text = rule.Category + " | Уровень: " + rule.Level;

            ShortExplanationText.Text = rule.ShortExplanation;
            WhenToUseText.Text = rule.WhenToUse;
            FormulaText.Text = rule.Formula;
            ExamplesText.Text = rule.Examples;
            CommonMistakesText.Text = rule.CommonMistakes;

            PracticeQuestionText.Text = rule.PracticeQuestion;
            PracticeAnswerBox.Clear();
            PracticeFeedbackText.Text = "";
        }

        private void CheckPracticeButton_Click(object sender, RoutedEventArgs e)
        {
            CheckPractice();
        }

        private void PracticeAnswerBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                CheckPractice();
            }
        }

        private void CheckPractice()
        {
            if (_currentRule == null)
                return;

            string userAnswer = PracticeAnswerBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(userAnswer))
            {
                PracticeFeedbackText.Foreground = Brushes.Firebrick;
                PracticeFeedbackText.Text = "Сначала напиши ответ.";
                return;
            }

            if (SoftAnswerComparer.IsCorrect(userAnswer, _currentRule.PracticeAnswer))
            {
                PracticeFeedbackText.Foreground = Brushes.ForestGreen;
                PracticeFeedbackText.Text = "Правильно! " + _currentRule.PracticeExplanation;
            }
            else
            {
                PracticeFeedbackText.Foreground = Brushes.Firebrick;
                PracticeFeedbackText.Text =
                    "Пока неверно. Правильный ответ: " +
                    _currentRule.PracticeAnswer +
                    ". " +
                    _currentRule.PracticeExplanation;
            }
        }

        private void ShowAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentRule == null)
                return;

            PracticeAnswerBox.Text = _currentRule.PracticeAnswer;
            PracticeFeedbackText.Foreground = Brushes.ForestGreen;
            PracticeFeedbackText.Text = _currentRule.PracticeExplanation;
        }

        private void MiniGamesButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.Navigate(this, new MiniGamesMenuWindow("Все темы"));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.NavigateToMain(this);
        }
    }
}