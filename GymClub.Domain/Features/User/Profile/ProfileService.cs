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

        public async Task<Result<ProfileResponseModel>> Profile(ProfileRequestModel reqUserId)
        {
            ProfileResponseModel model = new ProfileResponseModel();
            try
            {
                TblUser? user = await _db.TblUsers.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == reqUserId.UserId);
                if (user is null)
                {
                    return Result<ProfileResponseModel>.NoDataFoundResult("Sorry, No User is Found!");
                }
                model.UserId = user.UserId;
                model.UserName = user.UserName;
                model.PhoneNo = user.PhoneNo;
                model.ImagePath = user.ImagePath;
                if (System.IO.File.Exists(model.ImagePath))
                {
                    var imageBytes = System.IO.File.ReadAllBytes(model.ImagePath);
                    model.ImageStr = Convert.ToBase64String(imageBytes);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<ProfileResponseModel>.FailureResult(ex);
            }
            return Result<ProfileResponseModel>.SuccessResult(model, "Welcome back!");
        }

        public async Task<Result<ProfileResponseModel>> UpdateProfile(ProfileRequestModel reqModel)
        {
            ProfileResponseModel model = new ProfileResponseModel();
            try
            {
                TblUser? user = await _db.TblUsers.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == reqModel.UserId);
                if (user is null)
                {
                    return Result<ProfileResponseModel>.NoDataFoundResult("Sorry, No User is Found!");
                }

                #region Write Image File

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "D:\\GymClub_App\\profile");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fileName = $"{Guid.NewGuid()}{reqModel.ImageExtension}";
                var filePath = Path.Combine(folderPath, fileName);

                var fileData = Convert.FromBase64String(reqModel.ImageFile);
                await File.WriteAllBytesAsync(filePath, fileData);

                #endregion

                user.ImagePath = filePath;
                _db.Entry(user).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<ProfileResponseModel>.FailureResult(ex);
            }
            return Result<ProfileResponseModel>.SuccessResult(model, "Profile Updated Successfully!");
        }
    }
}