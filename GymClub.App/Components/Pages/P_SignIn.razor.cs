using GymClub.Domain.Features.User.Login;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;


namespace GymClub.App.Components.Pages
{
    public partial class P_SignIn
    {
        [Inject] private AuthenticationStateProvider authStateProvider { get; set; }

        private LoginRequestModel _reqModel = new LoginRequestModel();
        private LoginResponseModel model;
        private string _cookieDataStr { get; set; } = "";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _injectService.EnableLoading();
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
            if (model.Response.IsError)
            {
                await _injectService.ShowErrorMessage(model.Response.Message);
                _nav.NavigateTo("/signIn");
            }
            var userSessionModel = new UserSessionModel
            {
                UserName = model.UserName,
                UserId = model.UserId,
                SessionId = model.SessionId
            };
            //var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
            //await customAuthStateProvider.UpdateAuthenticationState(userSessionModel);
            _nav.NavigateTo("/workout");
        }
    }
}
