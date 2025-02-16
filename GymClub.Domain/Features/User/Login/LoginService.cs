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

    result:
        return model;
    }
}
