using System;
using System.IO;
using System.Xml.Serialization;

namespace EnglishTypingGame
{
    public static class SettingsService
    {
        private static readonly string FolderPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EnglishTypingGame");

        private static readonly string FilePath =
            Path.Combine(FolderPath, "settings.xml");

        public static SettingsData Load()
        {
            try
            {
                if (!File.Exists(FilePath))
                    return new SettingsData();

                XmlSerializer serializer = new XmlSerializer(typeof(SettingsData));

                using (FileStream stream = new FileStream(FilePath, FileMode.Open))
                {
                    SettingsData data = serializer.Deserialize(stream) as SettingsData;
                    return data ?? new SettingsData();
                }
            }
            catch
            {
                return new SettingsData();
            }
        }

        public static void Save(SettingsData data)
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            XmlSerializer serializer = new XmlSerializer(typeof(SettingsData));

            using (FileStream stream = new FileStream(FilePath, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
        }
    }
}