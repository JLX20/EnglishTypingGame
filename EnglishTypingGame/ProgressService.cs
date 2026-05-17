using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace EnglishTypingGame
{
    public static class ProgressService
    {
        private static readonly string FolderPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EnglishTypingGame");

        private static readonly string FilePath =
            Path.Combine(FolderPath, "progress.xml");

        public static ProgressData Load()
        {
            try
            {
                if (!Directory.Exists(FolderPath))
                    Directory.CreateDirectory(FolderPath);

                if (!File.Exists(FilePath))
                    return new ProgressData();

                XmlSerializer serializer = new XmlSerializer(typeof(ProgressData));

                using (FileStream stream = new FileStream(FilePath, FileMode.Open))
                {
                    ProgressData data = serializer.Deserialize(stream) as ProgressData;

                    if (data == null)
                        return new ProgressData();

                    FixNullCollections(data);
                    return data;
                }
            }
            catch
            {
                return new ProgressData();
            }
        }

        public static void Save(ProgressData data)
        {
            try
            {
                if (data == null)
                    data = new ProgressData();

                FixNullCollections(data);

                if (!Directory.Exists(FolderPath))
                    Directory.CreateDirectory(FolderPath);

                XmlSerializer serializer = new XmlSerializer(typeof(ProgressData));

                using (FileStream stream = new FileStream(FilePath, FileMode.Create))
                {
                    serializer.Serialize(stream, data);
                }
            }
            catch
            {
            }
        }

        private static void FixNullCollections(ProgressData data)
        {
            if (data.LearnedWords == null)
                data.LearnedWords = new List<string>();

            if (data.Mistakes == null)
                data.Mistakes = new List<MistakeRecord>();

            if (data.CompletedPathSteps == null)
                data.CompletedPathSteps = new List<string>();
        }

        public static bool IsWordLearned(string english)
        {
            if (string.IsNullOrWhiteSpace(english))
                return false;

            ProgressData progress = Load();

            return progress.LearnedWords.Any(w =>
                w.Equals(english, StringComparison.OrdinalIgnoreCase));
        }

        public static void MarkWordAsLearned(string english)
        {
            if (string.IsNullOrWhiteSpace(english))
                return;

            ProgressData progress = Load();

            if (!progress.LearnedWords.Any(w => w.Equals(english, StringComparison.OrdinalIgnoreCase)))
                progress.LearnedWords.Add(english);

            Save(progress);
        }

        public static void MarkPathStepCompleted(string topic, string step)
        {
            ProgressData progress = Load();

            string key = BuildPathKey(topic, step);

            if (!progress.CompletedPathSteps.Contains(key))
                progress.CompletedPathSteps.Add(key);

            Save(progress);
        }

        public static bool IsPathStepCompleted(string topic, string step)
        {
            ProgressData progress = Load();

            string key = BuildPathKey(topic, step);

            return progress.CompletedPathSteps.Contains(key);
        }

        private static string BuildPathKey(string topic, string step)
        {
            if (string.IsNullOrWhiteSpace(topic))
                topic = "Все темы";

            if (string.IsNullOrWhiteSpace(step))
                step = "Step";

            return topic.Trim().ToLowerInvariant() + "::" + step.Trim().ToLowerInvariant();
        }

        public static void ApplyResult(GameResult result)
        {
            if (result == null)
                return;

            ProgressData progress = Load();

            progress.TotalRounds++;
            progress.TotalWords += result.TotalWords;
            progress.CorrectWords += result.CorrectWords;
            progress.TotalMistakes += result.WrongWords;

            if (result.Accuracy > progress.BestAccuracy)
                progress.BestAccuracy = result.Accuracy;

            if (result.Wpm > progress.BestWpm)
                progress.BestWpm = result.Wpm;

            UpdateStreak(progress);
            AddMistakes(progress, result.Mistakes);

            Save(progress);
        }

        private static void UpdateStreak(ProgressData progress)
        {
            DateTime today = DateTime.Today;

            if (progress.LastPracticeDate == DateTime.MinValue)
            {
                progress.CurrentStreak = 1;
            }
            else
            {
                DateTime last = progress.LastPracticeDate.Date;

                if (last == today)
                {
                }
                else if (last == today.AddDays(-1))
                {
                    progress.CurrentStreak++;
                }
                else
                {
                    progress.CurrentStreak = 1;
                }
            }

            progress.LastPracticeDate = today;
        }

        private static void AddMistakes(ProgressData progress, List<MistakeRecord> mistakes)
        {
            if (mistakes == null)
                return;

            foreach (MistakeRecord mistake in mistakes)
            {
                if (mistake == null || string.IsNullOrWhiteSpace(mistake.English))
                    continue;

                MistakeRecord existing = progress.Mistakes.FirstOrDefault(m =>
                    m.English.Equals(mistake.English, StringComparison.OrdinalIgnoreCase));

                if (existing == null)
                {
                    mistake.Count = Math.Max(1, mistake.Count);
                    mistake.LastPracticed = DateTime.Now;
                    progress.Mistakes.Add(mistake);
                }
                else
                {
                    existing.Count += Math.Max(1, mistake.Count);
                    existing.Russian = mistake.Russian;
                    existing.LastWrongAnswer = mistake.LastWrongAnswer;
                    existing.LastPracticed = DateTime.Now;
                }
            }
        }

        public static List<TopicProgressInfo> GetTopicProgress()
        {
            ProgressData progress = Load();

            List<TopicProgressInfo> result = new List<TopicProgressInfo>();

            foreach (string topicUi in WordBankService.GetTopicsForUi())
            {
                if (topicUi == "Все темы")
                    continue;

                List<WordItem> words = WordBankService.GetWords(topicUi);

                int total = words.Count;

                int learned = words.Count(w =>
                    progress.LearnedWords.Any(l =>
                        l.Equals(w.English, StringComparison.OrdinalIgnoreCase)));

                int mistakes = 0;

                foreach (MistakeRecord mistake in progress.Mistakes)
                {
                    WordItem word = WordBankService.FindWordByEnglish(mistake.English);

                    if (word != null &&
                        words.Any(w => w.English.Equals(word.English, StringComparison.OrdinalIgnoreCase)))
                    {
                        mistakes++;
                    }
                }

                double percent = 0;

                if (total > 0)
                    percent = learned * 100.0 / total;

                int stars = 0;

                if (percent >= 90)
                    stars = 3;
                else if (percent >= 60)
                    stars = 2;
                else if (percent >= 30)
                    stars = 1;

                result.Add(new TopicProgressInfo
                {
                    TopicName = topicUi,
                    TotalWords = total,
                    LearnedWords = learned,
                    MistakeWords = mistakes,
                    LearnedPercent = percent,
                    Stars = stars
                });
            }

            return result;
        }
    }
}