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

            AddColours(words);
            AddNumbers(words);
            AddAnimals(words);
            AddFood(words);
            AddFamily(words);
            AddSchool(words);
            AddHome(words);
            AddClothes(words);
            AddBody(words);
            AddWeather(words);
            AddTransport(words);
            AddPlaces(words);
            AddDailyRoutine(words);
            AddActions(words);
            AddFeelings(words);
            AddAdjectives(words);
            AddSports(words);
            AddJobs(words);
            AddNature(words);
            AddShopping(words);
            AddDialoguePhrases(words);

            return words;
        }

        // =====================================================
        // COLOURS
        // =====================================================

        private static void AddColours(List<WordItem> words)
        {
            Add(words, "red", "красный", "Colours", "A0", "The apple is red.");
            Add(words, "blue", "синий", "Colours", "A0", "The sky is blue.");
            Add(words, "green", "зелёный", "Colours", "A0", "The grass is green.");
            Add(words, "yellow", "жёлтый", "Colours", "A0", "The sun is yellow.");
            Add(words, "black", "чёрный", "Colours", "A0", "My bag is black.");
            Add(words, "white", "белый", "Colours", "A0", "The snow is white.");
            Add(words, "orange", "оранжевый", "Colours", "A0", "The orange is orange.");
            Add(words, "brown", "коричневый", "Colours", "A0", "The table is brown.");
            Add(words, "pink", "розовый", "Colours", "A0", "Her dress is pink.");
            Add(words, "purple", "фиолетовый", "Colours", "A0", "The flower is purple.");
            Add(words, "grey", "серый", "Colours", "A1", "The cloud is grey.");
            Add(words, "gold", "золотой", "Colours", "A1", "The ring is gold.");
            Add(words, "silver", "серебряный", "Colours", "A1", "The spoon is silver.");
            Add(words, "dark", "тёмный", "Colours", "A1", "The room is dark.");
            Add(words, "light", "светлый", "Colours", "A1", "The wall is light.");
        }

        // =====================================================
        // NUMBERS
        // Убраны 21-29.
        // Убраны последние три составных числа:
        // one hundred and one, two hundred and fifty,
        // three hundred and sixty-five.
        // Цифры расставлены по возрастанию.
        // =====================================================

        private static void AddNumbers(List<WordItem> words)
        {
            Add(words, "zero", "ноль", "Numbers", "A0", "Zero means nothing.");
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
            Add(words, "thirteen", "тринадцать", "Numbers", "A1", "Thirteen comes after twelve.");
            Add(words, "fourteen", "четырнадцать", "Numbers", "A1", "Fourteen comes after thirteen.");
            Add(words, "fifteen", "пятнадцать", "Numbers", "A1", "Fifteen comes after fourteen.");
            Add(words, "sixteen", "шестнадцать", "Numbers", "A1", "Sixteen comes after fifteen.");
            Add(words, "seventeen", "семнадцать", "Numbers", "A1", "Seventeen comes after sixteen.");
            Add(words, "eighteen", "восемнадцать", "Numbers", "A1", "Eighteen comes after seventeen.");
            Add(words, "nineteen", "девятнадцать", "Numbers", "A1", "Nineteen comes after eighteen.");

            Add(words, "twenty", "двадцать", "Numbers", "A1", "Twenty students are in the class.");
            Add(words, "thirty", "тридцать", "Numbers", "A1", "Thirty days make a month.");
            Add(words, "forty", "сорок", "Numbers", "A1", "My father is forty.");
            Add(words, "fifty", "пятьдесят", "Numbers", "A1", "Fifty is a big number.");
            Add(words, "sixty", "шестьдесят", "Numbers", "A1", "One hour has sixty minutes.");
            Add(words, "seventy", "семьдесят", "Numbers", "A1", "Seventy is after sixty.");
            Add(words, "eighty", "восемьдесят", "Numbers", "A1", "Eighty is a big number.");
            Add(words, "ninety", "девяносто", "Numbers", "A1", "Ninety is before one hundred.");

            Add(words, "one hundred", "сто", "Numbers", "A1", "One hundred is 100.");
            Add(words, "two hundred", "двести", "Numbers", "A1", "Two hundred is 200.");
            Add(words, "three hundred", "триста", "Numbers", "A1", "Three hundred is 300.");
            Add(words, "four hundred", "четыреста", "Numbers", "A1", "Four hundred is 400.");
            Add(words, "five hundred", "пятьсот", "Numbers", "A1", "Five hundred is 500.");
            Add(words, "six hundred", "шестьсот", "Numbers", "A1", "Six hundred is 600.");
            Add(words, "seven hundred", "семьсот", "Numbers", "A1", "Seven hundred is 700.");
            Add(words, "eight hundred", "восемьсот", "Numbers", "A1", "Eight hundred is 800.");
            Add(words, "nine hundred", "девятьсот", "Numbers", "A1", "Nine hundred is 900.");

            Add(words, "one thousand", "одна тысяча", "Numbers", "A1", "One thousand is 1000.");
        }

        // =====================================================
        // ANIMALS
        // =====================================================

        private static void AddAnimals(List<WordItem> words)
        {
            Add(words, "cat", "кошка", "Animals", "A0", "I have a cat.");
            Add(words, "dog", "собака", "Animals", "A0", "The dog is friendly.");
            Add(words, "bird", "птица", "Animals", "A0", "A bird can fly.");
            Add(words, "fish", "рыба", "Animals", "A0", "A fish can swim.");
            Add(words, "horse", "лошадь", "Animals", "A0", "The horse is big.");
            Add(words, "rabbit", "кролик", "Animals", "A0", "The rabbit is white.");
            Add(words, "mouse", "мышь", "Animals", "A0", "The mouse is small.");
            Add(words, "lion", "лев", "Animals", "A0", "A lion is strong.");
            Add(words, "tiger", "тигр", "Animals", "A1", "The tiger is fast.");
            Add(words, "bear", "медведь", "Animals", "A1", "A bear is big.");
            Add(words, "fox", "лиса", "Animals", "A1", "The fox is orange.");
            Add(words, "wolf", "волк", "Animals", "A1", "A wolf lives in the forest.");
            Add(words, "cow", "корова", "Animals", "A0", "A cow gives milk.");
            Add(words, "pig", "свинья", "Animals", "A0", "The pig is pink.");
            Add(words, "sheep", "овца", "Animals", "A1", "The sheep is white.");
            Add(words, "duck", "утка", "Animals", "A0", "A duck can swim.");
            Add(words, "chicken", "курица", "Animals", "A0", "The chicken is small.");
            Add(words, "frog", "лягушка", "Animals", "A1", "A frog is green.");
            Add(words, "monkey", "обезьяна", "Animals", "A1", "A monkey can climb.");
            Add(words, "elephant", "слон", "Animals", "A1", "An elephant is very big.");
        }

        // =====================================================
        // FOOD
        // =====================================================

        private static void AddFood(List<WordItem> words)
        {
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
            Add(words, "tea", "чай", "Food", "A0", "I drink tea.");
            Add(words, "coffee", "кофе", "Food", "A1", "My mother drinks coffee.");
            Add(words, "meat", "мясо", "Food", "A1", "He eats meat.");
            Add(words, "potato", "картофель", "Food", "A1", "A potato is brown.");
            Add(words, "tomato", "помидор", "Food", "A1", "A tomato is red.");
            Add(words, "carrot", "морковь", "Food", "A1", "A carrot is orange.");
            Add(words, "cake", "торт", "Food", "A0", "The cake is sweet.");
            Add(words, "ice cream", "мороженое", "Food", "A0", "I like ice cream.");
            Add(words, "sandwich", "бутерброд", "Food", "A1", "I eat a sandwich.");
            Add(words, "pizza", "пицца", "Food", "A0", "Pizza is tasty.");
            Add(words, "salad", "салат", "Food", "A1", "I eat salad for lunch.");
            Add(words, "fruit", "фрукты", "Food", "A0", "Fruit is good for you.");
            Add(words, "vegetable", "овощ", "Food", "A1", "A carrot is a vegetable.");
            Add(words, "sugar", "сахар", "Food", "A1", "Tea with sugar is sweet.");
            Add(words, "salt", "соль", "Food", "A1", "Soup needs salt.");
        }

        // =====================================================
        // FAMILY
        // =====================================================

        private static void AddFamily(List<WordItem> words)
        {
            Add(words, "mother", "мама", "Family", "A0", "My mother is kind.");
            Add(words, "father", "папа", "Family", "A0", "My father is at home.");
            Add(words, "sister", "сестра", "Family", "A0", "My sister is young.");
            Add(words, "brother", "брат", "Family", "A0", "My brother is tall.");
            Add(words, "family", "семья", "Family", "A0", "I love my family.");
            Add(words, "baby", "малыш", "Family", "A0", "The baby is sleeping.");
            Add(words, "grandmother", "бабушка", "Family", "A1", "My grandmother is nice.");
            Add(words, "grandfather", "дедушка", "Family", "A1", "My grandfather is old.");
            Add(words, "parent", "родитель", "Family", "A1", "A mother is a parent.");
            Add(words, "parents", "родители", "Family", "A1", "My parents are at home.");
            Add(words, "son", "сын", "Family", "A1", "He is my son.");
            Add(words, "daughter", "дочь", "Family", "A1", "She is my daughter.");
            Add(words, "uncle", "дядя", "Family", "A1", "My uncle is funny.");
            Add(words, "aunt", "тётя", "Family", "A1", "My aunt is kind.");
            Add(words, "cousin", "двоюродный брат / сестра", "Family", "A1", "My cousin is ten.");
            Add(words, "wife", "жена", "Family", "A1", "His wife is a teacher.");
            Add(words, "husband", "муж", "Family", "A1", "Her husband is a doctor.");
            Add(words, "child", "ребёнок", "Family", "A0", "The child is happy.");
            Add(words, "children", "дети", "Family", "A1", "The children are playing.");
        }

        // =====================================================
        // SCHOOL
        // =====================================================

        private static void AddSchool(List<WordItem> words)
        {
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
            Add(words, "bag", "сумка / портфель", "School", "A0", "My bag is blue.");
            Add(words, "ruler", "линейка", "School", "A0", "I have a ruler.");
            Add(words, "eraser", "ластик", "School", "A0", "This eraser is small.");
            Add(words, "notebook", "тетрадь", "School", "A1", "I write in my notebook.");
            Add(words, "homework", "домашнее задание", "School", "A1", "I do my homework.");
            Add(words, "test", "тест", "School", "A1", "The test is easy.");
            Add(words, "question", "вопрос", "School", "A1", "I have a question.");
            Add(words, "answer", "ответ", "School", "A1", "This answer is correct.");
            Add(words, "word", "слово", "School", "A0", "Write the word.");
            Add(words, "sentence", "предложение", "School", "A1", "This sentence is short.");
            Add(words, "page", "страница", "School", "A0", "Open the book on page ten.");
            Add(words, "map", "карта", "School", "A1", "There is a map on the wall.");
            Add(words, "computer", "компьютер", "School", "A1", "We use a computer at school.");
            Add(words, "break", "перемена", "School", "A1", "We play during the break.");
        }

        // =====================================================
        // HOME
        // =====================================================

        private static void AddHome(List<WordItem> words)
        {
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
            Add(words, "sofa", "диван", "Home", "A1", "The sofa is comfortable.");
            Add(words, "lamp", "лампа", "Home", "A0", "The lamp is on the table.");
            Add(words, "floor", "пол", "Home", "A1", "The floor is clean.");
            Add(words, "wall", "стена", "Home", "A1", "The wall is white.");
            Add(words, "mirror", "зеркало", "Home", "A1", "The mirror is in the bathroom.");
            Add(words, "fridge", "холодильник", "Home", "A1", "Milk is in the fridge.");
            Add(words, "cup", "чашка", "Home", "A0", "This cup is blue.");
            Add(words, "plate", "тарелка", "Home", "A0", "The plate is white.");
            Add(words, "spoon", "ложка", "Home", "A0", "I have a spoon.");
            Add(words, "clock", "часы", "Home", "A1", "The clock is on the wall.");
            Add(words, "blanket", "одеяло", "Home", "A1", "The blanket is warm.");
            Add(words, "pillow", "подушка", "Home", "A1", "The pillow is soft.");
        }

        // =====================================================
        // CLOTHES
        // =====================================================

        private static void AddClothes(List<WordItem> words)
        {
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
            Add(words, "jeans", "джинсы", "Clothes", "A1", "My jeans are blue.");
            Add(words, "boots", "ботинки", "Clothes", "A1", "His boots are black.");
            Add(words, "gloves", "перчатки", "Clothes", "A1", "I wear gloves in winter.");
            Add(words, "scarf", "шарф", "Clothes", "A1", "This scarf is warm.");
            Add(words, "cap", "кепка", "Clothes", "A0", "He has a cap.");
            Add(words, "sweater", "свитер", "Clothes", "A1", "My sweater is warm.");
            Add(words, "uniform", "форма", "Clothes", "A1", "Students wear a uniform.");
            Add(words, "shorts", "шорты", "Clothes", "A1", "I wear shorts in summer.");
            Add(words, "belt", "ремень", "Clothes", "A1", "His belt is black.");
            Add(words, "pocket", "карман", "Clothes", "A1", "The key is in my pocket.");
        }

        // =====================================================
        // BODY
        // =====================================================

        private static void AddBody(List<WordItem> words)
        {
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
            Add(words, "hair", "волосы", "Body", "A0", "Her hair is long.");
            Add(words, "tooth", "зуб", "Body", "A1", "My tooth hurts.");
            Add(words, "teeth", "зубы", "Body", "A1", "Brush your teeth.");
            Add(words, "finger", "палец на руке", "Body", "A1", "I have five fingers.");
            Add(words, "toe", "палец на ноге", "Body", "A1", "My toe hurts.");
            Add(words, "back", "спина", "Body", "A1", "My back hurts.");
            Add(words, "stomach", "живот", "Body", "A1", "My stomach hurts.");
            Add(words, "shoulder", "плечо", "Body", "A1", "His shoulder hurts.");
            Add(words, "knee", "колено", "Body", "A1", "My knee hurts.");
            Add(words, "neck", "шея", "Body", "A1", "My neck hurts.");
        }

        // =====================================================
        // WEATHER
        // =====================================================

        private static void AddWeather(List<WordItem> words)
        {
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
            Add(words, "sunny", "солнечно", "Weather", "A1", "It is sunny today.");
            Add(words, "rainy", "дождливо", "Weather", "A1", "It is rainy today.");
            Add(words, "snowy", "снежно", "Weather", "A1", "It is snowy in winter.");
            Add(words, "windy", "ветрено", "Weather", "A1", "It is windy outside.");
            Add(words, "cloudy", "облачно", "Weather", "A1", "It is cloudy today.");
            Add(words, "fog", "туман", "Weather", "A1", "There is fog in the morning.");
            Add(words, "temperature", "температура", "Weather", "A1", "The temperature is low.");
            Add(words, "season", "время года", "Weather", "A1", "Winter is a cold season.");
        }

        // =====================================================
        // TRANSPORT
        // =====================================================

        private static void AddTransport(List<WordItem> words)
        {
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
            Add(words, "metro", "метро", "Transport", "A1", "I go by metro.");
            Add(words, "tram", "трамвай", "Transport", "A1", "The tram is slow.");
            Add(words, "driver", "водитель", "Transport", "A1", "The driver is in the car.");
            Add(words, "ticket", "билет", "Transport", "A1", "I have a ticket.");
            Add(words, "road", "дорога", "Transport", "A1", "The road is long.");
            Add(words, "stop", "остановка", "Transport", "A1", "The bus stop is near.");
            Add(words, "travel", "путешествовать", "Transport", "A1", "I like to travel.");
            Add(words, "walk", "ходить пешком", "Transport", "A0", "I walk to school.");
        }

        // =====================================================
        // PLACES
        // =====================================================

        private static void AddPlaces(List<WordItem> words)
        {
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
            Add(words, "museum", "музей", "Places", "A1", "We go to the museum.");
            Add(words, "hotel", "отель", "Places", "A1", "The hotel is big.");
            Add(words, "restaurant", "ресторан", "Places", "A1", "We eat in a restaurant.");
            Add(words, "post office", "почта", "Places", "A1", "The post office is near.");
            Add(words, "supermarket", "супермаркет", "Places", "A1", "I buy food at the supermarket.");
            Add(words, "playground", "детская площадка", "Places", "A1", "Children play on the playground.");
            Add(words, "zoo", "зоопарк", "Places", "A0", "We see animals at the zoo.");
            Add(words, "beach", "пляж", "Places", "A1", "The beach is beautiful.");
        }

        // =====================================================
        // DAILY ROUTINE
        // =====================================================

        private static void AddDailyRoutine(List<WordItem> words)
        {
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
            Add(words, "brush", "чистить щёткой", "DailyRoutine", "A1", "I brush my teeth.");
            Add(words, "dress", "одеваться", "DailyRoutine", "A1", "I dress in the morning.");
            Add(words, "cook", "готовить", "DailyRoutine", "A1", "I cook dinner.");
            Add(words, "clean", "убирать", "DailyRoutine", "A1", "I clean my room.");
            Add(words, "rest", "отдыхать", "DailyRoutine", "A1", "I rest after school.");
            Add(words, "walk", "гулять", "DailyRoutine", "A1", "I walk in the park.");
            Add(words, "start", "начинать", "DailyRoutine", "A1", "The lesson starts at nine.");
            Add(words, "finish", "заканчивать", "DailyRoutine", "A1", "I finish my homework.");
        }

        // =====================================================
        // ACTIONS
        // =====================================================

        private static void AddActions(List<WordItem> words)
        {
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
            Add(words, "buy", "покупать", "Actions", "A1", "I buy bread.");
            Add(words, "sell", "продавать", "Actions", "A1", "They sell fruit.");
            Add(words, "give", "давать", "Actions", "A1", "Give me a pen, please.");
            Add(words, "take", "брать", "Actions", "A1", "Take your book.");
            Add(words, "make", "делать / создавать", "Actions", "A1", "I make a cake.");
            Add(words, "draw", "рисовать", "Actions", "A0", "I draw a cat.");
            Add(words, "sing", "петь", "Actions", "A0", "She can sing.");
            Add(words, "dance", "танцевать", "Actions", "A0", "They dance every day.");
            Add(words, "find", "находить", "Actions", "A1", "I find my pen.");
            Add(words, "try", "пробовать", "Actions", "A1", "Try again.");
        }

        // =====================================================
        // FEELINGS
        // =====================================================

        private static void AddFeelings(List<WordItem> words)
        {
            Add(words, "happy", "счастливый", "Feelings", "A0", "I am happy.");
            Add(words, "sad", "грустный", "Feelings", "A0", "He is sad.");
            Add(words, "tired", "уставший", "Feelings", "A0", "She is tired.");
            Add(words, "angry", "злой", "Feelings", "A1", "The boy is angry.");
            Add(words, "scared", "испуганный", "Feelings", "A1", "I am scared.");
            Add(words, "hungry", "голодный", "Feelings", "A0", "I am hungry.");
            Add(words, "thirsty", "хочет пить / испытывает жажду", "Feelings", "A0", "I am thirsty.");
            Add(words, "excited", "взволнованный", "Feelings", "A1", "We are excited.");
            Add(words, "bored", "скучающий", "Feelings", "A1", "I am bored.");
            Add(words, "worried", "обеспокоенный", "Feelings", "A1", "She is worried.");
            Add(words, "calm", "спокойный", "Feelings", "A1", "He is calm.");
            Add(words, "proud", "гордый", "Feelings", "A1", "I am proud.");
            Add(words, "shy", "застенчивый", "Feelings", "A1", "The girl is shy.");
            Add(words, "kind", "добрый", "Feelings", "A0", "My teacher is kind.");
            Add(words, "friendly", "дружелюбный", "Feelings", "A1", "The dog is friendly.");
            Add(words, "surprised", "удивлённый", "Feelings", "A1", "He is surprised.");
        }

        // =====================================================
        // ADJECTIVES
        // =====================================================

        private static void AddAdjectives(List<WordItem> words)
        {
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
            Add(words, "clean", "чистый", "Adjectives", "A0", "My room is clean.");
            Add(words, "dirty", "грязный", "Adjectives", "A1", "The shoes are dirty.");
            Add(words, "easy", "лёгкий", "Adjectives", "A0", "This lesson is easy.");
            Add(words, "difficult", "сложный", "Adjectives", "A1", "This word is difficult.");
            Add(words, "short", "короткий", "Adjectives", "A0", "The sentence is short.");
            Add(words, "long", "длинный", "Adjectives", "A0", "The road is long.");
            Add(words, "beautiful", "красивый", "Adjectives", "A1", "The flower is beautiful.");
            Add(words, "interesting", "интересный", "Adjectives", "A1", "This game is interesting.");
            Add(words, "strong", "сильный", "Adjectives", "A1", "The lion is strong.");
            Add(words, "weak", "слабый", "Adjectives", "A1", "The baby is weak.");
            Add(words, "soft", "мягкий", "Adjectives", "A1", "The pillow is soft.");
            Add(words, "hard", "твёрдый / сложный", "Adjectives", "A1", "The test is hard.");
        }

        // =====================================================
        // SPORTS
        // =====================================================

        private static void AddSports(List<WordItem> words)
        {
            Add(words, "football", "футбол", "Sports", "A0", "I play football.");
            Add(words, "tennis", "теннис", "Sports", "A1", "She plays tennis.");
            Add(words, "basketball", "баскетбол", "Sports", "A1", "They play basketball.");
            Add(words, "volleyball", "волейбол", "Sports", "A1", "We play volleyball.");
            Add(words, "swimming", "плавание", "Sports", "A1", "Swimming is good.");
            Add(words, "running", "бег", "Sports", "A1", "Running is fun.");
            Add(words, "skating", "катание на коньках", "Sports", "A1", "I like skating.");
            Add(words, "skiing", "катание на лыжах", "Sports", "A1", "Skiing is popular in winter.");
            Add(words, "ball", "мяч", "Sports", "A0", "The ball is red.");
            Add(words, "team", "команда", "Sports", "A1", "Our team is strong.");
            Add(words, "player", "игрок", "Sports", "A1", "He is a good player.");
            Add(words, "game", "игра", "Sports", "A0", "This game is fun.");
        }

        // =====================================================
        // JOBS
        // =====================================================

        private static void AddJobs(List<WordItem> words)
        {
            Add(words, "doctor", "врач", "Jobs", "A1", "A doctor helps people.");
            Add(words, "nurse", "медсестра", "Jobs", "A1", "A nurse works in a hospital.");
            Add(words, "teacher", "учитель", "Jobs", "A1", "A teacher works at school.");
            Add(words, "driver", "водитель", "Jobs", "A1", "A driver drives a car.");
            Add(words, "cook", "повар", "Jobs", "A1", "A cook makes food.");
            Add(words, "farmer", "фермер", "Jobs", "A1", "A farmer works on a farm.");
            Add(words, "police officer", "полицейский", "Jobs", "A1", "A police officer helps people.");
            Add(words, "shop assistant", "продавец", "Jobs", "A1", "A shop assistant works in a shop.");
            Add(words, "waiter", "официант", "Jobs", "A1", "A waiter works in a restaurant.");
            Add(words, "artist", "художник", "Jobs", "A1", "An artist draws pictures.");
            Add(words, "singer", "певец", "Jobs", "A1", "A singer sings songs.");
            Add(words, "student", "ученик / студент", "Jobs", "A0", "A student studies.");

            Add(words, "builder", "строитель", "Jobs", "A1", "A builder builds houses.");
            Add(words, "firefighter", "пожарный", "Jobs", "A1", "A firefighter helps in a fire.");
            Add(words, "dentist", "стоматолог", "Jobs", "A1", "A dentist helps with teeth.");
            Add(words, "engineer", "инженер", "Jobs", "A1", "An engineer makes machines.");
            Add(words, "programmer", "программист", "Jobs", "A1", "A programmer writes code.");
            Add(words, "secretary", "секретарь", "Jobs", "A1", "A secretary works in an office.");
            Add(words, "manager", "менеджер", "Jobs", "A1", "A manager works with people.");
            Add(words, "cleaner", "уборщик", "Jobs", "A1", "A cleaner cleans rooms.");
            Add(words, "mechanic", "механик", "Jobs", "A1", "A mechanic repairs cars.");
            Add(words, "pilot", "пилот", "Jobs", "A1", "A pilot flies a plane.");
            Add(words, "actor", "актёр", "Jobs", "A1", "An actor works in films.");
            Add(words, "writer", "писатель", "Jobs", "A1", "A writer writes books.");
            Add(words, "journalist", "журналист", "Jobs", "A1", "A journalist writes news.");
            Add(words, "hairdresser", "парикмахер", "Jobs", "A1", "A hairdresser cuts hair.");
            Add(words, "businessman", "бизнесмен", "Jobs", "A1", "A businessman works in business.");
            Add(words, "office worker", "офисный работник", "Jobs", "A1", "An office worker works in an office.");
            Add(words, "scientist", "учёный", "Jobs", "A1", "A scientist studies science.");
            Add(words, "musician", "музыкант", "Jobs", "A1", "A musician plays music.");
            Add(words, "photographer", "фотограф", "Jobs", "A1", "A photographer takes photos.");
            Add(words, "lawyer", "юрист / адвокат", "Jobs", "A1", "A lawyer works with laws.");
            Add(words, "postman", "почтальон", "Jobs", "A1", "A postman brings letters.");
            Add(words, "soldier", "солдат", "Jobs", "A1", "A soldier works in the army.");
            Add(words, "vet", "ветеринар", "Jobs", "A1", "A vet helps animals.");
        }

        // =====================================================
        // NATURE
        // =====================================================

        private static void AddNature(List<WordItem> words)
        {
            Add(words, "tree", "дерево", "Nature", "A0", "The tree is tall.");
            Add(words, "flower", "цветок", "Nature", "A0", "The flower is beautiful.");
            Add(words, "grass", "трава", "Nature", "A0", "The grass is green.");
            Add(words, "river", "река", "Nature", "A1", "The river is long.");
            Add(words, "lake", "озеро", "Nature", "A1", "The lake is blue.");
            Add(words, "sea", "море", "Nature", "A1", "The sea is big.");
            Add(words, "mountain", "гора", "Nature", "A1", "The mountain is high.");
            Add(words, "forest", "лес", "Nature", "A1", "Animals live in the forest.");
            Add(words, "field", "поле", "Nature", "A1", "The field is green.");
            Add(words, "sky", "небо", "Nature", "A0", "The sky is blue.");
            Add(words, "star", "звезда", "Nature", "A0", "I see a star.");
            Add(words, "moon", "луна", "Nature", "A0", "The moon is bright.");

            Add(words, "leaf", "лист", "Nature", "A1", "A leaf is green.");
            Add(words, "leaves", "листья", "Nature", "A1", "Leaves fall in autumn.");
            Add(words, "branch", "ветка", "Nature", "A1", "A branch is part of a tree.");
            Add(words, "root", "корень", "Nature", "A1", "A root is under the ground.");
            Add(words, "plant", "растение", "Nature", "A1", "A plant needs water.");
            Add(words, "stone", "камень", "Nature", "A1", "The stone is hard.");
            Add(words, "rock", "скала / камень", "Nature", "A1", "The rock is big.");
            Add(words, "hill", "холм", "Nature", "A1", "The hill is small.");
            Add(words, "island", "остров", "Nature", "A1", "The island is in the sea.");
            Add(words, "desert", "пустыня", "Nature", "A1", "A desert is hot and dry.");
            Add(words, "ocean", "океан", "Nature", "A1", "The ocean is very big.");
            Add(words, "pond", "пруд", "Nature", "A1", "Ducks swim in the pond.");
            Add(words, "sand", "песок", "Nature", "A1", "The sand is warm.");
            Add(words, "ground", "земля", "Nature", "A1", "The ball is on the ground.");
            Add(words, "earth", "земля / планета Земля", "Nature", "A1", "The Earth is our planet.");
            Add(words, "air", "воздух", "Nature", "A1", "We need air.");
            Add(words, "fire", "огонь", "Nature", "A1", "Fire is hot.");
            Add(words, "ice", "лёд", "Nature", "A1", "Ice is cold.");
            Add(words, "rainbow", "радуга", "Nature", "A1", "A rainbow has many colours.");
            Add(words, "valley", "долина", "Nature", "A1", "The valley is green.");
            Add(words, "waterfall", "водопад", "Nature", "A1", "The waterfall is beautiful.");
            Add(words, "cave", "пещера", "Nature", "A1", "The cave is dark.");
            Add(words, "planet", "планета", "Nature", "A1", "Earth is a planet.");
        }

        // =====================================================
        // SHOPPING
        // =====================================================

        private static void AddShopping(List<WordItem> words)
        {
            Add(words, "money", "деньги", "Shopping", "A1", "I have money.");
            Add(words, "price", "цена", "Shopping", "A1", "The price is low.");
            Add(words, "shop", "магазин", "Shopping", "A0", "I go to the shop.");
            Add(words, "market", "рынок", "Shopping", "A1", "I buy fruit at the market.");
            Add(words, "bag", "сумка", "Shopping", "A0", "Put the apples in the bag.");
            Add(words, "list", "список", "Shopping", "A1", "I have a shopping list.");
            Add(words, "cash", "наличные", "Shopping", "A1", "I pay with cash.");
            Add(words, "card", "карта", "Shopping", "A1", "I pay by card.");
            Add(words, "cheap", "дешёвый", "Shopping", "A1", "This pen is cheap.");
            Add(words, "expensive", "дорогой", "Shopping", "A1", "This phone is expensive.");
            Add(words, "buy", "покупать", "Shopping", "A1", "I buy bread.");
            Add(words, "pay", "платить", "Shopping", "A1", "I pay for the book.");
        }

        // =====================================================
        // DIALOGUE PHRASES
        // =====================================================

        private static void AddDialoguePhrases(List<WordItem> words)
        {
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
            Add(words, "how are you", "как дела", "DialoguePhrases", "A0", "How are you?");
            Add(words, "what is your name", "как тебя зовут", "DialoguePhrases", "A0", "What is your name?");
            Add(words, "where are you from", "откуда ты", "DialoguePhrases", "A1", "Where are you from?");
            Add(words, "I do not know", "я не знаю", "DialoguePhrases", "A1", "I do not know.");
            Add(words, "excuse me", "извините", "DialoguePhrases", "A1", "Excuse me, where is the station?");
            Add(words, "you are welcome", "пожалуйста / не за что", "DialoguePhrases", "A1", "You are welcome.");
        }

        // =====================================================
        // COMMON METHODS
        // =====================================================

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
                "Sports — Спорт",
                "Jobs — Профессии",
                "Nature — Природа",
                "Shopping — Покупки",
                "DialoguePhrases — Диалоговые фразы"
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