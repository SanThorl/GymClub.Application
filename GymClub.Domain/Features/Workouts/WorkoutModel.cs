﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Features.Workouts;

public class WorkoutModel
{
    [Required]
    public string WorkoutName { get; set; }
    [Required]
    public string Place { get; set; }
    [Required]
    public string Level { get; set; }
    public int WId { get; set; }
}

public class ExerciseModel 
{
    public int Eid { get; set; }

    public string EName { get; set; } = null!;

    public int Wid { get; set; }

    public int Day { get; set; }

    public TimeOnly Time { get; set; }

    public int Calories { get; set; }

    public byte? DelFlag { get; set; }
}
public class WorkoutResponseModel
{
    public List<WorkoutModel> lstData { get; set; }
    public WorkoutModel Data { get; set; }
    public MessageResponseModel Response { get; set; }
    public List<ExerciseModel> EList { get; set; }
}
