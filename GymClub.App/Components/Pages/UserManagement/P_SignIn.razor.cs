using GymClub.Domain.Features.User.Login;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using GymClub.Shared;


namespace GymClub.App.Components.Pages.UserManagement
{
    public partial class P_SignIn
    {
        [Inject] private AuthenticationStateProvider authStateProvider { get; set; }
        private LoginRequestModel _reqModel = new LoginRequestModel();
        private Result<LoginResponseModel> model;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _injectService.EnableLoading();
                var customAuthStateProvider = (CustomAuthenticationStateProvider)_authStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);

                StateHasChanged();
                await _injectService.DisableLoading();
            }
        }

        async Task LogIn()
        {
            if (_reqModel.UserName.IsNullOrEmpty())
            {
                await _injectService.ShowErrorMessage("User Name is Requied!");
                return;
            }
            if (_reqModel.Password.IsNullOrEmpty())
            {
                await _injectService.ShowErrorMessage("Use Password for your privacy!");
                return;
            }
            model = await _login.SignIn(_reqModel);

            if (!model.Success)
            {
                await _injectService.ShowErrorMessage(model.Message);
                return;
            }
            var userSessionModel = new UserSessionModel
            {
                UserName = model.Data!.UserName,
                UserId = model.Data.UserId,
            };
            var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
            await customAuthStateProvider.UpdateAuthenticationState(userSessionModel);
            _nav.NavigateTo("/workout");
        }

        private async Task TogglePasswordVisibility()
        {
            await _injectService.TogglePasswordVisibility();
        }
    }
}
