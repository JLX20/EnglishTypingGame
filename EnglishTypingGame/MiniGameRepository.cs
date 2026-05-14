using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishTypingGame
{
    public static class MiniGameRepository
    {
        private static readonly Random Random = new Random();

        public static List<MiniGameInfo> GetMiniGames()
        {
            return new List<MiniGameInfo>
            {
                new MiniGameInfo
                {
                    Mode = MiniGameMode.WordCards,
                    Title = "Word Cards",
                    Group = "Слова",
                    Description = "Карточки для повторения слов из выбранной темы."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.MemoryPairs,
                    Title = "Memory Pairs",
                    Group = "Слова",
                    Description = "Найди пары: английское слово и русский перевод."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.TranslationMatch,
                    Title = "Translation Match",
                    Group = "Слова",
                    Description = "Соедини английские слова с переводами."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.WordBuilder,
                    Title = "Word Builder",
                    Group = "Слова",
                    Description = "Собери английское слово из букв."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.MissingLetter,
                    Title = "Missing Letter",
                    Group = "Слова",
                    Description = "Выбери пропущенную букву в слове."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.SpeedTranslation,
                    Title = "Speed Translation",
                    Group = "Слова",
                    Description = "Быстро переводи слова на английский."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.LetterRain,
                    Title = "Letter Rain",
                    Group = "Слова",
                    Description = "Лови буквы в правильном порядке."
                },

                new MiniGameInfo
                {
                    Mode = MiniGameMode.ArticleGame,
                    Title = "The Article Game",
                    Group = "Грамматика",
                    Description = "Выбери a, an или the."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.HaveHas,
                    Title = "Have / Has",
                    Group = "Грамматика",
                    Description = "Выбери have или has."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.DoDoes,
                    Title = "Do / Does",
                    Group = "Грамматика",
                    Description = "Выбери do или does в вопросах Present Simple."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.PresentSimpleBuilder,
                    Title = "Present Simple Builder",
                    Group = "Грамматика",
                    Description = "Собери простое предложение в Present Simple."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.PresentSimpleVsContinuous,
                    Title = "Present Simple vs Present Continuous",
                    Group = "Грамматика",
                    Description = "Выбери Present Simple или Present Continuous."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.WasWere,
                    Title = "Was / Were",
                    Group = "Грамматика",
                    Description = "Выбери was или were."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.CanCant,
                    Title = "Can / Can't",
                    Group = "Грамматика",
                    Description = "Выбери can или can't."
                },

                new MiniGameInfo
                {
                    Mode = MiniGameMode.SentencePuzzle,
                    Title = "Sentence Puzzle",
                    Group = "Предложения",
                    Description = "Собери английское предложение из слов."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.TranslationSentenceBuilder,
                    Title = "Translation Sentence Builder",
                    Group = "Предложения",
                    Description = "Собери английское предложение по русскому переводу."
                },

                new MiniGameInfo
                {
                    Mode = MiniGameMode.NumberTyping,
                    Title = "Number Typing",
                    Group = "Числа и даты",
                    Description = "Напиши число английским словом."
                },
                new MiniGameInfo
                {
                    Mode = MiniGameMode.DaysAndMonths,
                    Title = "Days and Months",
                    Group = "Числа и даты",
                    Description = "Расставь дни недели или месяцы по порядку."
                },

                new MiniGameInfo
                {
                    Mode = MiniGameMode.ChooseReply,
                    Title = "Choose the Reply",
                    Group = "Диалоги",
                    Description = "Выбери правильный ответ в простом диалоге."
                }
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

                case MiniGameMode.SentencePuzzle:
                    return BuildSentencePuzzle();

                case MiniGameMode.TranslationSentenceBuilder:
                    return BuildTranslationSentenceBuilder();

                case MiniGameMode.PresentSimpleVsContinuous:
                    return BuildPresentSimpleVsContinuous();

                case MiniGameMode.WasWere:
                    return BuildWasWere();

                case MiniGameMode.CanCant:
                    return BuildCanCant();

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

            return words
                .Where(w => !string.IsNullOrWhiteSpace(w.English))
                .OrderBy(w => Random.Next())
                .Take(count)
                .ToList();
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

        private static List<string> ShuffleLetters(string word)
        {
            List<string> letters = word
                .ToCharArray()
                .Select(c => c.ToString())
                .ToList();

            return letters.OrderBy(c => Random.Next()).ToList();
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

                List<string> options = BuildLetterOptions(correct);

                exercises.Add(new MiniGameExercise
                {
                    Prompt = hidden + " — " + word.Russian,
                    RussianPrompt = word.Russian,
                    Answer = correct.ToString(),
                    Options = options,
                    Explanation = "Полное слово: " + cleanWord,
                    Word = new WordItem(cleanWord, word.Russian, word.Topic, word.Level, word.Example)
                });
            }

            return exercises;
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

        private static List<MiniGameExercise> BuildSpeedTranslation(string topic)
        {
            return GetRandomWords(topic, 25)
                .Select(w => new MiniGameExercise
                {
                    Prompt = w.Russian,
                    RussianPrompt = w.Russian,
                    Answer = PrepareSingleWord(w.English),
                    Explanation = "Правильный ответ: " + w.English + ".",
                    Word = w
                })
                .ToList();
        }

        // =========================================================
        // GRAMMAR EXERCISES
        // =========================================================

        private static List<MiniGameExercise> BuildArticleGame()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("I have ___ dog.", "a", "Dog начинается с согласного звука: a dog.", "a", "an", "the"),
                OptionExercise("She has ___ apple.", "an", "Apple начинается с гласного звука: an apple.", "a", "an", "the"),
                OptionExercise("This is ___ egg.", "an", "Egg начинается с гласного звука: an egg.", "a", "an", "the"),
                OptionExercise("He has ___ book.", "a", "Book начинается с согласного звука: a book.", "a", "an", "the"),
                OptionExercise("Open ___ door, please.", "the", "Мы говорим о конкретной двери: the door.", "a", "an", "the"),
                OptionExercise("Look at ___ board.", "the", "В классе понятно, о какой доске речь: the board.", "a", "an", "the"),
                OptionExercise("I see ___ orange.", "an", "Orange начинается с гласного звука: an orange.", "a", "an", "the"),
                OptionExercise("This is ___ table.", "a", "Table начинается с согласного звука: a table.", "a", "an", "the")
            };
        }

        private static List<MiniGameExercise> BuildHaveHas()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("I ___ a book.", "have", "С I используется have.", "have", "has"),
                OptionExercise("You ___ a pen.", "have", "С you используется have.", "have", "has"),
                OptionExercise("We ___ a lesson today.", "have", "С we используется have.", "have", "has"),
                OptionExercise("They ___ a dog.", "have", "С they используется have.", "have", "has"),
                OptionExercise("He ___ a bike.", "has", "С he используется has.", "have", "has"),
                OptionExercise("She ___ a cat.", "has", "С she используется has.", "have", "has"),
                OptionExercise("Tom ___ a new bag.", "has", "Tom = he, поэтому has.", "have", "has"),
                OptionExercise("My sister ___ a red dress.", "has", "My sister = she, поэтому has.", "have", "has")
            };
        }

        private static List<MiniGameExercise> BuildDoDoes()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("___ you like apples?", "Do", "С you в вопросе используется Do.", "Do", "Does"),
                OptionExercise("___ they go to school?", "Do", "С they используется Do.", "Do", "Does"),
                OptionExercise("___ we have English today?", "Do", "С we используется Do.", "Do", "Does"),
                OptionExercise("___ he like milk?", "Does", "С he используется Does.", "Do", "Does"),
                OptionExercise("___ she play tennis?", "Does", "С she используется Does.", "Do", "Does"),
                OptionExercise("___ Tom live here?", "Does", "Tom = he, поэтому Does.", "Do", "Does"),
                OptionExercise("___ your friends speak English?", "Do", "Your friends = they, поэтому Do.", "Do", "Does"),
                OptionExercise("___ Anna have a dog?", "Does", "Anna = she, поэтому Does.", "Do", "Does")
            };
        }

        private static List<MiniGameExercise> BuildPresentSimpleBuilder()
        {
            return new List<MiniGameExercise>
            {
                ArrangeExercise("Собери предложение: Я люблю яблоки.", "I like apples.", "I", "like", "apples"),
                ArrangeExercise("Собери предложение: Он любит молоко.", "He likes milk.", "He", "likes", "milk"),
                ArrangeExercise("Собери предложение: Она читает книги.", "She reads books.", "She", "reads", "books"),
                ArrangeExercise("Собери предложение: Мы ходим в школу.", "We go to school.", "We", "go", "to", "school"),
                ArrangeExercise("Собери предложение: Они играют в футбол.", "They play football.", "They", "play", "football"),
                ArrangeExercise("Собери предложение: Том живёт здесь.", "Tom lives here.", "Tom", "lives", "here"),
                ArrangeExercise("Собери предложение: Моя мама работает каждый день.", "My mother works every day.", "My", "mother", "works", "every", "day"),
                ArrangeExercise("Собери предложение: Я изучаю английский.", "I study English.", "I", "study", "English")
            };
        }

        private static List<MiniGameExercise> BuildPresentSimpleVsContinuous()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("I usually ___ water in the morning.", "drink", "Usually показывает обычное действие: Present Simple.", "drink", "am drinking"),
                OptionExercise("I ___ water now.", "am drinking", "Now показывает действие сейчас: Present Continuous.", "drink", "am drinking"),
                OptionExercise("She often ___ books.", "reads", "Often показывает обычное действие.", "reads", "is reading"),
                OptionExercise("She ___ a book now.", "is reading", "Now показывает действие сейчас.", "reads", "is reading"),
                OptionExercise("They ___ football every day.", "play", "Every day показывает Present Simple.", "play", "are playing"),
                OptionExercise("They ___ football now.", "are playing", "Now показывает Present Continuous.", "play", "are playing"),
                OptionExercise("He usually ___ to school by bus.", "goes", "Usually показывает Present Simple.", "goes", "is going"),
                OptionExercise("He ___ to school now.", "is going", "Now показывает Present Continuous.", "goes", "is going")
            };
        }

        private static List<MiniGameExercise> BuildWasWere()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("I ___ at home yesterday.", "was", "С I используется was.", "was", "were"),
                OptionExercise("He ___ at school yesterday.", "was", "С he используется was.", "was", "were"),
                OptionExercise("She ___ tired yesterday.", "was", "С she используется was.", "was", "were"),
                OptionExercise("It ___ cold yesterday.", "was", "С it используется was.", "was", "were"),
                OptionExercise("We ___ in the classroom.", "were", "С we используется were.", "was", "were"),
                OptionExercise("They ___ happy.", "were", "С they используется were.", "was", "were"),
                OptionExercise("You ___ right.", "were", "С you используется were.", "was", "were"),
                OptionExercise("My friends ___ at the park.", "were", "My friends = they, поэтому were.", "was", "were")
            };
        }

        private static List<MiniGameExercise> BuildCanCant()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise("Birds ___ fly.", "can", "Птицы умеют летать.", "can", "can't"),
                OptionExercise("Fish ___ walk.", "can't", "Рыбы не умеют ходить.", "can", "can't"),
                OptionExercise("I ___ swim.", "can", "I can swim = Я умею плавать.", "can", "can't"),
                OptionExercise("A baby ___ drive a car.", "can't", "Малыш не может водить машину.", "can", "can't"),
                OptionExercise("A dog ___ run.", "can", "Собака может бегать.", "can", "can't"),
                OptionExercise("A cat ___ read a book.", "can't", "Кошка не умеет читать книгу.", "can", "can't"),
                OptionExercise("We ___ speak English.", "can", "We can speak English = Мы можем говорить по-английски.", "can", "can't"),
                OptionExercise("A chair ___ jump.", "can't", "Стул не может прыгать.", "can", "can't")
            };
        }

        // =========================================================
        // SENTENCES
        // =========================================================

        private static List<MiniGameExercise> BuildSentencePuzzle()
        {
            return new List<MiniGameExercise>
            {
                ArrangeExercise("Собери предложение.", "I am happy.", "I", "am", "happy"),
                ArrangeExercise("Собери предложение.", "This is my book.", "This", "is", "my", "book"),
                ArrangeExercise("Собери предложение.", "She has a dog.", "She", "has", "a", "dog"),
                ArrangeExercise("Собери предложение.", "We are students.", "We", "are", "students"),
                ArrangeExercise("Собери предложение.", "They can swim.", "They", "can", "swim"),
                ArrangeExercise("Собери предложение.", "He likes bananas.", "He", "likes", "bananas"),
                ArrangeExercise("Собери предложение.", "I go to school every day.", "I", "go", "to", "school", "every", "day"),
                ArrangeExercise("Собери предложение.", "The bus is near the station.", "The", "bus", "is", "near", "the", "station")
            };
        }

        private static List<MiniGameExercise> BuildTranslationSentenceBuilder()
        {
            return new List<MiniGameExercise>
            {
                ArrangeExercise("Я люблю молоко.", "I like milk.", "I", "like", "milk"),
                ArrangeExercise("У неё есть кошка.", "She has a cat.", "She", "has", "a", "cat"),
                ArrangeExercise("Он умеет бегать.", "He can run.", "He", "can", "run"),
                ArrangeExercise("Это моя книга.", "This is my book.", "This", "is", "my", "book"),
                ArrangeExercise("Мы ходим в школу.", "We go to school.", "We", "go", "to", "school"),
                ArrangeExercise("Они дома.", "They are at home.", "They", "are", "at", "home"),
                ArrangeExercise("Мой брат играет в футбол.", "My brother plays football.", "My", "brother", "plays", "football"),
                ArrangeExercise("Сегодня холодно.", "It is cold today.", "It", "is", "cold", "today")
            };
        }

        // =========================================================
        // NUMBERS, DAYS, MONTHS
        // =========================================================

        private static List<MiniGameExercise> BuildNumberTyping()
        {
            return new List<MiniGameExercise>
            {
                InputExercise("1", "one", "1 = one"),
                InputExercise("2", "two", "2 = two"),
                InputExercise("3", "three", "3 = three"),
                InputExercise("4", "four", "4 = four"),
                InputExercise("5", "five", "5 = five"),
                InputExercise("6", "six", "6 = six"),
                InputExercise("7", "seven", "7 = seven"),
                InputExercise("8", "eight", "8 = eight"),
                InputExercise("9", "nine", "9 = nine"),
                InputExercise("10", "ten", "10 = ten"),
                InputExercise("11", "eleven", "11 = eleven"),
                InputExercise("12", "twelve", "12 = twelve")
            };
        }

        private static List<MiniGameExercise> BuildDaysAndMonths()
        {
            return new List<MiniGameExercise>
            {
                ArrangeExercise(
                    "Расставь дни недели по порядку.",
                    "Monday Tuesday Wednesday Thursday Friday Saturday Sunday",
                    "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"),

                ArrangeExercise(
                    "Расставь первые 6 месяцев по порядку.",
                    "January February March April May June",
                    "January", "February", "March", "April", "May", "June"),

                ArrangeExercise(
                    "Расставь последние 6 месяцев по порядку.",
                    "July August September October November December",
                    "July", "August", "September", "October", "November", "December")
            };
        }

        // =========================================================
        // SIMPLE DIALOGUES
        // =========================================================

        private static List<MiniGameExercise> BuildChooseReply()
        {
            return new List<MiniGameExercise>
            {
                OptionExercise(
                    "A: Hello! How are you?\nB: ___",
                    "I am fine, thank you.",
                    "На How are you? можно ответить: I am fine, thank you.",
                    "I am fine, thank you.",
                    "I am a book.",
                    "It is red."),

                OptionExercise(
                    "A: What is your name?\nB: ___",
                    "My name is Anna.",
                    "На вопрос об имени отвечаем: My name is...",
                    "My name is Anna.",
                    "I am fine.",
                    "It is a dog."),

                OptionExercise(
                    "A: Nice to meet you.\nB: ___",
                    "Nice to meet you too.",
                    "Вежливый ответ: Nice to meet you too.",
                    "Nice to meet you too.",
                    "I have a pen.",
                    "It is Monday."),

                OptionExercise(
                    "A: How old are you?\nB: ___",
                    "I am ten.",
                    "На вопрос о возрасте отвечаем: I am ten.",
                    "I am ten.",
                    "I am fine.",
                    "This is my book."),

                OptionExercise(
                    "A: Can you help me?\nB: ___",
                    "Yes, I can.",
                    "На Can you...? можно ответить: Yes, I can.",
                    "Yes, I can.",
                    "Yes, I am.",
                    "Yes, I do."),

                OptionExercise(
                    "A: Do you like apples?\nB: ___",
                    "Yes, I do.",
                    "На Do you...? можно ответить: Yes, I do.",
                    "Yes, I do.",
                    "Yes, I can.",
                    "Yes, I am."),

                OptionExercise(
                    "A: Thank you.\nB: ___",
                    "You're welcome.",
                    "После Thank you можно сказать: You're welcome.",
                    "You're welcome.",
                    "Good morning.",
                    "My name is Tom."),

                OptionExercise(
                    "A: Goodbye!\nB: ___",
                    "Goodbye!",
                    "На Goodbye можно ответить Goodbye.",
                    "Goodbye!",
                    "I am hungry.",
                    "It is a cat."),

                OptionExercise(
                    "A: Good morning!\nB: ___",
                    "Good morning!",
                    "На Good morning можно ответить Good morning.",
                    "Good morning!",
                    "Good night!",
                    "I have a dog."),

                OptionExercise(
                    "A: Where are you from?\nB: ___",
                    "I am from Finland.",
                    "На Where are you from? отвечаем: I am from...",
                    "I am from Finland.",
                    "I am ten.",
                    "It is blue.")
            };
        }

        // =========================================================
        // HELPERS
        // =========================================================

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
    }
}