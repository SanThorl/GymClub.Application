using GymClub.Domain.Features.User.Login;

namespace GymClub.App.Components.Pages
{
    public partial class FirstPage
    {
        private LoginModel _reqModel = new LoginModel();
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
