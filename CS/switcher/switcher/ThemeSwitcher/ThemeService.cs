using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace switcher.ThemeSwitcher {
    public interface IThemeChangeRequestDispatcher {
        void RequestThemeChange(Theme theme);
    }

    public interface IThemeLoadNotifier {
        Task NotifyThemeLoadedAsync(Theme theme);
    }

    public class ThemeService {
        public static bool EnableNewBlazorThemes = true;
        readonly Dictionary<string, string> newBlazorThemesMapping = new() {
            ["blazing-berry"] = "blazing-berry.bs5",
            ["blazing-dark"] = "blazing-dark.bs5",
            ["office-white"] = "office-white.bs5",
            ["purple"] = "purple.bs5",
        };
        private Theme _activeTheme;

        const string DefaultThemeName = "blazing-berry";
        readonly Dictionary<string, string> HighlightJSThemes = new Dictionary<string, string>() {
            { DefaultThemeName, "default" },
            { "blazing-dark", "androidstudio" },
            { "cyborg", "androidstudio" },
            { "default-dark", "androidstudio" }
        };

        public IThemeChangeRequestDispatcher ThemeChangeRequestDispatcher { get; set; }

        public IThemeLoadNotifier ThemeLoadNotifier { get; set; }

        public ThemeService() {
            ResourcesReadyState = new ConcurrentDictionary<string, TaskCompletionSource<bool>>();
            ThemeSets = CreateSets(this);
        }

        public ConcurrentDictionary<string, TaskCompletionSource<bool>> ResourcesReadyState { get; }
        public List<ThemeSet> ThemeSets { get; }
        public Theme ActiveTheme => _activeTheme;
        public Theme DefaultTheme {
            get { return ThemeSets.SelectMany(ts => ts.Themes).FirstOrDefault(t => t.Name == DefaultThemeName); }
        }

        public string GetThemeCssUrl(Theme theme) {
            if(EnableNewBlazorThemes) {
                if (this.newBlazorThemesMapping.ContainsKey(theme.Name))
                    return $"_content/DevExpress.Blazor.Themes/{this.newBlazorThemesMapping.GetValueOrDefault(theme.Name)}.min.css";
                return $"_content/DevExpress.Blazor.Themes/bootstrap-external.bs5.min.css";
            }
            return GetBootstrapThemeCssUrl(theme);
        }
        public string GetBootstrapThemeCssUrl(Theme theme) {
            if(!EnableNewBlazorThemes || theme.IsBootstrapNative) {
                return $"switcher-resources/css/themes/{theme.ThemePath}/bootstrap.min.css";
            }
            return null;
        }
        public string GetActiveThemeCssUrl() {
            return GetThemeCssUrl(ActiveTheme);
        }
        public string GetActiveBootstrapThemeCssUrl() {
            return GetBootstrapThemeCssUrl(ActiveTheme);
        }
        public string GetHighlightJSThemeCssUrl(Theme theme) {
            var highlightjsTheme = HighlightJSThemes[DefaultThemeName];
            if(HighlightJSThemes.ContainsKey(theme.Name))
                highlightjsTheme = HighlightJSThemes[theme.Name];
            return $"https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.15.6/styles/{highlightjsTheme}.min.css";
        }
        public string GetActiveHighlightJSThemeCssUrl() {
            return GetHighlightJSThemeCssUrl(ActiveTheme);
        }

        public void SetActiveThemeByName(string themeName) {
            var theme = FindThemeByName(themeName);
            if(theme != null)
                _activeTheme = theme;
            else
                _activeTheme = DefaultTheme;
        }
        private Theme FindThemeByName(string themeName) {
            var themes = ThemeSets.SelectMany(ts => ts.Themes);
            foreach(var theme in themes) {
                if(theme.Name == themeName)
                    return theme;
            }
            return null;
        }

        private static List<ThemeSet> CreateSets(ThemeService config) {
            return new List<ThemeSet>() {
                new ThemeSet("DevExpress Themes", "blazing-berry", "blazing-dark", "purple", "office-white"),
                new ThemeSet("Bootstrap Themes", "default", "default-dark", "cerulean", "cyborg", "flatly", "journal", "litera", "lumen", "lux", "pulse", "simplex", "solar", "superhero", "united", "yeti")
            };
        }
    }
}
