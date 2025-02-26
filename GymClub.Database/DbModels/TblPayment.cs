using System;
using System.Collections.Generic;

namespace GymClub.Database.DbModels;

public partial class TblPayment
{
    public int Pid { get; set; }

    public int? Wid { get; set; }

    public string? WorkoutName { get; set; }

    public decimal? Price { get; set; }

    public byte? IsPaid { get; set; }
}
