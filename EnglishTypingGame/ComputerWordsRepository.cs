using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishTypingGame
{
    public static class ComputerWordsRepository
    {
        private static readonly List<WordItem> Words = BuildWords();

        private static List<WordItem> BuildWords()
        {
            List<WordItem> words = new List<WordItem>();

            AddComputerBasics(words);
            AddInternetAndOnline(words);
            AddFilesAndActions(words);
            AddProgrammingAndTech(words);

            return words;
        }

        // =====================================================
        // ComputerBasics — Компьютер и устройства
        // Computer Basics + Computer Parts
        // =====================================================

        private static void AddComputerBasics(List<WordItem> words)
        {
            Add(words, "computer", "компьютер", "ComputerBasics", "A0", "I use a computer every day.");
            Add(words, "laptop", "ноутбук", "ComputerBasics", "A0", "My laptop is on the table.");
            Add(words, "screen", "экран", "ComputerBasics", "A0", "The screen is bright.");
            Add(words, "keyboard", "клавиатура", "ComputerBasics", "A0", "I type on the keyboard.");
            Add(words, "mouse", "мышь", "ComputerBasics", "A0", "Click the mouse.");
            Add(words, "button", "кнопка", "ComputerBasics", "A0", "Press the button.");
            Add(words, "desktop", "рабочий стол", "ComputerBasics", "A1", "The file is on the desktop.");
            Add(words, "window", "окно", "ComputerBasics", "A1", "Open a new window.");
            Add(words, "program", "программа", "ComputerBasics", "A1", "This program is useful.");
            Add(words, "app", "приложение", "ComputerBasics", "A1", "Open the app.");
            Add(words, "icon", "значок", "ComputerBasics", "A1", "Click the icon.");
            Add(words, "menu", "меню", "ComputerBasics", "A1", "Open the menu.");
            Add(words, "settings", "настройки", "ComputerBasics", "A1", "Change the settings.");
            Add(words, "password", "пароль", "ComputerBasics", "A1", "Enter your password.");
            Add(words, "monitor", "монитор", "ComputerBasics", "A1", "The monitor is large.");
            Add(words, "speaker", "колонка", "ComputerBasics", "A1", "The speaker is loud.");
            Add(words, "headphones", "наушники", "ComputerBasics", "A1", "I use headphones.");
            Add(words, "microphone", "микрофон", "ComputerBasics", "A1", "Use the microphone to speak.");
            Add(words, "printer", "принтер", "ComputerBasics", "A1", "The printer prints documents.");
            Add(words, "scanner", "сканер", "ComputerBasics", "A1", "The scanner scans pictures.");
            Add(words, "camera", "камера", "ComputerBasics", "A1", "The camera takes photos.");
            Add(words, "webcam", "веб-камера", "ComputerBasics", "A1", "Turn on the webcam.");
            Add(words, "charger", "зарядное устройство", "ComputerBasics", "A1", "Where is my charger?");
            Add(words, "cable", "кабель", "ComputerBasics", "A1", "Connect the cable.");
            Add(words, "USB drive", "флешка", "ComputerBasics", "A1", "Save the file on a USB drive.");
            Add(words, "memory", "память", "ComputerBasics", "A1", "This computer has good memory.");
            Add(words, "processor", "процессор", "ComputerBasics", "A2", "The processor is fast.");
            Add(words, "hard drive", "жёсткий диск", "ComputerBasics", "A2", "The hard drive is full.");
            Add(words, "battery", "батарея", "ComputerBasics", "A1", "The battery is low.");
            Add(words, "power button", "кнопка питания", "ComputerBasics", "A1", "Press the power button.");
            Add(words, "tablet", "планшет", "ComputerBasics", "A1", "She uses a tablet.");
            Add(words, "device", "устройство", "ComputerBasics", "A1", "This device is new.");
        }

        // =====================================================
        // InternetAndOnline — Интернет и онлайн
        // =====================================================

        private static void AddInternetAndOnline(List<WordItem> words)
        {
            Add(words, "internet", "интернет", "InternetAndOnline", "A0", "I use the internet.");
            Add(words, "website", "сайт", "InternetAndOnline", "A1", "This website is useful.");
            Add(words, "browser", "браузер", "InternetAndOnline", "A1", "Open the browser.");
            Add(words, "link", "ссылка", "InternetAndOnline", "A1", "Click the link.");
            Add(words, "page", "страница", "InternetAndOnline", "A0", "Open this page.");
            Add(words, "search", "поиск / искать", "InternetAndOnline", "A1", "Search for a word.");
            Add(words, "email", "электронная почта", "InternetAndOnline", "A1", "Send an email.");
            Add(words, "message", "сообщение", "InternetAndOnline", "A1", "I have a new message.");
            Add(words, "chat", "чат", "InternetAndOnline", "A1", "We talk in a chat.");
            Add(words, "online", "онлайн", "InternetAndOnline", "A0", "I am online.");
            Add(words, "offline", "офлайн", "InternetAndOnline", "A1", "The computer is offline.");
            Add(words, "download", "скачивать", "InternetAndOnline", "A1", "Download the file.");
            Add(words, "upload", "загружать", "InternetAndOnline", "A1", "Upload a photo.");
            Add(words, "account", "аккаунт", "InternetAndOnline", "A1", "Create an account.");
            Add(words, "login", "вход / войти", "InternetAndOnline", "A1", "Login to your account.");
            Add(words, "logout", "выход / выйти", "InternetAndOnline", "A1", "Logout after work.");
            Add(words, "username", "имя пользователя", "InternetAndOnline", "A1", "Enter your username.");
            Add(words, "profile", "профиль", "InternetAndOnline", "A1", "Open your profile.");
            Add(words, "social media", "социальные сети", "InternetAndOnline", "A2", "Many people use social media.");
            Add(words, "video call", "видеозвонок", "InternetAndOnline", "A1", "We have a video call.");
            Add(words, "network", "сеть", "InternetAndOnline", "A1", "The network is slow.");
            Add(words, "Wi-Fi", "вай-фай", "InternetAndOnline", "A1", "Connect to Wi-Fi.");
            Add(words, "connection", "соединение", "InternetAndOnline", "A2", "The connection is good.");
            Add(words, "signal", "сигнал", "InternetAndOnline", "A1", "The signal is weak.");
            Add(words, "address", "адрес", "InternetAndOnline", "A1", "Type the website address.");
            Add(words, "tab", "вкладка", "InternetAndOnline", "A1", "Open a new tab.");
            Add(words, "bookmark", "закладка", "InternetAndOnline", "A2", "Save this page as a bookmark.");
            Add(words, "notification", "уведомление", "InternetAndOnline", "A2", "I got a notification.");
            Add(words, "subscribe", "подписаться", "InternetAndOnline", "A2", "Subscribe to the channel.");
            Add(words, "post", "публикация / пост", "InternetAndOnline", "A1", "Write a post.");
            Add(words, "comment", "комментарий", "InternetAndOnline", "A1", "Write a comment.");
            Add(words, "share", "делиться", "InternetAndOnline", "A1", "Share the link.");
        }

        // =====================================================
        // FilesAndActions — Файлы и действия
        // Files and Documents + Actions on Computer
        // =====================================================

        private static void AddFilesAndActions(List<WordItem> words)
        {
            Add(words, "file", "файл", "FilesAndActions", "A0", "Open the file.");
            Add(words, "folder", "папка", "FilesAndActions", "A0", "Create a new folder.");
            Add(words, "document", "документ", "FilesAndActions", "A1", "Save the document.");
            Add(words, "picture", "картинка", "FilesAndActions", "A0", "Open the picture.");
            Add(words, "photo", "фото", "FilesAndActions", "A0", "Upload a photo.");
            Add(words, "video", "видео", "FilesAndActions", "A0", "Watch the video.");
            Add(words, "music", "музыка", "FilesAndActions", "A0", "Play the music.");
            Add(words, "text", "текст", "FilesAndActions", "A0", "Write the text.");
            Add(words, "copy", "копировать", "FilesAndActions", "A1", "Copy the text.");
            Add(words, "paste", "вставить", "FilesAndActions", "A1", "Paste the text here.");
            Add(words, "cut", "вырезать", "FilesAndActions", "A1", "Cut this word.");
            Add(words, "save", "сохранить", "FilesAndActions", "A1", "Save your work.");
            Add(words, "open", "открыть", "FilesAndActions", "A0", "Open the folder.");
            Add(words, "close", "закрыть", "FilesAndActions", "A0", "Close the window.");
            Add(words, "delete", "удалить", "FilesAndActions", "A1", "Delete the old file.");
            Add(words, "rename", "переименовать", "FilesAndActions", "A1", "Rename the folder.");
            Add(words, "click", "нажимать", "FilesAndActions", "A0", "Click the button.");
            Add(words, "type", "печатать", "FilesAndActions", "A0", "Type your answer.");
            Add(words, "write", "писать", "FilesAndActions", "A0", "Write a sentence.");
            Add(words, "read", "читать", "FilesAndActions", "A0", "Read the text.");
            Add(words, "move", "перемещать", "FilesAndActions", "A1", "Move the file.");
            Add(words, "drag", "перетаскивать", "FilesAndActions", "A1", "Drag the icon.");
            Add(words, "drop", "отпускать / бросать", "FilesAndActions", "A1", "Drop the file here.");
            Add(words, "install", "устанавливать", "FilesAndActions", "A1", "Install the program.");
            Add(words, "update", "обновлять", "FilesAndActions", "A1", "Update the app.");
            Add(words, "restart", "перезагружать", "FilesAndActions", "A1", "Restart the computer.");
            Add(words, "turn on", "включать", "FilesAndActions", "A1", "Turn on the computer.");
            Add(words, "turn off", "выключать", "FilesAndActions", "A1", "Turn off the laptop.");
            Add(words, "print", "печатать на принтере", "FilesAndActions", "A1", "Print the document.");
            Add(words, "select", "выбирать / выделять", "FilesAndActions", "A1", "Select the word.");
            Add(words, "save as", "сохранить как", "FilesAndActions", "A2", "Save the file as a document.");
            Add(words, "backup", "резервная копия", "FilesAndActions", "A2", "Make a backup of your files.");
        }

        // =====================================================
        // ProgrammingAndTech — Программирование и технический английский
        // Programming Basics + Tech English for Beginners
        // =====================================================

        private static void AddProgrammingAndTech(List<WordItem> words)
        {
            Add(words, "code", "код", "ProgrammingAndTech", "A1", "Write the code.");
            Add(words, "project", "проект", "ProgrammingAndTech", "A1", "Open the project.");
            Add(words, "class", "класс", "ProgrammingAndTech", "A2", "Create a new class.");
            Add(words, "method", "метод", "ProgrammingAndTech", "A2", "This method checks the answer.");
            Add(words, "variable", "переменная", "ProgrammingAndTech", "A2", "Create a variable.");
            Add(words, "value", "значение", "ProgrammingAndTech", "A1", "The value is ten.");
            Add(words, "string", "строка", "ProgrammingAndTech", "A2", "This string has text.");
            Add(words, "number", "число", "ProgrammingAndTech", "A0", "This number is five.");
            Add(words, "error", "ошибка", "ProgrammingAndTech", "A1", "Fix the error.");
            Add(words, "bug", "баг / ошибка", "ProgrammingAndTech", "A1", "There is a bug in the program.");
            Add(words, "fix", "исправлять", "ProgrammingAndTech", "A1", "Fix the problem.");
            Add(words, "run", "запускать", "ProgrammingAndTech", "A1", "Run the program.");
            Add(words, "build", "собирать проект", "ProgrammingAndTech", "A2", "Build the project.");
            Add(words, "debug", "отлаживать", "ProgrammingAndTech", "A2", "Debug the code.");
            Add(words, "test", "тестировать", "ProgrammingAndTech", "A1", "Test the program.");
            Add(words, "system", "система", "ProgrammingAndTech", "A1", "The system is ready.");
            Add(words, "data", "данные", "ProgrammingAndTech", "A1", "Save the data.");
            Add(words, "user", "пользователь", "ProgrammingAndTech", "A1", "The user enters a word.");
            Add(words, "admin", "администратор", "ProgrammingAndTech", "A2", "The admin can change settings.");
            Add(words, "tool", "инструмент", "ProgrammingAndTech", "A1", "This tool is useful.");
            Add(words, "version", "версия", "ProgrammingAndTech", "A1", "This is a new version.");
            Add(words, "connect", "подключить", "ProgrammingAndTech", "A1", "Connect the device.");
            Add(words, "disconnect", "отключить", "ProgrammingAndTech", "A1", "Disconnect the cable.");
            Add(words, "load", "загружать", "ProgrammingAndTech", "A1", "Load the data.");
            Add(words, "loading", "загрузка", "ProgrammingAndTech", "A1", "Loading takes time.");
            Add(words, "ready", "готово", "ProgrammingAndTech", "A0", "The program is ready.");
            Add(words, "done", "выполнено", "ProgrammingAndTech", "A0", "The task is done.");
            Add(words, "command", "команда", "ProgrammingAndTech", "A2", "Run the command.");
            Add(words, "console", "консоль", "ProgrammingAndTech", "A2", "Open the console.");
            Add(words, "database", "база данных", "ProgrammingAndTech", "A2", "The database stores data.");
            Add(words, "interface", "интерфейс", "ProgrammingAndTech", "A2", "The interface is simple.");
            Add(words, "application", "приложение", "ProgrammingAndTech", "A1", "This application helps students.");
        }

        private static void Add(List<WordItem> words, string english, string russian, string topic, string level, string example)
        {
            words.Add(new WordItem(english, russian, topic, level, example));
        }

        public static List<string> GetTopicsForUi()
        {
            return new List<string>
            {
                "ComputerBasics — Компьютер и устройства",
                "InternetAndOnline — Интернет и онлайн",
                "FilesAndActions — Файлы и действия",
                "ProgrammingAndTech — Программирование и технический английский"
            };
        }

        public static List<WordItem> GetAllWords()
        {
            return Words.ToList();
        }

        public static List<WordItem> GetWords(string topicUi)
        {
            string topic = NormalizeTopic(topicUi);

            if (string.IsNullOrWhiteSpace(topic))
                return Words.ToList();

            return Words
                .Where(w => w.Topic.Equals(topic, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public static bool IsComputerTopic(string topicUi)
        {
            string topic = NormalizeTopic(topicUi);

            return topic == "ComputerBasics" ||
                   topic == "InternetAndOnline" ||
                   topic == "FilesAndActions" ||
                   topic == "ProgrammingAndTech";
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

            string[] parts = topicUi.Split(new[] { " — " }, StringSplitOptions.None);
            return parts[0].Trim();
        }
    }
}