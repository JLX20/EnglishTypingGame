using System.Windows;
using System.Windows.Media;

namespace EnglishTypingGame
{
    public static class ThemeService
    {
        public static void ApplyTheme(string themeName)
        {
            ApplyTheme(themeName, "Sky");
        }

        public static void ApplyTheme(string themeName, string backgroundName)
        {
            if (string.IsNullOrWhiteSpace(themeName))
                themeName = "Blue";

            if (string.IsNullOrWhiteSpace(backgroundName))
                backgroundName = "Sky";

            ApplyColorTheme(themeName);
            ApplyBackground(backgroundName);
        }

        private static void ApplyColorTheme(string themeName)
        {
            switch (themeName)
            {
                case "Green":
                    ApplyPalette(
                        "#FFFFFF",
                        "#D1FAE5",
                        "#064E3B",
                        "#047857",

                        "#059669",
                        "#10B981",
                        "#047857",
                        "#0D9488",
                        "#DC2626",
                        "#64748B",

                        "#059669",
                        "#ECFDF5",
                        "#064E3B");
                    break;

                case "Purple":
                    ApplyPalette(
                        "#FFFFFF",
                        "#EDE9FE",
                        "#3B0764",
                        "#6D28D9",

                        "#7C3AED",
                        "#8B5CF6",
                        "#6D28D9",
                        "#A855F7",
                        "#DC2626",
                        "#64748B",

                        "#7C3AED",
                        "#F5F3FF",
                        "#4C1D95");
                    break;

                case "Orange":
                    ApplyPalette(
                        "#FFFFFF",
                        "#FFEDD5",
                        "#7C2D12",
                        "#EA580C",

                        "#F97316",
                        "#FB923C",
                        "#EA580C",
                        "#FDBA74",
                        "#DC2626",
                        "#64748B",

                        "#EA580C",
                        "#FFF7ED",
                        "#7C2D12");
                    break;

                case "Dark":
                    ApplyPalette(
                        "#1F2937",
                        "#374151",
                        "#F9FAFB",
                        "#D1D5DB",

                        "#60A5FA",
                        "#818CF8",
                        "#38BDF8",
                        "#A78BFA",
                        "#F87171",
                        "#64748B",

                        "#2563EB",
                        "#E0F2FE",
                        "#0F172A");
                    break;

                default:
                    ApplyPalette(
                        "#FFFFFF",
                        "#F1F5F9",
                        "#0F172A",
                        "#64748B",

                        "#2563EB",
                        "#7C3AED",
                        "#059669",
                        "#EA580C",
                        "#DC2626",
                        "#64748B",

                        "#2563EB",
                        "#EFF6FF",
                        "#1E3A8A");
                    break;
            }
        }

        private static void ApplyPalette(
            string cardBg,
            string softBg,
            string darkText,
            string secondaryText,

            string buttonMain,
            string buttonAccent,
            string buttonSuccess,
            string buttonWarning,
            string buttonDanger,
            string buttonNeutral,

            string wordRainWord,
            string wordRainCloud,
            string wordRainCloudText)
        {
            SetBrush("CardBgBrush", cardBg);
            SetBrush("SoftBgBrush", softBg);
            SetBrush("DarkTextBrush", darkText);
            SetBrush("SecondaryTextBrush", secondaryText);

            // MAIN BUTTON RESOURCES
            SetBrush("ButtonMainBrush", buttonMain);
            SetBrush("ButtonAccentBrush", buttonAccent);
            SetBrush("ButtonSuccessBrush", buttonSuccess);
            SetBrush("ButtonWarningBrush", buttonWarning);
            SetBrush("ButtonDangerBrush", buttonDanger);
            SetBrush("ButtonNeutralBrush", buttonNeutral);

            // OLD RESOURCE NAMES — чтобы старый код тоже менял цвета
            SetBrush("PrimaryBrush", buttonMain);
            SetBrush("PrimaryHoverBrush", buttonMain);
            SetBrush("AccentBrush", buttonAccent);

            SetBrush("GreenBrush", buttonSuccess);
            SetBrush("PurpleBrush", buttonAccent);
            SetBrush("RedBrush", buttonDanger);
            SetBrush("OrangeBrush", buttonWarning);
            SetBrush("TealBrush", buttonSuccess);
            SetBrush("SlateBrush", buttonNeutral);
            SetBrush("GrayButtonBrush", buttonNeutral);

            SetBrush("WordRainWordBrush", wordRainWord);
            SetBrush("WordRainCloudBrush", wordRainCloud);
            SetBrush("WordRainCloudTextBrush", wordRainCloudText);
        }

        private static void ApplyBackground(string backgroundName)
        {
            Brush brush;

            switch (backgroundName)
            {
                case "Plain":
                    brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F9FAFB"));
                    break;

                case "Mint":
                    brush = CreateGradient("#ECFDF5", "#D1FAE5");
                    break;

                case "Sunset":
                    brush = CreateGradient("#FFF7ED", "#FCE7F3");
                    break;

                case "Lavender":
                    brush = CreateGradient("#FAF5FF", "#EDE9FE");
                    break;

                case "Night":
                    brush = CreateGradient("#0F172A", "#1E1B4B");
                    break;

                default:
                    brush = CreateGradient("#E0F2FE", "#F8FAFC");
                    break;
            }

            Application.Current.Resources["WindowBgBrush"] = brush;

            if (backgroundName == "Night")
            {
                SetBrush("CardBgBrush", "#1F2937");
                SetBrush("SoftBgBrush", "#374151");
                SetBrush("DarkTextBrush", "#F9FAFB");
                SetBrush("SecondaryTextBrush", "#D1D5DB");
            }
        }

        private static LinearGradientBrush CreateGradient(string color1, string color2)
        {
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new Point(0, 0);
            brush.EndPoint = new Point(1, 1);
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString(color1), 0));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString(color2), 1));
            return brush;
        }

        private static void SetBrush(string key, string color)
        {
            Application.Current.Resources[key] =
                new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
        }
    }
}