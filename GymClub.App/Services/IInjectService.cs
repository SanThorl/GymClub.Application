using GymClub.Domain.Models;
using MudBlazor;

namespace GymClub.App.Services
{
    public interface IInjectService
    {
        Task EnableLoading();
        Task DisableLoading();
        Task ShowSuccessMessage(string message);
        Task ShowErrorMessage(string message);
        Task TogglePasswordVisibility();
    }
}
