
namespace GymClub.App.Components.Pages
{
    public partial class P_Register
    {
        [Inject] private AuthenticationStateProvider authStateProvider { get; set; }
        private RegistrationRequestModel _reqModel = new RegistrationRequestModel();
        private UserSessionModel _userSession;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _injectService.EnableLoading();
                var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
                var authState = await customAuthStateProvider.GetAuthenticationStateAsync();
                if (authState.User.Identity != null && !authState.User.Identity.IsAuthenticated)
                {
                    _nav.NavigateTo(Page_SignIn);
                    return;
                }

                _userSession = await customAuthStateProvider.GetUserData();
                _injectService.DisableLoading();
            }
        }
        private void SignUp()
        {

        }

    }
}
