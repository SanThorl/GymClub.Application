using GymClub.App.Services;
using GymClub.Domain.Features.Payment;
using GymClub.Domain.Features.Workouts;
using MudBlazor;

namespace GymClub.App.Components.Pages;

public partial class PaymentDialog
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; }
    [CascadingParameter] public WorkoutModel reqModel { get; set; }
    //private WorkoutModel reqModel;
    private PaymentRequestModel requestModel = new();
    private void Cancel()
    {
        MudDialog.Cancel();
    }
    private async Task SaveAsync()
    {
        requestModel.CurrentUserId = reqModel.CurrentUserId;
        requestModel.WorkoutCode = reqModel.WorkoutCode;
        requestModel.Amount = reqModel.Price;
        var response = await _paymentServie.PayForWorkout(requestModel);
        if (!response.Success)
        {
            await _injectService.ShowErrorMessage(response.Message);
            return;
        }
        await _injectService.ShowSuccessMessage(response.Message);
        MudDialog.Close();
    }
}