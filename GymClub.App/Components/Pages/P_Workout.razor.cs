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

        private bool CheckSelected(int wid)
        {
            bool isSelected = wid == 0;
            _selectedWorkout = new List<WorkoutModel>();
            return isSelected;
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
            var eListForDay = lstExercise.Where(x => x.Day == day).ToList();
            await _injectService.DisableLoading();
        }

        async Task Back()
        {
            await List();
            _formType = EnumFormType.WorkoutList;
        }
    }
}
