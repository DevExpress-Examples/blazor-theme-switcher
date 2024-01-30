<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/227836631/23.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T845557)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to implement a Theme Switcher in Blazor applications

This example demonstrates how you can add a Theme Switcher to your application. The switcher in this example is the same as in the [DevExpress Blazor Demos](https://demos.devexpress.com/blazor/). The Theme Switcher component allows users to switch between [DevExpress built-in themes](https://docs.devexpress.com/Blazor/401523/common-concepts/customize-appearance/themes) and external Bootstrap themes (the default theme and [free Bootswatch options](https://bootswatch.com/)).

![Blazor - Theme Switcher](images/blazor-theme-switcher.png)

The example's solution targets .NET 8, but you can also integrate the Theme Switcher in projects that target .NET 6 and .NET 7.

## Add the Theme Switcher to an Application

Follow the steps below to add a Theme Switcher into your application:

1. Copy this example's [ThemeSwitcher](./CS/switcher/switcher/ThemeSwitcher) folder to your application.
2. In the *_Imports.razor* file, import the `switcher.ThemeSwitcher` namespace and files located in the *ThemeSwitcher* folder:

    ```cs
    @using <YourProjectName>.ThemeSwitcher
    @using switcher.ThemeSwitcher
    ```

3. Copy this example's [switcher-resources](./CS/switcher/switcher/wwwroot/switcher-resources) folder to your application's *wwwroot* folder. The *switcher-resources* folder has the following structure:

    * **css/themes**  
    Includes nested folders whose names correspond to external Bootstrap themes. Each nested folder stores an external theme's stylesheet (the *bootstrap.min.css* file). Note that built-in DevExpress themes are added to the application with DevExpress Blazor components and stored separately in the **DevExpress.Blazor.Themes** NuGet package.

    * **css/themes.css**  
    Contains CSS rules used to draw colored squares for each theme in the Theme Switcher.
    * **css/theme-switcher.css**  
    Contains CSS rules that define the Theme Switcher's appearance and behavior.
    * **theme-controler.js**  
    Contains functions that add and remove links to theme stylesheets.
    * **theme.svg**  
    An icon displayed in the Theme Switcher.

4. Register the Theme Switcher's styles in the `head` section of the layout file:
    ```html
    <head>
       <link href="switcher-resources/css/theme-switcher.css" rel="stylesheet" />
       <link href="switcher-resources/css/themes.css" rel="stylesheet" />
       @* ... *@
    </head>
    ```
5. Add the following `div` element to the `body` section of the layout file:
    ```html
    <body>
       <div id="switcher-container" data-permanent></div>
        @* ... *@
    </body>
    ```
6. Register the `ThemeService` in the `Program.cs` file:
    ```cs
    builder.Services.AddScoped<ThemeService>();
    ```
7. Declare the Theme Switcher component in the *MainLayout.razor* file:    
    ```razor
    <ThemeSwitcher />
    ``` 
8. *For .NET 6 and .NET 7 applications.* Remove the `@rendermode InteractiveServer` directive from the [ThemeSwitcher.razor](./CS/switcher/switcher/ThemeSwitcher/ThemeSwitcher.razor#L2), [ThemeSwitcherContainer.razor](./CS/switcher/switcher/ThemeSwitcher/ThemeSwitcherContainer.razor#L4), and [ThemeSwitcherItem.razor](./CS/switcher/switcher/ThemeSwitcher/ThemeSwitcherItem.razor#L2) files. 

## Add Themes to the Theme Switcher

Follow the steps below to add a Bootstrap theme to the Theme Switcher:

1. In the **wwwroot/css/themes** folder, create a new folder for your theme. The folder and theme names should match.

2. Add the theme's stylesheet (the *bootstrap.min.css* file) to the newly created folder.

3. Add the following CSS rule to the *wwwroot/css/themes.css* file:

    ```css
    .blazor-themes a.<your-theme-name>:before {
        background: <your-theme-color>;
    }
    ```

4. In the *ThemeService.cs* file, add the theme name to **Bootstrap Themes** theme set:

    ```cs
    private static List<ThemeSet> CreateSets(ThemeService config) {
        return new List<ThemeSet>() {
            new ThemeSet("DevExpress Themes", "blazing-berry", "blazing-dark", "purple", "office-white"),
            new ThemeSet("Bootstrap Themes", "<your-theme-name>", "default", "default-dark", "cerulean")
        };
    }
    ```

## Remove Themes from the Theme Switcher

Follow the steps below to remove a DevExpress or Bootstrap theme from the Theme Switcher:

1. Open the *ThemeService.cs* file and remove the theme name from the **DevExpress Themes** or **Bootstrap Themes** theme set:

    ```cs
    private static List<ThemeSet> CreateSets(ThemeService config) {
        return new List<ThemeSet>() {
            new ThemeSet("DevExpress Themes", "blazing-berry", "blazing-dark", "purple", "office-white"),
            new ThemeSet("Bootstrap Themes", /*"<your-theme-name>"*/, "default", "default-dark", "cerulean")
        };
    }
    ```

2. Remove the CSS rule that corresponds to the theme from the *wwwroot/css/themes.css* file.

    ```css
    /* .blazor-themes a.<your-theme-name>:before {
        background: <your-theme-color>;
    }*/
    ```

3. *For a Bootstrap theme.* Delete the *wwwroot/css/themes/\<your-theme-name\>* folder.

## Files to Review

* [ThemeSwitcher](./CS/switcher/switcher/ThemeSwitcher) (folder)
* [switcher-resources](./CS/switcher/switcher/wwwroot/switcher-resources) (folder)
* [App.razor](./CS/switcher/switcher/App.razor)
* [MainLayout.razor](./CS/switcher/switcher/Layout/MainLayout.razor)
* [Program.cs](./CS/switcher/switcher/Program.cs)

## Documentation

* [Themes](https://docs.devexpress.com/Blazor/401523/common-concepts/themes)
