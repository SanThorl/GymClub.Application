using GymClub.Domain.Features.User.Login;

namespace GymClub.App.Components.Pages
{
    public partial class FirstPage
    {
        private LoginRequestModel _reqModel = new LoginRequestModel();
        void SignUp()
        {
            _nav.NavigateTo("/register");
        }

        void Login()
        {
            _nav.NavigateTo("/signIn");
        }
    }
}
