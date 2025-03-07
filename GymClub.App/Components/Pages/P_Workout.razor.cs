﻿using GymClub.Domain.Features.Workouts;
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
        private int _selectedDay;
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
            lstExercise = model.ExerciseList;
            await _injectService.DisableLoading();
            _formType = EnumFormType.DayList;
        }

        private async Task ShowExercises(int day)
        {
            _selectedDay = day;
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

        private async Task Finish(int day)
        {
            _formType = EnumFormType.ExerciseList;
            await ShowExercises(day);
        }
    }
}
