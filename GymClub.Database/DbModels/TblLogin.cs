using System;
using System.Collections.Generic;

namespace GymClub.Database.DbModels;

public partial class TblLogin
{
    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? SessionId { get; set; }

    public DateTime? SessionExpiredDate { get; set; }
}
