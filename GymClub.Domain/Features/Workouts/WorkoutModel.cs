using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Features.Workouts;

public class WorkoutModel : BaseRequestModel
{
    public string WorkoutName { get; set; }
    public string Place { get; set; }
    public string Level { get; set; }
    public int WorkoutId { get; set; }
    public string WorkoutCode { get; set; }
    public decimal Price { get; set; }
}
public class WorkoutResponseModel
{
    public List<WorkoutModel> lstData { get; set; }
    public WorkoutModel Data { get; set; }
    public int TotalDays { get; set; }
}
public class ExerciseModel : BaseRequestModel
{
    public int ExerciseId { get; set; }
    public string ExerciseCode { get; set; }

    public string ExerciseName { get; set; } = null!;

    public string WorkoutCode { get; set; }

    public int Day { get; set; }

    public TimeOnly Time { get; set; }

    public int Calories { get; set; }

    public bool? DelFlag { get; set; }
    public bool? IsDone { get; set; }
    public TimeOnly RemainingTime { get; set; } // Timer Countdown

    // Convert TimeOnly to total seconds for easier countdown
    public int TotalSeconds => Time.Hour * 3600 + Time.Minute * 60 + Time.Second;
    public int RemainingSeconds { get; set; } // Used for countdown
    public bool IsRunning { get; set; } = false;
}
public class ExerciseResponseModel
{
    public List<ExerciseModel> lstExercise { get; set; }
}