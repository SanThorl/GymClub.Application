using GymClub.Database.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Features.Workouts
{
    public class WorkoutService
    {
        private readonly AppDbContext _db;

        public WorkoutService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<WorkoutResponseModel> GetWorkoutList()
        {
            WorkoutResponseModel model = new WorkoutResponseModel();

            var lst = await _db.TblWorkouts.AsNoTracking().ToListAsync();

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
                Level = x.Level
            }).ToList();

            model.Response = new MessageResponseModel
            {
                IsSuccess = true,
                Message = "The clock is ticking. Go on, keep building the person you want to be!"
            };
        result:
            return model;
        }
    }
}
