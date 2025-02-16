using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymClub.Domain.Models;

namespace GymClub.Domain.Features.User.Registration;

public class RegistrationModel
{
}

public class RegistrationRequestModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string PhoneNo { get; set; }
    public string Password { get; set; }
    public string Gender { get; set; }
    public string DateOfBirth { get; set; }
}

public class RegistrationResponseModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string PhoneNo { get; set; }
    public string Password { get; set; }
    public string Gender { get; set; }
    public string DateOfBirth { get; set; }

    public MessageResponseModel Response { get; set; }
}


