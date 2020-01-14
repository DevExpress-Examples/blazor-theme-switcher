# How to implement a Theme Switcher in Blazor applications

This example demonstrates how to create a Theme Switcher as in [DevExpress Blazor Demos](https://demos.devexpress.com/blazor/) and apply the selected theme to an application dynamically. The example contains solutions both for server-side and client-side Blazor.

This Theme Switcher offers the standard Bootstrap theme, [2 DevExpress Bootstrap themes](https://github.com/DevExpress/bootstrap-themes), and [21 Bootstwatch themes](https://bootswatch.com/). 

Refer to the [Themes](https://docs.devexpress.com/Blazor/401523/common-concepts/themes#implement-a-theme-switcher) documentation topic for more information.

![](images/blazor-theme-switcher.png)

*Files to look at*:

**Server-Side Blazor**
* [ThemeSwitcher.razor](./CS/ServerSideBlazor/BlazorAppThemes/ThemeSwitcher/ThemeSwitcher.razor)
* [MainLayout.razor](./CS/ServerSideBlazor/BlazorAppThemes/Shared/MainLayout.razor)
* [Index.razor](./CS/ServerSideBlazor/BlazorAppThemes/Pages/Index.razor)
* [site.css](./CS/ServerSideBlazor/BlazorAppThemes/wwwroot/css/site.css)
* [switcher-resources](./CS/ServerSideBlazor/BlazorAppThemes/wwwroot/css/switcher-resources) (folder)
* [ThemeLink.razor](./CS/ServerSideBlazor/BlazorAppThemes/ThemeSwitcher/ThemeLink.razor) 
* [LinkService.cs](./CS/ServerSideBlazor/BlazorAppThemes/ThemeSwitcher/LinkService.cs)
* [Startup.cs](./CS/ServerSideBlazor/BlazorAppThemes/Startup.cs)
* [_Host.cshtml](./CS/ServerSideBlazor/BlazorAppThemes/Pages/_Host.cshtml)

**Client-Side Blazor**
* [ThemeSwitcher.razor](./CS/ClientSideBlazor/BlazorAppThemes/ThemeSwitcher/ThemeSwitcher.razor)
* [MainLayout.razor](./CS/ClientSideBlazor/BlazorAppThemes/Shared/MainLayout.razor) 
* [Index.razor](./CS/ClientSideBlazor/BlazorAppThemes/Pages/Index.razor) 
* [site.css](./CS/ClientSideBlazor/BlazorAppThemes/wwwroot/css/site.css) 
* [switcher-resources](./CS/ClientSideBlazor/BlazorAppThemes/wwwroot/css/switcher-resources) (folder)



