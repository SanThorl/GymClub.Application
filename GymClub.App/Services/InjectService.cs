using Microsoft.JSInterop;

namespace GymClub.App.Services
{
    public class InjectService : IInjectService
    {
        private readonly IJSRuntime _jSRuntime;

        public InjectService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task EnableLoading()
        {
            await _jSRuntime.InvokeVoidAsync("enableLoading", true);
        }

        public async Task DisableLoading()
        {
            await _jSRuntime.InvokeVoidAsync("enableLoading", false);
        }
    }
}
