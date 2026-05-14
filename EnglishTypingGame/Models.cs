using System;
using System.Collections.Generic;

namespace EnglishTypingGame
{
    [Serializable]
    public class WordItem
    {
        public string English { get; set; }
        public string Russian { get; set; }
        public string Topic { get; set; }
        public string Level { get; set; }
        public string Example { get; set; }

        public WordItem()
        {
        }

        public WordItem(string english, string russian, string topic, string level, string example)
        {
            English = english;
            Russian = russian;
            Topic = topic;
            Level = level;
            Example = example;
        }
    }

    [Serializable]
    public class MistakeRecord
    {
        public string English { get; set; }
        public string Russian { get; set; }
        public int Count { get; set; }
        public string LastWrongAnswer { get; set; }
        public DateTime LastPracticed { get; set; }

        public MistakeRecord()
        {
        }
    }

    [Serializable]
    public class ProgressData
    {
        public int TotalRounds { get; set; }
        public int TotalWords { get; set; }
        public int CorrectWords { get; set; }
        public int TotalMistakes { get; set; }
        public double BestAccuracy { get; set; }
        public double BestWpm { get; set; }
        public int CurrentStreak { get; set; }
        public DateTime LastPlayedDate { get; set; }

        public List<MistakeRecord> Mistakes { get; set; }
        public List<string> LearnedWords { get; set; }

        public ProgressData()
        {
            Mistakes = new List<MistakeRecord>();
            LearnedWords = new List<string>();
        }
    }

    public class GameResult
    {
        public string GameName { get; set; }
        public int TotalWords { get; set; }
        public int CorrectWords { get; set; }
        public int WrongWords { get; set; }
        public double Accuracy { get; set; }
        public double Wpm { get; set; }
        public TimeSpan Duration { get; set; }
        public List<MistakeRecord> Mistakes { get; set; }

        public GameResult()
        {
            Mistakes = new List<MistakeRecord>();
        }
    }

    [Serializable]
    public class SettingsData
    {
        public int WordsPerRound { get; set; }
        public int WordRainSeconds { get; set; }

        public bool SlowMode { get; set; }
        public bool SoundEnabled { get; set; }
        public bool ShowExamples { get; set; }

        public double WordRainFallSpeedMultiplier { get; set; }
        public int WordRainSpawnIntervalMilliseconds { get; set; }

        public bool WordRainCloudMode { get; set; }
        public bool WordRainVisualEffects { get; set; }

        public string ThemeName { get; set; }
        public string BackgroundName { get; set; }

        public SettingsData()
        {
            WordsPerRound = 10;
            WordRainSeconds = 60;

            SlowMode = false;
            SoundEnabled = true;
            ShowExamples = true;

            WordRainFallSpeedMultiplier = 1.0;
            WordRainSpawnIntervalMilliseconds = 1300;

            WordRainCloudMode = true;
            WordRainVisualEffects = true;

            ThemeName = "Blue";
            BackgroundName = "Sky";
        }
    }

    public enum MiniGameMode
    {
        WordCards,
        MemoryPairs,
        TranslationMatch,
        WordBuilder,
        MissingLetter,
        SpeedTranslation,
        LetterRain,

        ArticleGame,
        HaveHas,
        DoDoes,
        PresentSimpleBuilder,
        SentencePuzzle,
        TranslationSentenceBuilder,
        PresentSimpleVsContinuous,
        WasWere,
        CanCant,
        NumberTyping,
        DaysAndMonths,
        ChooseReply
    }

    public class MiniGameInfo
    {
        public MiniGameMode Mode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }

    public class MiniGameExercise
    {
        public string Prompt { get; set; }
        public string RussianPrompt { get; set; }
        public string Answer { get; set; }
        public string Explanation { get; set; }

        public List<string> Options { get; set; }
        public List<string> WordsToArrange { get; set; }

        public WordItem Word { get; set; }

        public MiniGameExercise()
        {
            Options = new List<string>();
            WordsToArrange = new List<string>();
        }
    }
}