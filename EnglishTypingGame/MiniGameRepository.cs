using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishTypingGame
{
    public static class MiniGameRepository
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

        private static readonly Dictionary<string, Queue<string>> RecentWordsByTopic =
            new Dictionary<string, Queue<string>>();

        public static List<MiniGameInfo> GetMiniGames()
        {
            return new List<MiniGameInfo>
            {
                new MiniGameInfo { Mode = MiniGameMode.WordCards, Title = "Word Cards", Group = "Слова", Description = "Карточки для повторения слов из выбранной темы." },
                new MiniGameInfo { Mode = MiniGameMode.MemoryPairs, Title = "Memory Pairs", Group = "Слова", Description = "Найди пары: английское слово и русский перевод." },
                new MiniGameInfo { Mode = MiniGameMode.TranslationMatch, Title = "Translation Match", Group = "Слова", Description = "Соедини английские слова с переводами." },
                new MiniGameInfo { Mode = MiniGameMode.WordBuilder, Title = "Word Builder", Group = "Слова", Description = "Собери английское слово из букв." },
                new MiniGameInfo { Mode = MiniGameMode.MissingLetter, Title = "Missing Letter", Group = "Слова", Description = "Выбери пропущенную букву в слове." },
                new MiniGameInfo { Mode = MiniGameMode.SpeedTranslation, Title = "Speed Translation", Group = "Слова", Description = "Быстро переводи слова на английский." },
                new MiniGameInfo { Mode = MiniGameMode.LetterRain, Title = "Letter Rain", Group = "Слова", Description = "Лови буквы в правильном порядке." },

                new MiniGameInfo { Mode = MiniGameMode.ArticleGame, Title = "The Article Game", Group = "Грамматика", Description = "Выбери a, an или the." },
                new MiniGameInfo { Mode = MiniGameMode.HaveHas, Title = "Have / Has", Group = "Грамматика", Description = "Выбери have или has." },
                new MiniGameInfo { Mode = MiniGameMode.DoDoes, Title = "Do / Does", Group = "Грамматика", Description = "Выбери do или does." },
                new MiniGameInfo { Mode = MiniGameMode.PresentSimpleBuilder, Title = "Present Simple Builder", Group = "Грамматика", Description = "Собери предложение в Present Simple." },
                new MiniGameInfo { Mode = MiniGameMode.PresentSimpleVsContinuous, Title = "Present Simple vs Continuous", Group = "Грамматика", Description = "Выбери Present Simple или Present Continuous." },
                new MiniGameInfo { Mode = MiniGameMode.WasWere, Title = "Was / Were", Group = "Грамматика", Description = "Выбери was или were." },
                new MiniGameInfo { Mode = MiniGameMode.CanCant, Title = "Can / Can't", Group = "Грамматика", Description = "Выбери can или can't." },

                new MiniGameInfo { Mode = MiniGameMode.BigNumbers, Title = "Big Numbers", Group = "Грамматика", Description = "Повтори большие числа: десятки, сотни и тысячу." },
                new MiniGameInfo { Mode = MiniGameMode.PastSimple, Title = "Past Simple", Group = "Грамматика", Description = "Повтори прошедшее время: утверждение, отрицание и вопрос." },
                new MiniGameInfo { Mode = MiniGameMode.FutureSimple, Title = "Future Simple", Group = "Грамматика", Description = "Повтори будущее время с will." },
                new MiniGameInfo { Mode = MiniGameMode.TenseForms, Title = "Simple / Continuous / Perfect", Group = "Грамматика", Description = "Отличай Simple, Continuous и Perfect в настоящем, прошлом и будущем времени." },
                new MiniGameInfo { Mode = MiniGameMode.VerbRules, Title = "Verb Rules", Group = "Грамматика", Description = "Правильные и неправильные глаголы." },
                new MiniGameInfo { Mode = MiniGameMode.ModalVerbsRules, Title = "Modal Verbs", Group = "Грамматика", Description = "Can, must, should, may и правила после них." },
                new MiniGameInfo { Mode = MiniGameMode.NounPluralRules, Title = "Noun Plurals", Group = "Грамматика", Description = "Множественное число существительных." },
                new MiniGameInfo { Mode = MiniGameMode.CountableUncountable, Title = "Countable / Uncountable", Group = "Грамматика", Description = "Исчисляемые и неисчисляемые существительные." },
                new MiniGameInfo { Mode = MiniGameMode.PossessiveS, Title = "Possessive 's", Group = "Грамматика", Description = "Притяжательный 's." },
                new MiniGameInfo { Mode = MiniGameMode.PartsOfSpeech, Title = "Parts of Speech", Group = "Грамматика", Description = "Части речи: noun, verb, adjective, adverb, preposition." },

                new MiniGameInfo { Mode = MiniGameMode.SentencePuzzle, Title = "Sentence Puzzle", Group = "Предложения", Description = "Собери английское предложение из слов." },
                new MiniGameInfo { Mode = MiniGameMode.TranslationSentenceBuilder, Title = "Translation Sentence Builder", Group = "Предложения", Description = "Собери английское предложение по русскому переводу." },

                new MiniGameInfo { Mode = MiniGameMode.NumberTyping, Title = "Number Typing", Group = "Числа и даты", Description = "Напиши число английским словом." },
                new MiniGameInfo { Mode = MiniGameMode.DaysAndMonths, Title = "Days and Months", Group = "Числа и даты", Description = "Расставь дни недели или месяцы по порядку." },

                new MiniGameInfo { Mode = MiniGameMode.ChooseReply, Title = "Choose the Reply", Group = "Диалоги", Description = "Выбери правильный ответ в простом диалоге." }
            };
        }

        public static MiniGameInfo GetInfo(MiniGameMode mode)
        {
            return GetMiniGames().FirstOrDefault(g => g.Mode == mode);
        }

        public static List<MiniGameExercise> GetExercises(MiniGameMode mode, string topic)
        {
            switch (mode)
            {
                case MiniGameMode.WordCards:
                    return BuildWordCards(topic);

                case MiniGameMode.WordBuilder:
                    return BuildWordBuilder(topic);

                case MiniGameMode.MissingLetter:
                    return BuildMissingLetter(topic);

                case MiniGameMode.SpeedTranslation:
                    return BuildSpeedTranslation(topic);

                case MiniGameMode.ArticleGame:
                    return BuildArticleGame();

                case MiniGameMode.HaveHas:
                    return BuildHaveHas();

                case MiniGameMode.DoDoes:
                    return BuildDoDoes();

                case MiniGameMode.PresentSimpleBuilder:
                    return BuildPresentSimpleBuilder();

                case MiniGameMode.PresentSimpleVsContinuous:
                    return BuildPresentSimpleVsContinuous();

                case MiniGameMode.WasWere:
                    return BuildWasWere();

                case MiniGameMode.CanCant:
                    return BuildCanCant();

                case MiniGameMode.BigNumbers:
                    return BuildBigNumbers();

                case MiniGameMode.PastSimple:
                    return BuildPastSimple();

                case MiniGameMode.FutureSimple:
                    return BuildFutureSimple();

                case MiniGameMode.TenseForms:
                    return BuildTenseForms();

                case MiniGameMode.VerbRules:
                    return BuildVerbRules();

                case MiniGameMode.ModalVerbsRules:
                    return BuildModalVerbsRules();

                case MiniGameMode.NounPluralRules:
                    return BuildNounPluralRules();

                case MiniGameMode.CountableUncountable:
                    return BuildCountableUncountable();

                case MiniGameMode.PossessiveS:
                    return BuildPossessiveS();

                case MiniGameMode.PartsOfSpeech:
                    return BuildPartsOfSpeech();

                case MiniGameMode.SentencePuzzle:
                    return BuildSentencePuzzle();

                case MiniGameMode.TranslationSentenceBuilder:
                    return BuildTranslationSentenceBuilder();

                case MiniGameMode.NumberTyping:
                    return BuildNumberTyping();

                case MiniGameMode.DaysAndMonths:
                    return BuildDaysAndMonths();

                case MiniGameMode.ChooseReply:
                    return BuildChooseReply();

                default:
                    return BuildSpeedTranslation(topic);
            }
        }

        public static List<WordItem> GetRandomWords(string topic, int count)
        {
            List<WordItem> words = LessonRepository.GetWords(topic);

            if (words == null || words.Count == 0)
                words = LessonRepository.GetAllWords();

            words = words
                .Where(w => w != null)
                .Where(w => !string.IsNullOrWhiteSpace(w.English))
                .GroupBy(w => w.English.ToLowerInvariant())
                .Select(g => g.First())
                .ToList();

            if (words.Count == 0)
                return new List<WordItem>();

            string key = NormalizeTopicKey(topic);

            if (!RecentWordsByTopic.ContainsKey(key))
                RecentWordsByTopic[key] = new Queue<string>();

            Queue<string> recent = RecentWordsByTopic[key];

            List<WordItem> notRecentWords = words
                .Where(w => !recent.Contains(w.English.ToLowerInvariant()))
                .ToList();

            List<WordItem> source;

            if (notRecentWords.Count >= count)
            {
                source = notRecentWords;
            }
            else
            {
                source = words.ToList();
            }

            List<WordItem> selected = Shuffle(source)
                .Take(count)
                .ToList();

            RememberRecentWords(key, selected, words.Count, count);

            return selected;
        }

        private static string NormalizeTopicKey(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
                return "all";

            return topic.Trim().ToLowerInvariant();
        }

        private static List<T> Shuffle<T>(IEnumerable<T> source)
        {
            List<T> list = source.ToList();

            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Next(i + 1);

                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }

            return list;
        }

        private static void RememberRecentWords(string key, List<WordItem> selected, int totalWordsCount, int requestedCount)
        {
            if (!RecentWordsByTopic.ContainsKey(key))
                RecentWordsByTopic[key] = new Queue<string>();

            Queue<string> recent = RecentWordsByTopic[key];

            foreach (WordItem word in selected)
            {
                string english = word.English.ToLowerInvariant();

                if (!recent.Contains(english))
                    recent.Enqueue(english);
            }

            int maxRecentCount = requestedCount * 3;

            if (totalWordsCount > 0)
                maxRecentCount = Math.Min(maxRecentCount, Math.Max(3, totalWordsCount / 2));

            if (maxRecentCount < requestedCount)
                maxRecentCount = requestedCount;

            while (recent.Count > maxRecentCount)
                recent.Dequeue();
        }

        private static List<MiniGameExercise> BuildWordCards(string topic)
        {
            return GetRandomWords(topic, 12)
                .Select(w => new MiniGameExercise
                {
                    Prompt = w.English,
                    RussianPrompt = w.Russian,
                    Answer = w.English,
                    Explanation = w.Example,
                    Word = w
                })
                .ToList();
        }

        private static List<MiniGameExercise> BuildWordBuilder(string topic)
        {
            List<MiniGameExercise> exercises = new List<MiniGameExercise>();

            foreach (WordItem word in GetRandomWords(topic, 10))
            {
                string cleanWord = PrepareSingleWord(word.English);

                if (cleanWord.Length < 2)
                    continue;

                exercises.Add(new MiniGameExercise
                {
                    Prompt = "Собери слово: " + word.Russian,
                    RussianPrompt = word.Russian,
                    Answer = cleanWord,
                    WordsToArrange = ShuffleLetters(cleanWord),
                    Explanation = "Правильное слово: " + cleanWord + ". Пример: " + word.Example,
                    Word = new WordItem(cleanWord, word.Russian, word.Topic, word.Level, word.Example)
                });
            }

            return exercises;
        }

        private static List<MiniGameExercise> BuildMissingLetter(string topic)
        {
            List<MiniGameExercise> exercises = new List<MiniGameExercise>();

            foreach (WordItem word in GetRandomWords(topic, 10))
            {
                string cleanWord = PrepareSingleWord(word.English);

                if (cleanWord.Length < 3)
                    continue;

                int missingIndex = Random.Next(1, cleanWord.Length - 1);
                char correct = cleanWord[missingIndex];

                string hidden =
                    cleanWord.Substring(0, missingIndex) +
                    "_" +
                    cleanWord.Substring(missingIndex + 1);

                exercises.Add(new MiniGameExercise
                {
                    Prompt = hidden + " — " + word.Russian,
                    RussianPrompt = word.Russian,
                    Answer = correct.ToString(),
                    Options = BuildLetterOptions(correct),
                    Explanation = "Полное слово: " + cleanWord,
                    Word = new WordItem(cleanWord, word.Russian, word.Topic, word.Level, word.Example)
                });
            }

            return exercises;
        }

        private static List<MiniGameExercise> BuildSpeedTranslation(string topic)
        {
            return GetRandomWords(topic, 25)
                .Select(w => new MiniGameExercise
                {
                    Prompt = w.Russian,
                    RussianPrompt = w.Russian,
                    Answer = w.English,
                    Explanation = "Правильный ответ: " + w.English + ".",
                    Word = w
                })
                .ToList();
        }

        private static List<MiniGameExercise> BuildArticleGame()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("I have ___ dog.", "a", "Dog начинается с согласного звука: a dog.", "a", "an", "the"),
                OptionExercise("She has ___ apple.", "an", "Apple начинается с гласного звука: an apple.", "a", "an", "the"),
                OptionExercise("Open ___ door, please.", "the", "Мы говорим о конкретной двери: the door.", "a", "an", "the"),
                OptionExercise("I see ___ orange.", "an", "Orange начинается с гласного звука: an orange.", "a", "an", "the")
            };
        }

        private static List<MiniGameExercise> BuildHaveHas()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("I ___ a book.", "have", "С I используется have.", "have", "has"),
                OptionExercise("She ___ a cat.", "has", "С she используется has.", "have", "has"),
                OptionExercise("They ___ a dog.", "have", "С they используется have.", "have", "has"),
                OptionExercise("Tom ___ a bike.", "has", "Tom = he, поэтому has.", "have", "has")
            };
        }

        private static List<MiniGameExercise> BuildDoDoes()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("___ you like apples?", "Do", "С you используется Do.", "Do", "Does"),
                OptionExercise("___ he like milk?", "Does", "С he используется Does.", "Do", "Does"),
                OptionExercise("___ they go to school?", "Do", "С they используется Do.", "Do", "Does"),
                OptionExercise("___ Anna have a dog?", "Does", "Anna = she, поэтому Does.", "Do", "Does")
            };
        }

        private static List<MiniGameExercise> BuildPresentSimpleBuilder()
        {
            return new List<MiniGameExercise>
            {
                ArrangeExercise("Я люблю яблоки.", "I like apples.", "I", "like", "apples"),
                ArrangeExercise("Он любит молоко.", "He likes milk.", "He", "likes", "milk"),
                ArrangeExercise("Мы ходим в школу.", "We go to school.", "We", "go", "to", "school"),
                ArrangeExercise("Она читает книги.", "She reads books.", "She", "reads", "books")
            };
        }

        private static List<MiniGameExercise> BuildPresentSimpleVsContinuous()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("I usually ___ water.", "drink", "Usually = обычное действие.", "drink", "am drinking"),
                OptionExercise("I ___ water now.", "am drinking", "Now = действие сейчас.", "drink", "am drinking"),
                OptionExercise("She often ___ books.", "reads", "Often = обычное действие.", "reads", "is reading"),
                OptionExercise("She ___ a book now.", "is reading", "Now = действие сейчас.", "reads", "is reading")
            };
        }

        private static List<MiniGameExercise> BuildWasWere()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("I ___ at home yesterday.", "was", "С I используется was.", "was", "were"),
                OptionExercise("They ___ at school.", "were", "С they используется were.", "was", "were"),
                OptionExercise("She ___ tired.", "was", "С she используется was.", "was", "were"),
                OptionExercise("We ___ happy.", "were", "С we используется were.", "was", "were")
            };
        }

        private static List<MiniGameExercise> BuildCanCant()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("Birds ___ fly.", "can", "Птицы умеют летать.", "can", "can't"),
                OptionExercise("Fish ___ walk.", "can't", "Рыбы не умеют ходить.", "can", "can't"),
                OptionExercise("A baby ___ drive a car.", "can't", "Малыш не может водить машину.", "can", "can't"),
                OptionExercise("I ___ swim.", "can", "I can swim = Я умею плавать.", "can", "can't")
            };
        }

        private static List<MiniGameExercise> BuildBigNumbers()
        {
            return new List<MiniGameExercise>
            {
                InputExercise("Напиши 20 словами.", "twenty", "20 = twenty."),
                InputExercise("Напиши 60 словами.", "sixty", "60 = sixty."),
                InputExercise("Напиши 90 словами.", "ninety", "90 = ninety."),
                InputExercise("Напиши 100 словами.", "one hundred", "100 = one hundred."),
                InputExercise("Напиши 200 словами.", "two hundred", "200 = two hundred."),
                InputExercise("Напиши 1000 словами.", "one thousand", "1000 = one thousand."),
                InputExercise("Напиши 125 словами.", "one hundred and twenty-five", "125 = one hundred and twenty-five."),
                InputExercise("Напиши 365 словами.", "three hundred and sixty-five", "365 = three hundred and sixty-five.")
            };
        }

        private static List<MiniGameExercise> BuildPastSimple()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("Yesterday I ___ football.", "played", "Play — правильный глагол: played.", "play", "played", "plays"),
                OptionExercise("I ___ go to school yesterday.", "didn't", "В отрицании Past Simple используется didn't.", "don't", "didn't", "doesn't"),
                OptionExercise("___ you watch TV yesterday?", "Did", "В вопросе Past Simple используется Did.", "Do", "Does", "Did"),
                OptionExercise("I ___ to school yesterday.", "went", "Go — неправильный глагол: went.", "goed", "went", "go"),
                OptionExercise("She ___ a cat last year.", "had", "Have — неправильный глагол: had.", "haved", "had", "has")
            };
        }

        private static List<MiniGameExercise> BuildFutureSimple()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("Tomorrow I ___ help you.", "will", "Future Simple строится с will.", "will", "did", "does"),
                OptionExercise("She ___ go tomorrow.", "won't", "Отрицание Future Simple: won't.", "doesn't", "didn't", "won't"),
                OptionExercise("___ you come tomorrow?", "Will", "В вопросе will ставится в начало.", "Do", "Did", "Will"),
                OptionExercise("He will ___ football.", "play", "После will глагол без -s.", "plays", "play", "played")
            };
        }

        private static List<MiniGameExercise> BuildTenseForms()
        {
            return new List<MiniGameExercise>
    {
        OptionExercise(
            "Какая форма показывает обычное действие или факт?",
            "Simple",
            "Simple показывает факт, привычку или обычное действие.",
            "Simple",
            "Continuous",
            "Perfect"),

        OptionExercise(
            "Какая форма показывает действие в процессе?",
            "Continuous",
            "Continuous показывает процесс: am/is/are + verb + ing.",
            "Simple",
            "Continuous",
            "Perfect"),

        OptionExercise(
            "Какая форма показывает результат или опыт?",
            "Perfect",
            "Perfect показывает результат или опыт: have/has/had + V3.",
            "Simple",
            "Continuous",
            "Perfect"),

        OptionExercise(
            "I read books every day. Это...",
            "Present Simple",
            "Every day показывает обычное действие.",
            "Present Simple",
            "Present Continuous",
            "Present Perfect"),

        OptionExercise(
            "I am reading now. Это...",
            "Present Continuous",
            "Now показывает действие сейчас.",
            "Present Simple",
            "Present Continuous",
            "Present Perfect"),

        OptionExercise(
            "I have read this book. Это...",
            "Present Perfect",
            "Have + V3 показывает результат или опыт.",
            "Present Simple",
            "Present Continuous",
            "Present Perfect"),

        OptionExercise(
            "She was watching TV at 6. Это...",
            "Past Continuous",
            "Was/were + ing показывает процесс в прошлом.",
            "Past Simple",
            "Past Continuous",
            "Past Perfect"),

        OptionExercise(
            "She had finished before dinner. Это...",
            "Past Perfect",
            "Had + V3 показывает действие раньше другого прошлого действия.",
            "Past Simple",
            "Past Continuous",
            "Past Perfect"),

        OptionExercise(
            "I will be studying at 6. Это...",
            "Future Continuous",
            "Will be + ing показывает процесс в будущем.",
            "Future Simple",
            "Future Continuous",
            "Future Perfect"),

        OptionExercise(
            "I will have finished by 6. Это...",
            "Future Perfect",
            "Will have + V3 показывает результат к моменту в будущем.",
            "Future Simple",
            "Future Continuous",
            "Future Perfect")
    };
        }

        private static List<MiniGameExercise> BuildVerbRules()
        {
            return new List<MiniGameExercise>
    {
        OptionExercise("Past Simple от go:", "went", "Go — went — gone.", "goed", "went", "gone"),
        OptionExercise("V3 от go:", "gone", "Go — went — gone.", "went", "gone", "goed"),

        OptionExercise("Past Simple от see:", "saw", "See — saw — seen.", "saw", "seen", "seed"),
        OptionExercise("V3 от see:", "seen", "See — saw — seen.", "saw", "seen", "seed"),

        OptionExercise("Past Simple от eat:", "ate", "Eat — ate — eaten.", "eated", "ate", "eaten"),
        OptionExercise("V3 от eat:", "eaten", "Eat — ate — eaten.", "ate", "eaten", "eated"),

        OptionExercise("Past Simple от write:", "wrote", "Write — wrote — written.", "writed", "wrote", "written"),
        OptionExercise("V3 от write:", "written", "Write — wrote — written.", "wrote", "written", "writed"),

        OptionExercise("Past Simple от take:", "took", "Take — took — taken.", "taked", "took", "taken"),
        OptionExercise("V3 от take:", "taken", "Take — took — taken.", "took", "taken", "taked"),

        OptionExercise("Past Simple от have:", "had", "Have — had — had.", "haved", "had", "has"),
        OptionExercise("V3 от have:", "had", "Have — had — had.", "had", "haved", "has"),

        OptionExercise("Past Simple от do:", "did", "Do — did — done.", "did", "done", "doed"),
        OptionExercise("V3 от do:", "done", "Do — did — done.", "did", "done", "doed"),

        OptionExercise("Past Simple от make:", "made", "Make — made — made.", "maked", "made", "make"),
        OptionExercise("Past Simple от come:", "came", "Come — came — come.", "comed", "came", "come"),

        OptionExercise("После did нужен глагол:",
            "без -ed",
            "После did глагол идёт в простой форме: Did you play?",
            "с -ed",
            "без -ed",
            "с -s"),

        OptionExercise("После have / has в Perfect нужна форма:",
            "V3",
            "Perfect строится как have / has + V3.",
            "V1",
            "V2",
            "V3")
    };
        }

        private static List<MiniGameExercise> BuildModalVerbsRules()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("He can ___ swim.", "swim", "После can глагол без -s.", "swim", "swims", "swimming"),
                OptionExercise("You ___ do your homework.", "must", "Must показывает обязанность.", "must", "can", "may"),
                OptionExercise("You ___ sleep more.", "should", "Should используется для совета.", "should", "mustn't", "can't"),
                OptionExercise("It ___ rain tomorrow.", "may", "May показывает возможность.", "may", "must", "can't"),
                OptionExercise("You ___ run here.", "mustn't", "Mustn't означает нельзя.", "mustn't", "should", "may")
            };
        }

        private static List<MiniGameExercise> BuildNounPluralRules()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("two ___", "cats", "Обычно добавляем -s.", "cat", "cats", "cates"),
                OptionExercise("two ___", "boxes", "Box заканчивается на x, поэтому boxes.", "boxs", "boxes", "box"),
                OptionExercise("two ___", "babies", "Baby → babies.", "babys", "babies", "baby"),
                OptionExercise("two ___", "children", "Child → children.", "childs", "children", "childes"),
                OptionExercise("two ___", "teeth", "Tooth → teeth.", "tooths", "teeth", "toothes")
            };
        }

        private static List<MiniGameExercise> BuildCountableUncountable()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("___ books", "many", "Books можно посчитать, поэтому many.", "many", "much"),
                OptionExercise("___ water", "much", "Water неисчисляемое, поэтому much.", "many", "much"),
                OptionExercise("___ apples", "many", "Apples можно посчитать.", "many", "much"),
                OptionExercise("___ milk", "much", "Milk неисчисляемое.", "many", "much")
            };
        }

        private static List<MiniGameExercise> BuildPossessiveS()
        {
            return new List<MiniGameExercise>
            {
                InputExercise("Книга Тома:", "Tom's book", "Принадлежность: Tom's book."),
                InputExercise("Сумка Анны:", "Anna's bag", "Принадлежность: Anna's bag."),
                InputExercise("Кошка моей мамы:", "my mother's cat", "Принадлежность: my mother's cat."),
                InputExercise("Парта учителя:", "teacher's desk", "Принадлежность: teacher's desk.")
            };
        }

        private static List<MiniGameExercise> BuildPartsOfSpeech()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("Book — это...", "noun", "Book называет предмет.", "noun", "verb", "adjective"),
                OptionExercise("Run — это...", "verb", "Run означает действие.", "noun", "verb", "adjective"),
                OptionExercise("Red — это...", "adjective", "Red описывает предмет.", "noun", "verb", "adjective"),
                OptionExercise("Quickly — это...", "adverb", "Quickly показывает как происходит действие.", "adverb", "noun", "preposition"),
                OptionExercise("In — это...", "preposition", "In показывает связь слов.", "verb", "adjective", "preposition")
            };
        }

        private static List<MiniGameExercise> BuildSentencePuzzle()
        {
            return new List<MiniGameExercise>
            {
                ArrangeExercise("Собери предложение.", "I am happy.", "I", "am", "happy"),
                ArrangeExercise("Собери предложение.", "This is my book.", "This", "is", "my", "book"),
                ArrangeExercise("Собери предложение.", "She has a dog.", "She", "has", "a", "dog"),
                ArrangeExercise("Собери предложение.", "We are students.", "We", "are", "students")
            };
        }

        private static List<MiniGameExercise> BuildTranslationSentenceBuilder()
        {
            return new List<MiniGameExercise>
            {
                ArrangeExercise("Я люблю молоко.", "I like milk.", "I", "like", "milk"),
                ArrangeExercise("У неё есть кошка.", "She has a cat.", "She", "has", "a", "cat"),
                ArrangeExercise("Он умеет бегать.", "He can run.", "He", "can", "run"),
                ArrangeExercise("Сегодня холодно.", "It is cold today.", "It", "is", "cold", "today")
            };
        }

        private static List<MiniGameExercise> BuildNumberTyping()
        {
            return new List<MiniGameExercise>
            {
                InputExercise("1", "one", "1 = one"),
                InputExercise("2", "two", "2 = two"),
                InputExercise("3", "three", "3 = three"),
                InputExercise("10", "ten", "10 = ten"),
                InputExercise("20", "twenty", "20 = twenty"),
                InputExercise("100", "one hundred", "100 = one hundred")
            };
        }

        private static List<MiniGameExercise> BuildDaysAndMonths()
        {
            return new List<MiniGameExercise>
            {
                ArrangeExercise("Расставь дни недели по порядку.",
                    "Monday Tuesday Wednesday Thursday Friday Saturday Sunday",
                    "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"),

                ArrangeExercise("Расставь первые 6 месяцев по порядку.",
                    "January February March April May June",
                    "January", "February", "March", "April", "May", "June")
            };
        }

        private static List<MiniGameExercise> BuildChooseReply()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("A: Hello! How are you?\nB: ___", "I am fine, thank you.", "На How are you? отвечаем: I am fine, thank you.", "I am fine, thank you.", "I am a book.", "It is red."),
                OptionExercise("A: What is your name?\nB: ___", "My name is Anna.", "На вопрос об имени отвечаем: My name is...", "My name is Anna.", "I am fine.", "It is a dog."),
                OptionExercise("A: Can you help me?\nB: ___", "Yes, I can.", "На Can you...? отвечаем: Yes, I can.", "Yes, I can.", "Yes, I am.", "Yes, I do.")
            };
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

        private static MiniGameExercise InputExercise(string prompt, string answer, string explanation)
        {
            return new MiniGameExercise
            {
                Prompt = prompt,
                Answer = answer,
                Explanation = explanation
            };
        }

        private static MiniGameExercise ArrangeExercise(string prompt, string answer, params string[] words)
        {
            return new MiniGameExercise
            {
                Prompt = prompt,
                Answer = answer,
                Explanation = "Правильный ответ: " + answer,
                WordsToArrange = words.OrderBy(x => Random.Next()).ToList()
            };
        }

        private static List<string> ShuffleLetters(string word)
        {
            return word
                .ToCharArray()
                .Select(c => c.ToString())
                .OrderBy(c => Random.Next())
                .ToList();
        }

        private static string PrepareSingleWord(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            return value
                .Trim()
                .ToLowerInvariant()
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("'", "");
        }

        private static List<string> BuildLetterOptions(char correct)
        {
            string vowels = "aeiou";
            string consonants = "bcdfghjklmnpqrstvwxyz";
            string source = vowels.Contains(correct) ? vowels : consonants;

            List<string> options = new List<string>();
            options.Add(correct.ToString());

            while (options.Count < 4)
            {
                string letter = source[Random.Next(source.Length)].ToString();

                if (!options.Contains(letter))
                    options.Add(letter);
            }

            return options.OrderBy(x => Random.Next()).ToList();
        }
    }
}