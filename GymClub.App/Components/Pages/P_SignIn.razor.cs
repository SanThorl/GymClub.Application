using GymClub.Domain.Features.User.Login;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using GymClub.App.Services.Security;

namespace GymClub.App.Components.Pages
{
    public partial class P_SignIn
    {
        [Inject] private AuthenticationStateProvider authStateProvider { get; set; }

        private LoginRequestModel _reqModel = new LoginRequestModel();
        private string _cookieDataStr { get; set; } = "";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                StateHasChanged();
                await _injectService.DisableLoading();
            }
        }

        async Task LogIn()
        {

        }
    }
}
