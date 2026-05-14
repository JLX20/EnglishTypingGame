using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishTypingGame
{
    public static class LessonRepository
    {
        private static readonly List<WordItem> Words = BuildWords();

        private static List<WordItem> BuildWords()
        {
            List<WordItem> words = new List<WordItem>();

            // =====================================================
            // BASIC A0-A1 VOCABULARY
            // =====================================================

            Add(words, "red", "красный", "Colours", "A0", "The apple is red.");
            Add(words, "blue", "синий", "Colours", "A0", "The sky is blue.");
            Add(words, "green", "зелёный", "Colours", "A0", "The grass is green.");
            Add(words, "yellow", "жёлтый", "Colours", "A0", "The sun is yellow.");
            Add(words, "black", "чёрный", "Colours", "A0", "My bag is black.");
            Add(words, "white", "белый", "Colours", "A0", "The snow is white.");
            Add(words, "orange", "оранжевый", "Colours", "A0", "The orange is orange.");
            Add(words, "brown", "коричневый", "Colours", "A0", "The table is brown.");

            Add(words, "one", "один", "Numbers", "A0", "I have one book.");
            Add(words, "two", "два", "Numbers", "A0", "I have two pens.");
            Add(words, "three", "три", "Numbers", "A0", "I see three cats.");
            Add(words, "four", "четыре", "Numbers", "A0", "There are four chairs.");
            Add(words, "five", "пять", "Numbers", "A0", "I have five apples.");
            Add(words, "six", "шесть", "Numbers", "A0", "Six birds are in the tree.");
            Add(words, "seven", "семь", "Numbers", "A0", "There are seven days in a week.");
            Add(words, "eight", "восемь", "Numbers", "A0", "I see eight stars.");
            Add(words, "nine", "девять", "Numbers", "A0", "Nine students are here.");
            Add(words, "ten", "десять", "Numbers", "A0", "I can count to ten.");
            Add(words, "eleven", "одиннадцать", "Numbers", "A1", "Eleven comes after ten.");
            Add(words, "twelve", "двенадцать", "Numbers", "A1", "Twelve months make a year.");

            Add(words, "cat", "кошка", "Animals", "A0", "I have a cat.");
            Add(words, "dog", "собака", "Animals", "A0", "The dog is friendly.");
            Add(words, "bird", "птица", "Animals", "A0", "A bird can fly.");
            Add(words, "fish", "рыба", "Animals", "A0", "A fish can swim.");
            Add(words, "horse", "лошадь", "Animals", "A0", "The horse is big.");
            Add(words, "rabbit", "кролик", "Animals", "A0", "The rabbit is white.");
            Add(words, "mouse", "мышь", "Animals", "A0", "The mouse is small.");
            Add(words, "lion", "лев", "Animals", "A0", "A lion is strong.");

            Add(words, "apple", "яблоко", "Food", "A0", "I like apples.");
            Add(words, "banana", "банан", "Food", "A0", "A banana is yellow.");
            Add(words, "bread", "хлеб", "Food", "A0", "I eat bread.");
            Add(words, "milk", "молоко", "Food", "A0", "I drink milk.");
            Add(words, "water", "вода", "Food", "A0", "I drink water.");
            Add(words, "egg", "яйцо", "Food", "A0", "She has an egg.");
            Add(words, "cheese", "сыр", "Food", "A0", "I like cheese.");
            Add(words, "soup", "суп", "Food", "A0", "The soup is hot.");
            Add(words, "rice", "рис", "Food", "A1", "We eat rice for lunch.");
            Add(words, "juice", "сок", "Food", "A0", "I drink orange juice.");

            Add(words, "mother", "мама", "Family", "A0", "My mother is kind.");
            Add(words, "father", "папа", "Family", "A0", "My father is at home.");
            Add(words, "sister", "сестра", "Family", "A0", "My sister is young.");
            Add(words, "brother", "брат", "Family", "A0", "My brother is tall.");
            Add(words, "family", "семья", "Family", "A0", "I love my family.");
            Add(words, "baby", "малыш", "Family", "A0", "The baby is sleeping.");
            Add(words, "grandmother", "бабушка", "Family", "A1", "My grandmother is nice.");
            Add(words, "grandfather", "дедушка", "Family", "A1", "My grandfather is old.");

            Add(words, "book", "книга", "School", "A0", "This is my book.");
            Add(words, "pen", "ручка", "School", "A0", "I have a pen.");
            Add(words, "pencil", "карандаш", "School", "A0", "This pencil is yellow.");
            Add(words, "desk", "парта", "School", "A0", "The desk is brown.");
            Add(words, "teacher", "учитель", "School", "A1", "The teacher is in the classroom.");
            Add(words, "student", "ученик", "School", "A1", "The student reads a book.");
            Add(words, "school", "школа", "School", "A0", "I go to school.");
            Add(words, "lesson", "урок", "School", "A1", "The lesson starts now.");
            Add(words, "classroom", "класс", "School", "A1", "We are in the classroom.");
            Add(words, "board", "доска", "School", "A0", "Look at the board.");

            Add(words, "house", "дом", "Home", "A0", "This is my house.");
            Add(words, "room", "комната", "Home", "A0", "My room is clean.");
            Add(words, "door", "дверь", "Home", "A0", "Open the door, please.");
            Add(words, "window", "окно", "Home", "A0", "Close the window.");
            Add(words, "table", "стол", "Home", "A0", "The book is on the table.");
            Add(words, "chair", "стул", "Home", "A0", "The chair is near the table.");
            Add(words, "bed", "кровать", "Home", "A0", "The bed is in my room.");
            Add(words, "kitchen", "кухня", "Home", "A1", "My mother is in the kitchen.");
            Add(words, "bathroom", "ванная", "Home", "A1", "The bathroom is small.");
            Add(words, "garden", "сад", "Home", "A1", "There is a garden near the house.");

            // =====================================================
            // A1-A2 VOCABULARY TOPICS
            // =====================================================

            Add(words, "shirt", "рубашка", "Clothes", "A0", "This shirt is blue.");
            Add(words, "dress", "платье", "Clothes", "A0", "Her dress is red.");
            Add(words, "shoes", "обувь", "Clothes", "A0", "My shoes are black.");
            Add(words, "hat", "шляпа", "Clothes", "A0", "He has a hat.");
            Add(words, "coat", "пальто", "Clothes", "A0", "I wear a coat in winter.");
            Add(words, "skirt", "юбка", "Clothes", "A0", "The skirt is green.");
            Add(words, "socks", "носки", "Clothes", "A0", "My socks are white.");
            Add(words, "jacket", "куртка", "Clothes", "A1", "This jacket is warm.");
            Add(words, "trousers", "брюки", "Clothes", "A1", "His trousers are black.");
            Add(words, "T-shirt", "футболка", "Clothes", "A0", "I like this T-shirt.");

            Add(words, "head", "голова", "Body", "A0", "My head hurts.");
            Add(words, "hand", "кисть / рука", "Body", "A0", "Raise your hand.");
            Add(words, "arm", "рука", "Body", "A0", "My arm is strong.");
            Add(words, "leg", "нога", "Body", "A0", "His leg hurts.");
            Add(words, "foot", "ступня", "Body", "A0", "My foot is cold.");
            Add(words, "eye", "глаз", "Body", "A0", "I have two eyes.");
            Add(words, "ear", "ухо", "Body", "A0", "I hear with my ears.");
            Add(words, "nose", "нос", "Body", "A0", "My nose is small.");
            Add(words, "mouth", "рот", "Body", "A0", "Open your mouth.");
            Add(words, "face", "лицо", "Body", "A0", "Her face is happy.");

            Add(words, "sun", "солнце", "Weather", "A0", "The sun is bright.");
            Add(words, "rain", "дождь", "Weather", "A0", "It is raining today.");
            Add(words, "snow", "снег", "Weather", "A0", "The snow is white.");
            Add(words, "wind", "ветер", "Weather", "A0", "The wind is cold.");
            Add(words, "cloud", "облако", "Weather", "A0", "There is a cloud in the sky.");
            Add(words, "hot", "жарко / горячий", "Weather", "A0", "It is hot today.");
            Add(words, "cold", "холодно / холодный", "Weather", "A0", "It is cold outside.");
            Add(words, "warm", "тёплый", "Weather", "A0", "It is warm in spring.");
            Add(words, "cool", "прохладный", "Weather", "A1", "It is cool in the evening.");
            Add(words, "storm", "буря", "Weather", "A1", "The storm is strong.");

            Add(words, "car", "машина", "Transport", "A0", "This car is fast.");
            Add(words, "bus", "автобус", "Transport", "A0", "I go to school by bus.");
            Add(words, "train", "поезд", "Transport", "A0", "The train is long.");
            Add(words, "plane", "самолёт", "Transport", "A0", "A plane can fly.");
            Add(words, "bike", "велосипед", "Transport", "A0", "I ride my bike.");
            Add(words, "boat", "лодка", "Transport", "A0", "The boat is small.");
            Add(words, "taxi", "такси", "Transport", "A0", "We take a taxi.");
            Add(words, "ship", "корабль", "Transport", "A1", "The ship is big.");
            Add(words, "station", "станция", "Transport", "A1", "The station is near the school.");
            Add(words, "airport", "аэропорт", "Transport", "A1", "The airport is big.");

            Add(words, "park", "парк", "Places", "A0", "I go to the park.");
            Add(words, "shop", "магазин", "Places", "A0", "The shop is open.");
            Add(words, "city", "город", "Places", "A0", "The city is big.");
            Add(words, "street", "улица", "Places", "A0", "The street is long.");
            Add(words, "hospital", "больница", "Places", "A1", "The hospital is near.");
            Add(words, "library", "библиотека", "Places", "A1", "I read in the library.");
            Add(words, "cinema", "кинотеатр", "Places", "A1", "We go to the cinema.");
            Add(words, "bank", "банк", "Places", "A1", "The bank is on this street.");
            Add(words, "cafe", "кафе", "Places", "A1", "We meet in a cafe.");
            Add(words, "market", "рынок", "Places", "A1", "I buy fruit at the market.");

            Add(words, "wake", "просыпаться", "DailyRoutine", "A1", "I wake up at seven.");
            Add(words, "wash", "мыть", "DailyRoutine", "A1", "I wash my face.");
            Add(words, "eat", "есть / кушать", "DailyRoutine", "A0", "I eat breakfast.");
            Add(words, "drink", "пить", "DailyRoutine", "A0", "I drink water.");
            Add(words, "sleep", "спать", "DailyRoutine", "A0", "I sleep at night.");
            Add(words, "read", "читать", "DailyRoutine", "A0", "I read a book.");
            Add(words, "write", "писать", "DailyRoutine", "A0", "I write a sentence.");
            Add(words, "work", "работать", "DailyRoutine", "A1", "My father works every day.");
            Add(words, "study", "учиться / изучать", "DailyRoutine", "A1", "I study English.");
            Add(words, "watch", "смотреть", "DailyRoutine", "A1", "I watch TV in the evening.");

            Add(words, "run", "бегать", "Actions", "A0", "I can run.");
            Add(words, "jump", "прыгать", "Actions", "A0", "The dog can jump.");
            Add(words, "swim", "плавать", "Actions", "A0", "I can swim.");
            Add(words, "go", "идти / ехать", "Actions", "A0", "I go to school.");
            Add(words, "come", "приходить", "Actions", "A0", "Come here, please.");
            Add(words, "play", "играть", "Actions", "A0", "I play football.");
            Add(words, "look", "смотреть", "Actions", "A0", "Look at the board.");
            Add(words, "listen", "слушать", "Actions", "A0", "Listen to the teacher.");
            Add(words, "speak", "говорить", "Actions", "A1", "I speak English.");
            Add(words, "open", "открывать", "Actions", "A0", "Open your book.");
            Add(words, "close", "закрывать", "Actions", "A0", "Close the door.");
            Add(words, "help", "помогать", "Actions", "A0", "Can you help me?");
            Add(words, "like", "нравиться / любить", "Actions", "A0", "I like apples.");
            Add(words, "love", "любить", "Actions", "A0", "I love my family.");
            Add(words, "live", "жить", "Actions", "A1", "I live in a small town.");

            Add(words, "happy", "счастливый", "Feelings", "A0", "I am happy.");
            Add(words, "sad", "грустный", "Feelings", "A0", "He is sad.");
            Add(words, "tired", "уставший", "Feelings", "A0", "She is tired.");
            Add(words, "angry", "злой", "Feelings", "A1", "The boy is angry.");
            Add(words, "scared", "испуганный", "Feelings", "A1", "I am scared.");
            Add(words, "hungry", "голодный", "Feelings", "A0", "I am hungry.");
            Add(words, "thirsty", "хочет пить / испытывает жажду", "Feelings", "A0", "I am thirsty.");
            Add(words, "excited", "взволнованный", "Feelings", "A1", "We are excited.");

            Add(words, "big", "большой", "Adjectives", "A0", "The house is big.");
            Add(words, "small", "маленький", "Adjectives", "A0", "The cat is small.");
            Add(words, "fast", "быстрый", "Adjectives", "A0", "The car is fast.");
            Add(words, "slow", "медленный", "Adjectives", "A0", "The bus is slow.");
            Add(words, "good", "хороший", "Adjectives", "A0", "This is a good book.");
            Add(words, "bad", "плохой", "Adjectives", "A0", "This is a bad idea.");
            Add(words, "new", "новый", "Adjectives", "A0", "I have a new pen.");
            Add(words, "old", "старый", "Adjectives", "A0", "This is an old house.");
            Add(words, "young", "молодой", "Adjectives", "A0", "My brother is young.");
            Add(words, "nice", "милый / хороший", "Adjectives", "A0", "She is nice.");

            // =====================================================
            // GRAMMAR SUPPORT WORDS
            // Исправлены переводы: теперь в LearnWindow не будет
            // показываться просто английское слово вместо перевода.
            // =====================================================

            Add(words, "a", "неопределённый артикль перед согласным звуком", "Articles", "A1", "I have a dog.");
            Add(words, "an", "неопределённый артикль перед гласным звуком", "Articles", "A1", "She has an apple.");
            Add(words, "the", "определённый артикль", "Articles", "A1", "Open the door, please.");

            Add(words, "I", "я", "Pronouns", "A0", "I am happy.");
            Add(words, "you", "ты / вы", "Pronouns", "A0", "You are my friend.");
            Add(words, "he", "он", "Pronouns", "A0", "He is a student.");
            Add(words, "she", "она", "Pronouns", "A0", "She has a cat.");
            Add(words, "it", "это / оно", "Pronouns", "A0", "It is red.");
            Add(words, "we", "мы", "Pronouns", "A0", "We are students.");
            Add(words, "they", "они", "Pronouns", "A0", "They are friends.");
            Add(words, "my", "мой / моя / моё", "Pronouns", "A0", "This is my book.");
            Add(words, "your", "твой / ваш", "Pronouns", "A0", "What is your name?");
            Add(words, "his", "его", "Pronouns", "A1", "This is his bag.");
            Add(words, "her", "её", "Pronouns", "A1", "This is her dog.");

            Add(words, "am", "форма глагола to be для I", "ToBe", "A0", "I am happy.");
            Add(words, "is", "форма глагола to be для he / she / it", "ToBe", "A0", "She is a student.");
            Add(words, "are", "форма глагола to be для you / we / they", "ToBe", "A0", "They are friends.");
            Add(words, "was", "был / была", "ToBe", "A1", "I was at home yesterday.");
            Add(words, "were", "были / был", "ToBe", "A1", "They were at school.");

            Add(words, "have", "иметь / у меня есть", "AuxiliaryVerbs", "A0", "I have a book.");
            Add(words, "has", "имеет / у него или неё есть", "AuxiliaryVerbs", "A0", "She has a cat.");
            Add(words, "do", "вспомогательный глагол для вопросов с I / you / we / they", "AuxiliaryVerbs", "A1", "Do you like apples?");
            Add(words, "does", "вспомогательный глагол для вопросов с he / she / it", "AuxiliaryVerbs", "A1", "Does he like milk?");

            Add(words, "can", "мочь / уметь", "ModalVerbs", "A0", "I can swim.");
            Add(words, "can't", "не мочь / не уметь", "ModalVerbs", "A0", "Fish can't walk.");

            Add(words, "now", "сейчас", "TimeMarkers", "A1", "I am reading now.");
            Add(words, "usually", "обычно", "TimeMarkers", "A1", "I usually drink water.");
            Add(words, "often", "часто", "TimeMarkers", "A1", "She often reads books.");
            Add(words, "every day", "каждый день", "TimeMarkers", "A1", "I go to school every day.");
            Add(words, "today", "сегодня", "TimeMarkers", "A0", "It is cold today.");
            Add(words, "yesterday", "вчера", "TimeMarkers", "A1", "I was at home yesterday.");
            Add(words, "tomorrow", "завтра", "TimeMarkers", "A1", "See you tomorrow.");

            Add(words, "what", "что / какой", "QuestionWords", "A0", "What is your name?");
            Add(words, "where", "где / куда", "QuestionWords", "A1", "Where are you from?");
            Add(words, "who", "кто", "QuestionWords", "A1", "Who is your teacher?");
            Add(words, "how", "как", "QuestionWords", "A0", "How are you?");
            Add(words, "why", "почему", "QuestionWords", "A1", "Why are you sad?");

            // =====================================================
            // DAYS AND MONTHS
            // =====================================================

            Add(words, "Monday", "понедельник", "Days", "A1", "Monday is the first school day.");
            Add(words, "Tuesday", "вторник", "Days", "A1", "Tuesday comes after Monday.");
            Add(words, "Wednesday", "среда", "Days", "A1", "Wednesday is in the middle of the week.");
            Add(words, "Thursday", "четверг", "Days", "A1", "Thursday comes after Wednesday.");
            Add(words, "Friday", "пятница", "Days", "A1", "Friday is before Saturday.");
            Add(words, "Saturday", "суббота", "Days", "A1", "Saturday is a weekend day.");
            Add(words, "Sunday", "воскресенье", "Days", "A1", "Sunday is a weekend day.");

            Add(words, "January", "январь", "Months", "A1", "January is the first month.");
            Add(words, "February", "февраль", "Months", "A1", "February is the second month.");
            Add(words, "March", "март", "Months", "A1", "March comes after February.");
            Add(words, "April", "апрель", "Months", "A1", "April is in spring.");
            Add(words, "May", "май", "Months", "A1", "May is a spring month.");
            Add(words, "June", "июнь", "Months", "A1", "June is in summer.");
            Add(words, "July", "июль", "Months", "A1", "July is a summer month.");
            Add(words, "August", "август", "Months", "A1", "August comes after July.");
            Add(words, "September", "сентябрь", "Months", "A1", "September is in autumn.");
            Add(words, "October", "октябрь", "Months", "A1", "October comes after September.");
            Add(words, "November", "ноябрь", "Months", "A1", "November is in autumn.");
            Add(words, "December", "декабрь", "Months", "A1", "December is the last month.");

            // =====================================================
            // SIMPLE DIALOGUE PHRASES
            // =====================================================

            Add(words, "hello", "привет", "DialoguePhrases", "A0", "Hello! How are you?");
            Add(words, "good morning", "доброе утро", "DialoguePhrases", "A0", "Good morning!");
            Add(words, "good afternoon", "добрый день", "DialoguePhrases", "A1", "Good afternoon!");
            Add(words, "good evening", "добрый вечер", "DialoguePhrases", "A1", "Good evening!");
            Add(words, "goodbye", "до свидания", "DialoguePhrases", "A0", "Goodbye! See you tomorrow.");
            Add(words, "thank you", "спасибо", "DialoguePhrases", "A0", "Thank you very much.");
            Add(words, "please", "пожалуйста", "DialoguePhrases", "A0", "Please help me.");
            Add(words, "sorry", "извини / простите", "DialoguePhrases", "A0", "Sorry, I am late.");
            Add(words, "I am fine", "у меня всё хорошо", "DialoguePhrases", "A0", "I am fine, thank you.");
            Add(words, "my name is", "меня зовут", "DialoguePhrases", "A0", "My name is Anna.");
            Add(words, "nice to meet you", "приятно познакомиться", "DialoguePhrases", "A1", "Nice to meet you.");
            Add(words, "see you", "увидимся", "DialoguePhrases", "A0", "See you tomorrow.");

            // =====================================================
            // SENTENCE BUILDING SUPPORT
            // =====================================================

            Add(words, "this", "это", "SentenceWords", "A0", "This is my book.");
            Add(words, "that", "то / тот", "SentenceWords", "A1", "That is your bag.");
            Add(words, "here", "здесь", "SentenceWords", "A0", "I live here.");
            Add(words, "there", "там", "SentenceWords", "A1", "The school is there.");
            Add(words, "at", "в / у / на", "SentenceWords", "A1", "I am at home.");
            Add(words, "to", "к / в", "SentenceWords", "A0", "I go to school.");
            Add(words, "from", "из", "SentenceWords", "A1", "I am from Finland.");
            Add(words, "with", "с", "SentenceWords", "A1", "I play with my dog.");
            Add(words, "and", "и", "SentenceWords", "A0", "I have a book and a pen.");
            Add(words, "but", "но", "SentenceWords", "A1", "I like tea, but I don't like coffee.");

            return words;
        }

        private static void Add(List<WordItem> words, string english, string russian, string topic, string level, string example)
        {
            words.Add(new WordItem(english, russian, topic, level, example));
        }

        public static List<string> GetTopicsForUi()
        {
            return new List<string>
            {
                "Все темы",

                "Colours — Цвета",
                "Numbers — Числа",
                "Animals — Животные",
                "Food — Еда",
                "Family — Семья",
                "School — Школа",
                "Home — Дом",

                "Clothes — Одежда",
                "Body — Тело",
                "Weather — Погода",
                "Transport — Транспорт",
                "Places — Места",
                "DailyRoutine — Распорядок дня",
                "Actions — Действия",
                "Feelings — Чувства",
                "Adjectives — Прилагательные",

                "Articles — Артикли",
                "Pronouns — Местоимения",
                "ToBe — Глагол to be",
                "AuxiliaryVerbs — Have / Has / Do / Does",
                "ModalVerbs — Can / Can't",
                "TimeMarkers — Маркеры времени",
                "QuestionWords — Вопросительные слова",

                "Days — Дни недели",
                "Months — Месяцы",
                "DialoguePhrases — Диалоговые фразы",
                "SentenceWords — Слова для предложений"
            };
        }

        public static List<WordItem> GetAllWords()
        {
            return Words.ToList();
        }

        public static List<WordItem> GetWords(string topicUi)
        {
            string topic = NormalizeTopic(topicUi);

            if (string.IsNullOrEmpty(topic))
                return Words.ToList();

            return Words
                .Where(w => w.Topic.Equals(topic, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public static WordItem FindWordByEnglish(string english)
        {
            return Words.FirstOrDefault(w =>
                w.English.Equals(english, StringComparison.OrdinalIgnoreCase));
        }

        private static string NormalizeTopic(string topicUi)
        {
            if (string.IsNullOrWhiteSpace(topicUi))
                return "";

            if (topicUi == "Все темы")
                return "";

            string[] parts = topicUi.Split(new[] { " — " }, StringSplitOptions.None);
            return parts[0].Trim();
        }
    }
}