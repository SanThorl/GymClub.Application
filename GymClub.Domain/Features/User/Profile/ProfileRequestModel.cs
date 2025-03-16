using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Features.User.Profile;

public class ProfileRequestModel
{
    public string UserId { get; set; }
    public string ImageUrl { get; set; }
    public string ImageFile { get; set; }
    public string ImageExtension { get; set; }
}

public class ProfileResponseModel
{
    public string PhoneNo { get; set; }
    public string UserName { get; set; }
    public string UserId { get; set; }
    public string? ImagePath { get; set; }
    public string? ImageStr { get; set; }
}

