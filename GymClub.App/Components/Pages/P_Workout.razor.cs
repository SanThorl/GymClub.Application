using GymClub.Domain.Features.Payment;
using GymClub.Domain.Features.Workouts;
using GymClub.Domain.Models;
using GymClub.Shared;
using GymClub.Shared.Enum;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Diagnostics.Metrics;

namespace GymClub.App.Components.Pages
{
    public partial class P_Workout
    {
        private UserSessionModel _userSession;
        private Result<WorkoutResponseModel> model;
        private List<WorkoutModel> lst;
        //private List<ExerciseModel> lstExercise;
        private Result<ExerciseResponseModel> resModel;
        private List<ExerciseModel> eListForEachDay = new();
        private EnumFormType _formType = EnumFormType.WorkoutList;
        private IList<WorkoutModel> _selectedWorkout = new List<WorkoutModel>();
        private int _selectedDay;
        private int _totalDays;
        private string _selectedWorkoutId;
        private WorkoutModel data;
        private PaymentRequestModel _paymentRequestModel = new();
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

        async Task WorkoutCollection(string workoutCode)
        {
            try
            {
                await _injectService.EnableLoading();
                model = await _workout.GetWorkoutById(workoutCode);
                _selectedWorkoutId = workoutCode;
                //lstExercise = model.Data!.ExerciseList;
                _totalDays = model.Data.TotalDays;
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
                //_selectedDay = day;
                //if(_selectedDay >2 )
                //{
                //    _paymentRequestModel.CurrentUserId = _userSession.UserId;
                //    _paymentRequestModel.WorkoutCode = _selectedWorkoutId;
                //    var response = await _paymentServie.IsPaid(_paymentRequestModel);
                //    if (!response)
                //    {
                //        var dialog = await _dialogService.ShowAsync<PaymentDialog>("Payment is Requied for this Workout");
                //        var result = await dialog.Result;

                //        if (result!.Canceled)
                //        {
                //            await List();
                //        }
                //    }
                //}
                await _injectService.EnableLoading();
                resModel = await _workout.GetExerciseListForEachDay(_selectedWorkoutId, day);
                eListForEachDay = resModel.Data.lstExercise; 

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
            StateHasChanged();
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

        private async Task Finish(string workoutCode, int day)
        {
            await _workout.UpdateDay(_selectedWorkoutId, day);
            _nav.NavigateTo("/dashboard");
        }
    }
}
