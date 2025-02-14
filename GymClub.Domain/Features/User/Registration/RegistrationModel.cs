using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Features.User.Registration
{
    public class RegistrationModel
    {
    }

    public class RegistrationRequestModel
    {
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
    }
}
