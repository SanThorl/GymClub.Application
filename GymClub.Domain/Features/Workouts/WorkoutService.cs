using Dapper;
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

    public async Task<Result<WorkoutResponseModel>> GetWorkoutList()
    {
        WorkoutResponseModel model = new WorkoutResponseModel();
        try
        {
            var lst = _dapperService.Query<WorkoutModel>(SqlQueries.WorkoutList).ToList();
            if (lst is null)
            {
                //model.Response = new MessageResponseModel
                //{
                //    IsSuccess = false,
                //    Message = "Workouts are not available now!"
                //};
                //goto result;
                return Result<WorkoutResponseModel>.FailureResult("Workouts are not available now!");
            }

            model.lstData = lst.Select(x => new WorkoutModel
            {
                WorkoutCode = x.WorkoutCode,
                WorkoutName = x.WorkoutName,
                Place = x.Place,
                Level = x.Level
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result<WorkoutResponseModel>.FailureResult(ex);
        }
        return Result<WorkoutResponseModel>.SuccessResult(model, "The clock is ticking.Go on, keep building the person you want to be!");
    }

    public async Task<Result<WorkoutResponseModel>> GetWorkoutById(string workoutCode)
    {
        WorkoutResponseModel model = new WorkoutResponseModel();
        try
        {
            var data = await _db.TblWorkouts.FirstOrDefaultAsync(x => x.WorkoutCode == workoutCode);
            if (data is null)
            {
                return Result<WorkoutResponseModel>.FailureResult("Workout is not available now!");
            }
            model.Data = new WorkoutModel
            {
                WorkoutCode = data.WorkoutCode,
                WorkoutName = data.WorkoutName,
                Place = data.Place,
                Level = data.Level,
                WorkoutId = data.WorkoutId
            };

            var days = await _db.TblExercises
                .AsNoTracking()
                .Where(x => x.WorkoutCode == workoutCode)
                .Select(x => new ExerciseModel
                {
                    ExerciseName = x.ExerciseName,
                    WorkoutCode = x.WorkoutCode,
                    Day = x.Day,
                    Time = x.Time,
                    Calories = x.Calories,
                    DelFlag = x.DelFlag
                })
                .GroupBy(x => x.Day)
                .ToListAsync();

            model.TotalDays = days.Count;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result<WorkoutResponseModel>.FailureResult(ex);
        }
        return Result<WorkoutResponseModel>.SuccessResult(model, "The clock is ticking. Go on, keep building the person you want to be!");
    }

    public async Task UpdateDay(string workoutCode, int day)
    {
        //var result = _dapperService.Execute(SqlQueries.FinishedExercises, new { workoutId, day });
        await _db.TblExercises
            .Where(x => x.Day == day && x.WorkoutCode == workoutCode)
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.IsDone, true));
    }

    public async Task<Result<ExerciseResponseModel>> GetExerciseListForEachDay(string workoutCode, int day)
    {
        ExerciseResponseModel model = new ExerciseResponseModel();
        try
        {
            var lst = await _db.TblExercises
                .AsNoTracking()
                .Where(x => x.WorkoutCode == workoutCode && x.Day == day)
                .Select(x => new ExerciseModel
                {
                    ExerciseCode = x.ExerciseCode,
                    ExerciseName = x.ExerciseName,
                    WorkoutCode = x.WorkoutCode,
                    Day = x.Day,
                    Time = x.Time,
                    Calories = x.Calories,
                    DelFlag = x.DelFlag,
                    IsDone = x.IsDone
                })
                .ToListAsync();
            model.lstExercise = lst;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result<ExerciseResponseModel>.FailureResult(ex);
        }
        return Result<ExerciseResponseModel>.SuccessResult(model, "The clock is ticking. Go on, keep building the person you want to be!");
    }
}