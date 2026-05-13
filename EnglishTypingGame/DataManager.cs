using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EnglishTypingGame.Models;

namespace EnglishTypingGame
{
    public static class DataManager
    {
        private static string dataFile = "words_data.txt";
        public static List<Word> AllWords = new List<Word>();
        public static int CurrentLevel = 1;
        public static int CalibrationSpeed = 500;
        public static float SoundVolume = 0.5f;

        static DataManager()
        {
            LoadData();
        }

        public static void LoadData()
        {
            if (File.Exists(dataFile))
            {
                var lines = File.ReadAllLines(dataFile, Encoding.UTF8);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length >= 4)
                    {
                        var word = new Word(parts[0], parts[1], int.Parse(parts[2]));
                        word.IsLearned = bool.Parse(parts[3]);
                        AllWords.Add(word);
                    }
                }
            }
            else
            {
                // Уровень 1
                AllWords.Add(new Word("cat", "кошка", 1));
                AllWords.Add(new Word("dog", "собака", 1));
                AllWords.Add(new Word("bird", "птица", 1));
                AllWords.Add(new Word("fish", "рыба", 1));
                AllWords.Add(new Word("rabbit", "кролик", 1));
                // Уровень 2
                AllWords.Add(new Word("run", "бежать", 2));
                AllWords.Add(new Word("jump", "прыгать", 2));
                AllWords.Add(new Word("eat", "есть", 2));
                AllWords.Add(new Word("sleep", "спать", 2));
                AllWords.Add(new Word("drink", "пить", 2));
                // Уровень 3
                AllWords.Add(new Word("big", "большой", 3));
                AllWords.Add(new Word("small", "маленький", 3));
                AllWords.Add(new Word("fast", "быстрый", 3));
                AllWords.Add(new Word("slow", "медленный", 3));
                AllWords.Add(new Word("happy", "счастливый", 3));
                SaveData();
            }
        }

        public static void SaveData()
        {
            var lines = AllWords.Select(w => $"{w.English}|{w.Russian}|{w.Level}|{w.IsLearned}");
            File.WriteAllLines(dataFile, lines, Encoding.UTF8);
        }

        public static List<Word> GetWordsByLevel(int level)
        {
            return AllWords.Where(w => w.Level == level).ToList();
        }

        public static bool IsLevelCompleted(int level)
        {
            var words = GetWordsByLevel(level);
            return words.All(w => w.IsLearned);
        }

        public static void MarkWordAsLearned(string english)
        {
            var word = AllWords.FirstOrDefault(w => w.English == english);
            if (word != null) word.IsLearned = true;
            SaveData();
        }
    }
}