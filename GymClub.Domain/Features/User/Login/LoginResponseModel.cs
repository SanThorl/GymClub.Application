using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymClub.Domain.Models;

namespace GymClub.Domain.Features.User.Login;

public class LoginResponseModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public MessageResponseModel Response { get; set; }
}
