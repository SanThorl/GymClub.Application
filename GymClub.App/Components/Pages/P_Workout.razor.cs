using GymClub.Domain.Features.Workouts;
using GymClub.Domain.Models;
using GymClub.Shared.Enum;
using Microsoft.AspNetCore.Components.Web;

namespace GymClub.App.Components.Pages
{
    public partial class P_Workout
    {
        private WorkoutResponseModel model;
        private List<WorkoutModel> lst;
        private List<ExerciseModel> lstExercise;
        private List<ExerciseModel> eListForEachDay = new();
        private EnumFormType _formType = EnumFormType.WorkoutList;
        private IList<WorkoutModel> _selectedWorkout = new List<WorkoutModel>();

        protected override async Task OnInitializedAsync()
        {
            await List();
        }

        private async Task List()
        {
            model = await _workout.GetWorkoutList();
            lst = model.lstData;
        }

        async Task WorkoutCollection(int workoutId)
        {
            await _injectService.EnableLoading();
             model = await _workout.GetWorkoutById(workoutId);
            lstExercise = model.EList;
            await _injectService.DisableLoading();
            _formType = EnumFormType.ExerciseList;
        }

        private async Task ShowExercises(int day)
        {
            await _injectService.EnableLoading();
            eListForEachDay = lstExercise.Where(x => x.Day == day).ToList();

            foreach (var exercise in eListForEachDay)
            {
                exercise.RemainingSeconds = exercise.TotalSeconds;
                exercise.RemainingTime = exercise.Time;
            }
            await _injectService.DisableLoading();
        }

        async Task Back()
        {
            await List();
            _formType = EnumFormType.WorkoutList;
        }
    }
}
