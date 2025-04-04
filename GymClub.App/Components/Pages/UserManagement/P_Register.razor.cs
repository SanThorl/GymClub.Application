﻿using GymClub.Shared;
using Microsoft.IdentityModel.Tokens;

namespace GymClub.App.Components.Pages.UserManagement
{
    public partial class P_Register
    {
        private RegistrationRequestModel _reqModel = new RegistrationRequestModel();
        private Result<RegistrationResponseModel> model;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _injectService.EnableLoading();

                StateHasChanged();
                await _injectService.DisableLoading();
            }
        }
        async Task SignUp()
        {
            if (_reqModel.UserName.IsNullOrEmpty())
            {
                await _injectService.ShowErrorMessage("User Name is Requied!");
                return;
            }
            if (_reqModel.Password.IsNullOrEmpty())
            {
                await _injectService.ShowErrorMessage("Use Password for your privacy!");
                return;
            }
            if (_reqModel.PhoneNo.IsNullOrEmpty())
            {
                await _injectService.ShowErrorMessage("Phone Number is Requied!");
                return;
            }
            if (_reqModel.Gender.IsNullOrEmpty())
            {
                await _injectService.ShowErrorMessage("Please Mention your Gender!");
                return;
            }

            model = await _registrationService.RegisterUser(_reqModel);
            if (!model.Success)
            {
                await _injectService.ShowErrorMessage(model.Message);
                return;
            }
            await _injectService.ShowSuccessMessage(model.Message);
            _nav.NavigateTo("/signIn");
            StateHasChanged();
        }

        private async Task TogglePasswordVisibility()
        {
            await _injectService.TogglePasswordVisibility();
        }
    }
}

