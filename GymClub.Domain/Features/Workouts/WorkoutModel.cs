using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Features.Workouts;

public class WorkoutModel : BaseRequestModel
{
    [Required]
    public string WorkoutName { get; set; }
    [Required]
    public string Place { get; set; }
    [Required]
    public string Level { get; set; }
    public int WorkoutId { get; set; }
}

public class ExerciseModel : BaseRequestModel
{
    public int ExerciseId { get; set; }

    public string ExerciseName { get; set; } = null!;

    public int WorkoutId { get; set; }

    public int Day { get; set; }

    public TimeOnly Time { get; set; }

    public int Calories { get; set; }

    public byte? DelFlag { get; set; }
    public byte? IsDone { get; set; }
    public TimeOnly RemainingTime { get; set; } // Timer Countdown

    // Convert TimeOnly to total seconds for easier countdown
    public int TotalSeconds => Time.Hour * 3600 + Time.Minute * 60 + Time.Second;
    public int RemainingSeconds { get; set; } // Used for countdown
    public bool IsRunning { get; set; } = false;
}
public class WorkoutResponseModel
{
    public List<WorkoutModel> lstData { get; set; }
    public WorkoutModel Data { get; set; }
    public MessageResponseModel Response { get; set; }
    public List<ExerciseModel> ExerciseList { get; set; }
}
