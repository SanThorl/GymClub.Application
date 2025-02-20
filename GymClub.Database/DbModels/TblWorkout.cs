using System;
using System.Collections.Generic;

namespace GymClub.Database.DbModels;

public partial class TblWorkout
{
    public int Wid { get; set; }

    public string WorkoutName { get; set; } = null!;

    public string Place { get; set; } = null!;

    public string Level { get; set; } = null!;

    public byte? DelFlag { get; set; }
}
