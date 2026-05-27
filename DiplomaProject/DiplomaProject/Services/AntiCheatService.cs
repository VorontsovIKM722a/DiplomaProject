using Microsoft.JSInterop;

namespace DiplomaProject.Services
{
    public class AntiCheatService : IAsyncDisposable
    {
        private readonly IJSRuntime _js;
        private DotNetObjectReference<object>? _dotnetRef;

        public AntiCheatService(IJSRuntime js)
        {
            _js = js;
        }

      
        public async Task InitAsync(object component)
        {
            _dotnetRef = DotNetObjectReference.Create(component);
            await _js.InvokeVoidAsync("anticheat.init", _dotnetRef);
        }

        public async Task EnableExitWarningAsync()
        {
            await _js.InvokeVoidAsync("anticheat.enableExitWarning");
        }

        public async Task BlackoutAsync()
        {
            await _js.InvokeVoidAsync("anticheat.blackout");
        }

        public async Task ResetAsync()
        {
            await _js.InvokeVoidAsync("anticheat.reset");
        }

        public ValueTask DisposeAsync()
        {
            _dotnetRef?.Dispose();
            return ValueTask.CompletedTask;
        }
    }
}