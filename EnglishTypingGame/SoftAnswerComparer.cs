using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnglishTypingGame
{
    public static class SoftAnswerComparer
    {
        public static bool IsCorrect(string userAnswer, string correctAnswer)
        {
            List<string> answers = new List<string>();
            answers.Add(correctAnswer);

            return IsCorrect(userAnswer, answers);
        }

        public static bool IsCorrect(string userAnswer, IEnumerable<string> correctAnswers)
        {
            string user = Normalize(userAnswer);

            if (string.IsNullOrWhiteSpace(user))
                return false;

            foreach (string answer in correctAnswers)
            {
                string correct = Normalize(answer);

                if (user == correct)
                    return true;

                if (correct.Length >= 5 && GetDistance(user, correct) == 1)
                    return true;
            }

            return false;
        }

        public static List<string> SplitPossibleAnswers(string value)
        {
            List<string> result = new List<string>();

            if (string.IsNullOrWhiteSpace(value))
                return result;

            result.Add(value);

            string[] parts = value.Split(new[] { "/", ",", ";", "|" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts)
            {
                string trimmed = part.Trim();

                if (!string.IsNullOrWhiteSpace(trimmed))
                    result.Add(trimmed);
            }

            return result.Distinct().ToList();
        }

        public static string Normalize(string value)
        {
            if (value == null)
                return "";

            string text = value.Trim().ToLowerInvariant();

            text = text
                .Replace("’", "'")
                .Replace("`", "'")
                .Replace("ё", "е")
                .Replace("don't", "dont")
                .Replace("doesn't", "doesnt")
                .Replace("didn't", "didnt")
                .Replace("can't", "cant")
                .Replace("won't", "wont")
                .Replace("i'm", "im")
                .Replace("it's", "its");

            StringBuilder builder = new StringBuilder();

            foreach (char c in text)
            {
                if (char.IsLetterOrDigit(c) || c == ' ')
                    builder.Append(c);

                if (c == '-')
                    builder.Append(' ');
            }

            string normalized = builder.ToString();

            while (normalized.Contains("  "))
                normalized = normalized.Replace("  ", " ");

            return normalized.Trim();
        }

        private static int GetDistance(string a, string b)
        {
            if (a == null)
                a = "";

            if (b == null)
                b = "";

            int[,] dp = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; i++)
                dp[i, 0] = i;

            for (int j = 0; j <= b.Length; j++)
                dp[0, j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = a[i - 1] == b[j - 1] ? 0 : 1;

                    dp[i, j] = Math.Min(
                        Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + cost);
                }
            }

            return dp[a.Length, b.Length];
        }
    }
}