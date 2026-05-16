using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishTypingGame
{
    public static class MixedTrainingRepository
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

        public static List<MiniGameExercise> BuildExercises(string topic, string level)
        {
            List<MiniGameExercise> exercises = new List<MiniGameExercise>();

            List<WordItem> words = LessonQueryService.GetWords(topic, level)
                .OrderBy(w => Random.Next())
                .Take(10)
                .ToList();

            foreach (WordItem word in words.Take(4))
            {
                exercises.Add(new MiniGameExercise
                {
                    Prompt = "Напиши английский перевод: " + word.Russian,
                    Answer = word.English,
                    Explanation = "Правильный ответ: " + word.English,
                    Word = word
                });
            }

            foreach (WordItem word in words.Skip(4).Take(3))
            {
                List<string> options = words
                    .Where(w => w.English != word.English)
                    .OrderBy(w => Random.Next())
                    .Take(3)
                    .Select(w => w.Russian)
                    .ToList();

                options.Add(word.Russian);
                options = options.OrderBy(x => Random.Next()).ToList();

                exercises.Add(new MiniGameExercise
                {
                    Prompt = "Выбери перевод слова: " + word.English,
                    Answer = word.Russian,
                    Options = options,
                    Explanation = word.English + " = " + word.Russian,
                    Word = word
                });
            }

            exercises.Add(OptionExercise(
                "He ___ milk.",
                "likes",
                "В Present Simple с he добавляем -s: likes.",
                "like",
                "likes",
                "liked"));

            exercises.Add(OptionExercise(
                "She ___ reading now.",
                "is",
                "Present Continuous: she is reading.",
                "am",
                "is",
                "are"));

            exercises.Add(OptionExercise(
                "Yesterday I ___ football.",
                "played",
                "Past Simple: play → played.",
                "play",
                "played",
                "plays"));

            exercises.Add(OptionExercise(
                "Tomorrow I ___ help you.",
                "will",
                "Future Simple строится с will.",
                "did",
                "will",
                "do"));

            exercises.Add(OptionExercise(
                "Birds ___ fly.",
                "can",
                "Птицы умеют летать: can fly.",
                "can",
                "can't",
                "mustn't"));

            exercises.Add(OptionExercise(
                "Book — это...",
                "noun",
                "Book называет предмет, значит это noun.",
                "verb",
                "noun",
                "adjective"));

            return exercises.OrderBy(x => Random.Next()).ToList();
        }

        private static MiniGameExercise OptionExercise(string prompt, string answer, string explanation, params string[] options)
        {
            return new MiniGameExercise
            {
                Prompt = prompt,
                Answer = answer,
                Explanation = explanation,
                Options = options.OrderBy(x => Random.Next()).ToList()
            };
        }
    }
}