using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymClub.Database.DbModels;
using GymClub.Shared;
using Microsoft.Extensions.Logging;

namespace GymClub.Domain.Features.User.Login;

public class LoginService
{
    private readonly AppDbContext _db;
    private readonly ILogger<LoginService> _logger;

    public LoginService(AppDbContext db, ILogger<LoginService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Result<LoginResponseModel>> SignIn(LoginRequestModel reqModel)
    {
        LoginResponseModel model = new LoginResponseModel();
        try
        {
            string hashPassword = reqModel.Password.SHA256HexHashString(reqModel.UserName);
            TblUser? item = await _db.TblUsers.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == reqModel.UserName
                    && x.Password == hashPassword);

            if (item is null)
            {
                return Result<LoginResponseModel>.FailureResult("Please, fill the correct name and password");
            }

            //string sessionId = Guid.NewGuid().ToString();
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
            model.UserId = item.UserId;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return Result<LoginResponseModel>.FailureResult(ex);
        }
        return Result<LoginResponseModel>.SuccessResult(model, "Welcome back!");
    }
}
