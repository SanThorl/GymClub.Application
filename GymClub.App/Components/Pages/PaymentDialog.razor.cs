using GymClub.App.Services;
using GymClub.Domain.Features.Payment;
using GymClub.Domain.Features.Workouts;
using MudBlazor;

namespace GymClub.App.Components.Pages;

public partial class PaymentDialog
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; }

    private PaymentRequestModel reqModel = new();
    private WorkoutModel _selectedWorkout;
    private void Cancel()
    {
        MudDialog.Cancel();
    }

    //private async Task SaveAsync()
    //{
    //    var response = await HttpClientService.ExecuteAsync<ProductCategoryResponseModel>(
    //        EndPoints.ProductCategory,
    //        EnumHttpMethod.Post,
    //        reqModel
    //    );

    //    if (response.IsError)
    //    {
    //        _injectService.ShowMessage(response.Message);
    //        return;
    //    }
    //    InjectService.ShowMessage(response.Message, EnumResponseType.Success);
    //    MudDialog.Close();
    //}
    private async Task SaveAsync()
    {
        var response = await _paymentServie.PayForWorkout(reqModel);
        if (!response.Success)
        {
            await _injectService.ShowErrorMessage(response.Message);
            return;
        }
        await _injectService.ShowSuccessMessage(response.Message);
        MudDialog.Close();
    }
}