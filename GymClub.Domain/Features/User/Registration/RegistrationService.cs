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

        public async Task<Result<RegistrationResponseModel>> RegisterUser(RegistrationRequestModel reqModel)
        {
            Result<RegistrationResponseModel> model = new Result<RegistrationResponseModel>();
            try
            {
                TblUser? user = await _db.TblUsers.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserName.ToLower().Trim() == reqModel.UserName.ToLower().Trim() 
                    && x.PhoneNo == reqModel.PhoneNo);
                if (user is not null)
                {
                    //model.Response = new MessageResponseModel()
                    //{
                    //    IsSuccess = false,
                    //    Message = "User Already Exist!"
                    //};
                    //goto result;
                    return Result<RegistrationResponseModel>.FailureResult("User already exists.");
                }

                string hashedPassword = reqModel.Password.SHA256HexHashString(reqModel.UserName);
                var ulid = Ulid.NewUlid().ToString();

                TblUser newUser = new TblUser()
                {
                    UserId = ulid,
                    UserName = reqModel.UserName,
                    PhoneNo = reqModel.PhoneNo,
                    Password = hashedPassword,
                    Gender = reqModel.Gender,
                    DateOfBirth = reqModel.DateOfBirth,
                    JoinDate = DateTime.Today,
                    DelFlag = 0
                };

                //await _db.AddAsync(newUser);
                //await _db.SaveChangesAsync();
                //int result = _dapperService.Execute(SqlQueries.RegisterNewUser, new {
                //    ulid,
                //    reqModel.UserName,
                //    reqModel.PhoneNo,
                //    hashedPassword,
                //    reqModel.Gender,
                //    reqModel.DateOfBirth,
                //    DateTime.Today,
                //    DelFlag = 0
                //});
                return Result<RegistrationResponseModel>.SuccessResult("Registered successfully.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        result:
            return model;
        }
    }
}
