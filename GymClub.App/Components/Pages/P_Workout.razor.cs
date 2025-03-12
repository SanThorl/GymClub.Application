using GymClub.Domain.Features.Workouts;
using GymClub.Domain.Models;
using GymClub.Shared;
using GymClub.Shared.Enum;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.Metrics;

namespace GymClub.App.Components.Pages
{
    public partial class P_Workout
    {
        private UserSessionModel _userSession;
        private Result<WorkoutResponseModel> model;
        private List<WorkoutModel> lst;
        private List<ExerciseModel> lstExercise;
        private List<ExerciseModel> eListForEachDay = new();
        private EnumFormType _formType = EnumFormType.WorkoutList;
        private IList<WorkoutModel> _selectedWorkout = new List<WorkoutModel>();
        private int _selectedDay;
        private int _selectedWorkoutId;
        private WorkoutModel data;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    await _injectService.EnableLoading();
                    var customAuthStateProvider = (CustomAuthenticationStateProvider)_authStateProvider;
                    var authState = await customAuthStateProvider.GetAuthenticationStateAsync();
                    _userSession = await customAuthStateProvider.GetUserData();
                    await _injectService.DisableLoading();
                    await List();
                    StateHasChanged();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }
        }
        private async Task List()
        {
            model = await _workout.GetWorkoutList();
            lst = model.Data!.lstData;
        }

        async Task WorkoutCollection(int workoutId)
        {
            try
            {
                await _injectService.EnableLoading();
                model = await _workout.GetWorkoutById(workoutId);
                _selectedWorkoutId = workoutId;
                lstExercise = model.Data!.ExerciseList;
                data = model.Data.Data;
                await _injectService.DisableLoading();
                _formType = EnumFormType.DayList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private async Task ShowExercises(int day)
        {
            try
            {
                _selectedDay = day;
                if(_selectedDay > 2)
                {

                }
                await _injectService.EnableLoading();
                eListForEachDay = lstExercise.Where(x => x.Day == day).ToList();

                _formType = EnumFormType.ExerciseList;
                foreach (var exercise in eListForEachDay)
                {
                    exercise.RemainingSeconds = exercise.TotalSeconds;
                    exercise.RemainingTime = exercise.Time;
                }
                await _injectService.DisableLoading();
            }
            catch (Exception ex)
            {
                await _injectService.DisableLoading();
                _logger.LogError(ex, ex.Message);
            }
        }

        private async Task StartTimer(ExerciseModel exercise)
        {
            exercise.IsRunning = true;

            while (exercise.RemainingSeconds > 0 && exercise.IsRunning)
            {
                await Task.Delay(1000);
                exercise.RemainingSeconds--;

                // Update RemainingTime (Convert from seconds)
                exercise.RemainingTime = TimeOnly.FromTimeSpan(TimeSpan.FromSeconds(exercise.RemainingSeconds));

                StateHasChanged();
            }

            exercise.IsRunning = false;
        }

        private async Task PauseTimer(ExerciseModel exercise)
        {
            exercise.IsRunning = false;
        }

        private async Task ResetTimer(ExerciseModel exercise)
        {
            exercise.RemainingSeconds = exercise.TotalSeconds;
            exercise.RemainingTime = exercise.Time;
            StateHasChanged();
        }

        private string FormatTime(int seconds)
        {
            return TimeOnly.FromTimeSpan(TimeSpan.FromSeconds(seconds)).ToString("mm':'ss");
        }
        async Task Back()
        {
            _formType = EnumFormType.WorkoutList;
            await List();
        }

        private async Task Finish(int workoutId, int day)
        {
            await _workout.UpdateDay(_selectedWorkoutId, day);
            _nav.NavigateTo("/dashboard");
        }
    }
}
