using GymClub.Database.DbModels;
using GymClub.Domain.Features.User.Registration;
using GymClub.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Features.User.Profile
{
    public class ProfileService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ProfileService> _logger;
        private readonly DapperService _dapperService;

        public ProfileService(AppDbContext db,
            ILogger<ProfileService> logger,
            DapperService dapperService)
        {
            _db = db;
            _logger = logger;
            _dapperService = dapperService;
        }

        public async Task<RegistrationResponseModel> Profile(ProfileRequestModel reqUserId)
        {
            RegistrationResponseModel model = new RegistrationResponseModel();
            try
            {
                TblUser? user = await _db.TblUsers.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == reqUserId.UserId);
                if (user is null)
                {
                    model.Response = new MessageResponseModel()
                    {
                        IsSuccess = false,
                        Message = "User Not Found!"
                    };
                    goto result;
                }
                model.UserId = user.UserId;
                model.UserName = user.UserName;
                model.PhoneNo = user.PhoneNo;
                model.Gender = user.Gender;
                model.Response = new MessageResponseModel()
                {
                    IsSuccess = true,
                    Message = " "
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        result:
            return model;
        }
    }
}