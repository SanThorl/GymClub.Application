using System;
using System.Collections.Generic;

namespace GymClub.Database.DbModels;

public partial class TblLogin
{
    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;
}
