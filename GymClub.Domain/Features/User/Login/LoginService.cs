using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymClub.Database.DbModels;

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
        TblUser? item = await _db.TblUsers.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == reqModel.UserName
        && x.Password == reqModel.Password);

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
    result:
        return model;
    }
}
