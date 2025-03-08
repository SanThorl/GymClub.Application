using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymClub.Database.DbModels;
using GymClub.Shared;

namespace GymClub.Domain.Features.User.Login;

public class LoginService
{
    private readonly AppDbContext _db;

    public LoginService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<LoginResponseModel> SignIn(LoginRequestModel reqModel)
    {
        LoginResponseModel model = new LoginResponseModel();
        try
        {
            string hashPassword = reqModel.Password.SHA256HexHashString(reqModel.UserName);
            TblUser? item = await _db.TblUsers.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == reqModel.UserName
                    && x.Password == hashPassword);

            if (item is null)
            {
                model.Response = new MessageResponseModel
                {
                    IsSuccess = false,
                    Message = "Please, fill the correct name and password"
                };
                goto result;
            }

            string sessionId = Guid.NewGuid().ToString();
            //TblLogin login = new TblLogin
            //{
            //    SessionId = sessionId,
            //    SessionExpiredDate = DateTime.UtcNow.AddHours(1),
            //    UserId = item.UserId,
            //    UserName = item.UserName
            //};

            //await _db.TblLogins.AddAsync(login);
            //await _db.SaveChangesAsync();

            //model.UserId = login.UserId;
            //model.SessionId = login.SessionId;
            model.UserName = item.UserName;
            model.Response = new MessageResponseModel
            {
                IsSuccess = true,
                Message = "Login Successfully!"
            };
        }
        catch(Exception ex)
        {
            model.Response = new MessageResponseModel
            {
                Message = ex.ToString()
            };
        }
        
    result:
        return model;
    }
}
