using GymClub.Database.DbModels;
using GymClub.Shared;

namespace GymClub.Domain.Features.User.Registration
{
    public class RegistrationService
    {
        private readonly AppDbContext _db;
        public RegistrationService(AppDbContext db)
        {
            _db = db;
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

                TblUser newUser = new TblUser()
                {
                    UserId = ulid,
                    UserName = reqModel.UserName,
                    PhoneNo = reqModel.PhoneNo,
                    Password = hashedPassword,
                    Gender = reqModel.Gender,
                    JoinDate = DateTime.Today
                };

                await _db.AddAsync(newUser);
                await _db.SaveChangesAsync();
                model.Response = new MessageResponseModel()
                {
                    IsSuccess = true,
                    Message = "Registered Successfully!"
                };
            }
            catch(Exception ex)
            {
                model.Response.Message = ex.ToString();
            }
        result:
            return model;
        }
    }
}
