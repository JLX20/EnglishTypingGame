using System.Collections.Generic;
using System.Linq;

namespace EnglishTypingGame
{
    public static class GrammarLessonRepository
    {
        public static List<GrammarLesson> GetLessons()
        {
            List<GrammarRule> rules = GrammarRuleRepository.GetAllRules();

            List<GrammarLesson> lessons = new List<GrammarLesson>();

            lessons.Add(CreateLesson(
                "Урок 1. Части речи",
                "Что такое noun, verb, adjective, adverb, pronoun, article, preposition и conjunction.",
                rules,
                "Части речи"));

            lessons.Add(CreateLesson(
                "Урок 2. Существительные",
                "Множественное число, исключения, исчисляемые и неисчисляемые существительные.",
                rules,
                "Существительные"));

            lessons.Add(CreateLesson(
                "Урок 3. Артикли",
                "A, an и the.",
                rules,
                "Артикли"));

            lessons.Add(CreateLesson(
                "Урок 4. Глаголы",
                "Правильные и неправильные глаголы.",
                rules,
                "Глаголы"));

            lessons.Add(CreateLesson(
                "Урок 5. Модальные глаголы",
                "Can, must, should, may, might.",
                rules,
                "Модальные глаголы"));

            lessons.Add(CreateLesson(
                "Урок 6. Времена",
                "Simple, Continuous и Perfect.",
                rules,
                "Времена"));

            lessons.Add(CreateLesson(
                "Урок 7. Числа",
                "Большие числа, десятки, сотни и thousand.",
                rules,
                "Числа"));

            lessons = lessons
                .Where(l => l.Rules.Count > 0)
                .ToList();

            GrammarLesson all = new GrammarLesson();
            all.Title = "Все уроки";
            all.Description = "Все правила в одном списке.";
            all.Rules = rules;

            lessons.Insert(0, all);

            return lessons;
        }

        private static GrammarLesson CreateLesson(string title, string description, List<GrammarRule> allRules, string category)
        {
            GrammarLesson lesson = new GrammarLesson();
            lesson.Title = title;
            lesson.Description = description;
            lesson.Rules = allRules
                .Where(r => r.Category == category)
                .ToList();

            return lesson;
        }
    }
}