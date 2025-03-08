using System;
using System.Collections.Generic;

namespace GymClub.Database.DbModels;

public partial class TblExercise
{
    public int ExerciseId { get; set; }

    public string ExerciseName { get; set; } = null!;

    public int WorkoutId { get; set; }

    public int Day { get; set; }

    public TimeOnly Time { get; set; }

    public int Calories { get; set; }

    public byte? DelFlag { get; set; }

    public string? Url { get; set; }

    public byte? IsDone { get; set; }
}
