using System;
using System.Collections.Generic;

namespace GymClub.Database.DbModels;

public partial class TblWorkout
{
    public int WorkoutId { get; set; }

    public string? WorkoutCode { get; set; }

    public string WorkoutName { get; set; } = null!;

    public string Place { get; set; } = null!;

    public string Level { get; set; } = null!;

    public bool? DelFlag { get; set; }

    public decimal? Price { get; set; }
}
