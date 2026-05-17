using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishTypingGame
{
    public static class WordBankService
    {
        public static List<string> GetTopicsForUi()
        {
            List<string> topics = new List<string>();

            topics.AddRange(LessonRepository.GetTopicsForUi());

            foreach (string topic in ComputerWordsRepository.GetTopicsForUi())
            {
                if (!topics.Contains(topic))
                    topics.Add(topic);
            }

            return topics;
        }

        public static List<WordItem> GetAllWords()
        {
            List<WordItem> words = new List<WordItem>();

            words.AddRange(LessonRepository.GetAllWords());
            words.AddRange(ComputerWordsRepository.GetAllWords());

            return words
                .GroupBy(w => w.Topic + "|" + w.English.ToLowerInvariant())
                .Select(g => g.First())
                .ToList();
        }

        public static List<WordItem> GetWords(string topicUi)
        {
            if (string.IsNullOrWhiteSpace(topicUi) || topicUi == "Все темы")
                return GetAllWords();

            if (ComputerWordsRepository.IsComputerTopic(topicUi))
                return ComputerWordsRepository.GetWords(topicUi);

            return LessonRepository.GetWords(topicUi);
        }

        public static WordItem FindWordByEnglish(string english)
        {
            if (string.IsNullOrWhiteSpace(english))
                return null;

            WordItem word = LessonRepository.FindWordByEnglish(english);

            if (word != null)
                return word;

            return ComputerWordsRepository.FindWordByEnglish(english);
        }
    }
}