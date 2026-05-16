using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EnglishTypingGame
{
    public partial class GrammarLearnWindow : Window
    {
        private List<GrammarRule> _rules;
        private GrammarRule _currentRule;

        public GrammarLearnWindow()
        {
            InitializeComponent();

            CategoryComboBox.ItemsSource = GrammarRuleRepository.GetCategories();
            CategoryComboBox.SelectedIndex = 0;

            LoadRules("Все правила");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            WindowNavigationService.HandleCloseToMain(this, e);
        }

        private void LoadRules(string category)
        {
            _rules = GrammarRuleRepository.GetRules(category);

            RulesListBox.ItemsSource = null;
            RulesListBox.ItemsSource = _rules;

            if (_rules.Count > 0)
                RulesListBox.SelectedIndex = 0;
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem == null)
                return;

            LoadRules(CategoryComboBox.SelectedItem.ToString());
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
            string correctAnswer = _currentRule.PracticeAnswer.Trim();

            if (string.IsNullOrWhiteSpace(userAnswer))
            {
                PracticeFeedbackText.Foreground = Brushes.Firebrick;
                PracticeFeedbackText.Text = "Сначала напиши ответ.";
                return;
            }

            if (userAnswer.ToLower() == correctAnswer.ToLower())
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