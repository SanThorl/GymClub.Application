using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymClub.Database.DbModels;
using GymClub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            TblUser? user = await _db.TblUsers.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNo == reqModel.PhoneNo);
            if (user is not null)
            {
                model.Response = new MessageResponseModel()
                {
                    IsSuccess = false,
                    Message = "User Already Exist!"
                };
                goto result;
            }

            string randomId = Guid.NewGuid().ToString();

            TblUser newUser = new TblUser()
            {
                UserId = randomId,
                UserName = reqModel.UserName,
                PhoneNo = reqModel.PhoneNo,
                Password = reqModel.Password,
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
        result:
            return model;
        }
    }
}
