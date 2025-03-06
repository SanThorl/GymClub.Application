using System;
using System.Collections.Generic;

namespace GymClub.Database.DbModels;

public partial class TblExercise
{
    public int Eid { get; set; }

    public string EName { get; set; } = null!;

    public int Wid { get; set; }

    public int Day { get; set; }

    public TimeOnly Time { get; set; }

    public int Calories { get; set; }

    public byte? DelFlag { get; set; }

    public string? Url { get; set; }
}
