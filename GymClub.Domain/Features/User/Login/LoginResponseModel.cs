using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Features.User.Login;

public class LoginResponseModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public ResponseModel Response { get; set; }
}
