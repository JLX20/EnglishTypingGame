using System;
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
                if (!File.Exists(FilePath))
                    return new ProgressData();

                XmlSerializer serializer = new XmlSerializer(typeof(ProgressData));

                using (FileStream stream = new FileStream(FilePath, FileMode.Open))
                {
                    ProgressData data = serializer.Deserialize(stream) as ProgressData;

                    if (data == null)
                        data = new ProgressData();

                    if (data.Mistakes == null)
                        data.Mistakes = new System.Collections.Generic.List<MistakeRecord>();

                    if (data.LearnedWords == null)
                        data.LearnedWords = new System.Collections.Generic.List<string>();

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
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            XmlSerializer serializer = new XmlSerializer(typeof(ProgressData));

            using (FileStream stream = new FileStream(FilePath, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
        }

        public static void ApplyResult(GameResult result)
        {
            ProgressData data = Load();

            data.TotalRounds++;
            data.TotalWords += result.TotalWords;
            data.CorrectWords += result.CorrectWords;
            data.TotalMistakes += result.WrongWords;

            if (result.Accuracy > data.BestAccuracy)
                data.BestAccuracy = result.Accuracy;

            if (result.Wpm > data.BestWpm)
                data.BestWpm = result.Wpm;

            UpdateStreak(data);

            foreach (MistakeRecord mistake in result.Mistakes)
            {
                AddMistake(data, mistake.English, mistake.Russian, mistake.LastWrongAnswer);
            }

            Save(data);
        }

        public static void AddMistake(ProgressData data, string english, string russian, string wrongAnswer)
        {
            MistakeRecord existing = data.Mistakes.FirstOrDefault(m =>
                m.English.Equals(english, StringComparison.OrdinalIgnoreCase));

            if (existing == null)
            {
                data.Mistakes.Add(new MistakeRecord
                {
                    English = english,
                    Russian = russian,
                    Count = 1,
                    LastWrongAnswer = wrongAnswer,
                    LastPracticed = DateTime.Now
                });
            }
            else
            {
                existing.Count++;
                existing.LastWrongAnswer = wrongAnswer;
                existing.LastPracticed = DateTime.Now;
            }
        }

        public static void MarkWordAsLearned(string english)
        {
            ProgressData data = Load();

            bool alreadyExists = data.LearnedWords.Any(w =>
                w.Equals(english, StringComparison.OrdinalIgnoreCase));

            if (!alreadyExists)
            {
                data.LearnedWords.Add(english);
                Save(data);
            }
        }

        public static bool IsWordLearned(string english)
        {
            ProgressData data = Load();

            return data.LearnedWords.Any(w =>
                w.Equals(english, StringComparison.OrdinalIgnoreCase));
        }

        public static void Reset()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }

        private static void UpdateStreak(ProgressData data)
        {
            DateTime today = DateTime.Today;

            if (data.LastPlayedDate.Date == today)
                return;

            if (data.LastPlayedDate.Date == today.AddDays(-1))
                data.CurrentStreak++;
            else
                data.CurrentStreak = 1;

            data.LastPlayedDate = today;
        }
    }
}