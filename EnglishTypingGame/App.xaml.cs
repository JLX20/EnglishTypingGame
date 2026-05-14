using System.Windows;

namespace EnglishTypingGame
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SettingsData settings = SettingsService.Load();
            ThemeService.ApplyTheme(settings.ThemeName, settings.BackgroundName);
        }
    }
}