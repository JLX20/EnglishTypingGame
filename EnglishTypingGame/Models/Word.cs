using System;

namespace EnglishTypingGame.Models
{
    [Serializable]
    public class Word
    {
        public string English { get; set; }
        public string Russian { get; set; }
        public int Level { get; set; }
        public bool IsLearned { get; set; }

        public Word(string english, string russian, int level)
        {
            English = english;
            Russian = russian;
            Level = level;
            IsLearned = false;
        }
    }
}