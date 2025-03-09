using GymClub.Database.DbModels;
using GymClub.Shared;
using Microsoft.Extensions.Logging;

namespace GymClub.Domain.Features.User.Registration
{
    public class RegistrationService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<RegistrationService> _logger;
        private readonly DapperService _dapperService;

        public RegistrationService(AppDbContext db, ILogger<RegistrationService> logger, DapperService dapperService)
        {
            _db = db;
            _logger = logger;
            _dapperService = dapperService;
        }

        public async Task<RegistrationResponseModel> RegisterUser(RegistrationRequestModel reqModel)
        {
            RegistrationResponseModel model = new RegistrationResponseModel();
            try
            {
                TblUser? user = await _db.TblUsers.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserName.ToLower().Trim() == reqModel.UserName.ToLower().Trim()
                    && x.PhoneNo == reqModel.PhoneNo);
                if (user is not null)
                {
                    model.Response = new MessageResponseModel()
                    {
                        IsSuccess = false,
                        Message = "User Already Exist!"
                    };
                    goto result;
                }

                string hashedPassword = reqModel.Password.SHA256HexHashString(reqModel.UserName);
                var ulid = Ulid.NewUlid().ToString();
                
                int result = _dapperService.Execute(SqlQueries.RegisterNewUser, new
                {
                    UserId = ulid,
                    UserName = reqModel.UserName,
                    PhoneNo = reqModel.PhoneNo,
                    Password = hashedPassword,
                    Gender = reqModel.Gender,
                    DateOfBirth = reqModel.DateOfBirth,
                    JoinDate = DateTime.Today,
                    DelFlag = 0
                });
                if (result > 0)
                {
                    model.Response = new MessageResponseModel()
                    {
                        IsSuccess = true,
                        Message = "Registered Successfully!"
                    };
                }
                else
                {
                    model.Response = new MessageResponseModel()
                    {
                        IsSuccess = false,
                        Message = "Registration Failed!"
                    };
                }
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
