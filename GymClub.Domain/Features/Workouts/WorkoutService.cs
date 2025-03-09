using GymClub.Database.DbModels;
using GymClub.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Features.Workouts;

public class WorkoutService
{
    private readonly AppDbContext _db;
    private readonly DapperService _dapperService;
    private ILogger<WorkoutService> _logger;
    public WorkoutService(AppDbContext db, DapperService dapperService, ILogger<WorkoutService> logger)
    {
        _db = db;
        _dapperService = dapperService;
        _logger = logger;
    }

    public async Task<WorkoutResponseModel> GetWorkoutList()
    {
        WorkoutResponseModel model = new WorkoutResponseModel();
        try
        {
            var lst = _dapperService.Query<WorkoutModel>(SqlQueries.WorkoutList).ToList();
            if (lst is null)
            {
                model.Response = new MessageResponseModel
                {
                    IsSuccess = false,
                    Message = "Workouts are not available now!"
                };
                goto result;
            }
            model.lstData = lst.Select(x => new WorkoutModel
            {
                WorkoutName = x.WorkoutName,
                Place = x.Place,
                Level = x.Level,
                WorkoutId = x.WorkoutId
            }).ToList();

            model.Response = new MessageResponseModel
            {
                IsSuccess = true,
                Message = "The clock is ticking. Go on, keep building the person you want to be!"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

    result:
        return model;
    }

    public async Task<WorkoutResponseModel> GetWorkoutById(int id)
    {
        WorkoutResponseModel model = new WorkoutResponseModel();
        try
        {
            var data = await _db.TblWorkouts.FirstOrDefaultAsync(x => x.WorkoutId == id);
            if (data is null)
            {
                model.Response = new MessageResponseModel
                {
                    IsSuccess = false,
                    Message = "Workout is not available now!"
                };
                goto result;
            }
            model.Data = new WorkoutModel
            {
                WorkoutName = data.WorkoutName,
                Place = data.Place,
                Level = data.Level,
                WorkoutId = data.WorkoutId
            };

            var days = await _db.TblExercises
                .AsNoTracking()
                .Where(x => x.WorkoutId == id)
                .Select(x => new ExerciseModel
                {
                    ExerciseName = x.ExerciseName,
                    WorkoutId = x.WorkoutId,
                    Day = x.Day,
                    Time = x.Time,
                    Calories = x.Calories,
                    DelFlag = x.DelFlag
                })
                .Distinct().ToListAsync();

            model.ExerciseList = days;
            model.Response = new MessageResponseModel
            {
                IsSuccess = true,
                Message = "The clock is ticking. Go on, keep building the person you want to be!"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

    result:
        return model;
    }

    public async Task UpdateDay(int workoutId,int day)
    {
        //var result = _dapperService.Execute(SqlQueries.FinishedExercises, new { workoutId, day });
        await _db.TblExercises
            .Where(x => x.Day == day && x.WorkoutId == workoutId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.IsDone, true));
    }
}