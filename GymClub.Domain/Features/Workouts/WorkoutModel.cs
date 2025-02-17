using System;
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
}

public class WorkoutResponseModel
{
    public List<WorkoutModel> lstData { get; set; }
    public WorkoutModel Data { get; set; }
    public MessageResponseModel Response { get; set; }
}