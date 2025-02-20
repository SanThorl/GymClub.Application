using System;
using System.Collections.Generic;

namespace GymClub.Database.DbModels;

public partial class TblLogin
{
    public int LoginId { get; set; }

    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? SessionId { get; set; }

    public DateTime? SessionExpiredDate { get; set; }
}
