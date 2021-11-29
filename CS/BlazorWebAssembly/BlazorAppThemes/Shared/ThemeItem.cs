using System;

namespace BlazorAppThemes.Shared {
    public record ThemeItem(string Name, string StylesheetLinkUrl) {
        public static  readonly ThemeItem Default = Create("default");
        public static  ThemeItem Create(string name) => new ThemeItem(name, $"css/switcher-resources/themes/{name}/bootstrap.min.css");
    };
}
