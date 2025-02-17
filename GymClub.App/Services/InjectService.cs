using GymClub.Domain.Models;
using Microsoft.JSInterop;
using MudBlazor;
using Radzen;

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
        
        public async Task ShowErrorMessage(string message)
        {
            await _jSRuntime.InvokeVoidAsync("errorMessage", message);
        }

        public async Task ShowSuccessMessage(string message)
        {
            await _jSRuntime.InvokeVoidAsync("successMessage", message);
        }
    }
}
