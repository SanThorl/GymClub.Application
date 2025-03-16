using System;
using System.Collections.Generic;

namespace GymClub.Database.DbModels;

public partial class TblUser
{
    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public DateTime JoinDate { get; set; }

    public bool DelFlag { get; set; }

    public string? ImagePath { get; set; }

    public DateTime? UpdatedTime { get; set; }
}
