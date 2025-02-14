using System;
using System.Collections.Generic;

namespace GymClub.Database.DbModels;

public partial class TblUser
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public DateOnly JoinDate { get; set; }

    public bool? DelFlag { get; set; }
}
