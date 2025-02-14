using GymClub.Domain.Features.User.Login;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace GymClub.App.Components.Pages
{
    public partial class SignIn
    {
        private string _cookieDataStr { get; set; } = "";
        [Inject] private AuthenticationStateProvider authStateProvider { get; set; }

        private LoginModel _reqModel = new LoginModel();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _injectService.EnableLoading();
                //var customAuthStateProvider = (CustomAuthenticationStateProvider)_authStateProvider;
                await _injectService.DisableLoading();
            }
        }

        private void LogIn()
        {

        }
    }
}
