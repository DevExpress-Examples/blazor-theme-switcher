using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorAppThemes.Shared {
    public partial class ThemeSwitcher {
        public sealed class ThemeSelectorContext {
            public bool ThemeSelectorVisible { get;}
            public RenderFragment ThemeSelectorView { get; }

            public ThemeSelectorContext(bool visible, RenderFragment view) {
                ThemeSelectorVisible = visible;
                ThemeSelectorView = view;
            }
        }
        public sealed class ToggleButtonContext {
            public string CurrentTheme { get; }
            public Action ToggleThemeSelectorContainer { get; }
            public bool ThemeSelectorVisible { get; }

            public ToggleButtonContext(ThemeSwitcher owner) {
                var toggleState = !owner.themeSelectorVisible;
                CurrentTheme = owner.currentTheme;
                ToggleThemeSelectorContainer = () => {
                    owner.themeSelectorVisible = toggleState;
                    owner.StateHasChanged();
                };
            }

        }
        public sealed class StylesheetLinkContext {
            public string CurrentTheme { get; }
            public bool ThemeSelectorVisible { get; }
            public Action HideThemeSelectorContainer { get; }
            public Action ShowThemeSelectorContainer { get; }
            public RenderFragment ToggleButton { get; }
            public RenderFragment ThemeSelectorContainer { get; }
            public StylesheetLinkContext(ThemeSwitcher owner, RenderFragment toggleButton, RenderFragment themeSelectorContainer) {
                ThemeSelectorVisible = owner.themeSelectorVisible;
                CurrentTheme = $"css/switcher-resources/themes/{owner.currentTheme}/bootstrap.min.css";
                ToggleButton = toggleButton;
                ThemeSelectorContainer = themeSelectorContainer;
                HideThemeSelectorContainer = () => {
                    owner.themeSelectorVisible = false;
                    owner.StateHasChanged();
                };
                ShowThemeSelectorContainer = () => {
                    owner.themeSelectorVisible = true;
                    owner.StateHasChanged();
                };
                
            }
        }

        sealed class JSModuleSource : Lazy<TaskCompletionSource<IJSObjectReference>>, IAsyncDisposable {
            public JSModuleSource() : base(() => new TaskCompletionSource<IJSObjectReference>(TaskCreationOptions.RunContinuationsAsynchronously)) { }

            public async ValueTask Attach(IJSRuntime runtime) {
                try {
                    var module = await runtime.InvokeAsync<IJSObjectReference>("import", "/theme-switcher.js");
                    Value.TrySetResult(module);
                } catch (OperationCanceledException e) {
                    Value.TrySetCanceled(e.CancellationToken);
                } catch (AggregateException e) {
                    Value.TrySetException(e.InnerExceptions);
                } catch (Exception e) {
                    Value.TrySetException(e);
                }
            }
            public async ValueTask<string> PreloadStylesheetResource(string theme) {
                try {
                    var module = await Value.Task;
                    return await module.InvokeAsync<string>("preloadStylesheetResource", theme);
                } catch (OperationCanceledException) { }
                return string.Empty;
            }
            public async  ValueTask DisposeAsync() {
                try {
                    if(IsValueCreated && !Value.TrySetCanceled()) {
                        await Value.Task.Result.DisposeAsync().ConfigureAwait(false);
                    }
                } catch (OperationCanceledException) { }
            }
        }
    }
}
