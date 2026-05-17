using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishTypingGame
{
    public static class LessonQueryService
    {
        public static List<string> GetTopicsForUi()
        {
            return WordBankService.GetTopicsForUi();
        }

        public static List<string> GetLevelsForUi()
        {
            return new List<string>
            {
                "Все уровни",
                "A0 — самые простые",
                "A1 — базовые",
                "A2 — сложнее"
            };
        }

        public static List<WordItem> GetWords(string topicUi, string levelUi)
        {
            List<WordItem> words = WordBankService.GetWords(topicUi);

            string level = NormalizeLevel(levelUi);

            if (string.IsNullOrWhiteSpace(level))
                return words;

            return words
                .Where(w => w.Level.Equals(level, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public static string NormalizeLevel(string levelUi)
        {
            if (string.IsNullOrWhiteSpace(levelUi))
                return "";

            if (levelUi == "Все уровни")
                return "";

            string[] parts = levelUi.Split(new[] { " — " }, StringSplitOptions.None);
            return parts[0].Trim();
        }

        public static string NormalizeTopic(string topicUi)
        {
            if (string.IsNullOrWhiteSpace(topicUi))
                return "";

            if (topicUi == "Все темы")
                return "";

            string[] parts = topicUi.Split(new[] { " — " }, StringSplitOptions.None);
            return parts[0].Trim();
        }
    }
}