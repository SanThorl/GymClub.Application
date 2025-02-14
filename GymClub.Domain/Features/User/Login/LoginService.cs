using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymClub.Database.DbModels;

namespace GymClub.Domain.Features.User.Login
{
    public class LoginService
    {
        private readonly AppDbContext _db;

        public LoginService(AppDbContext db)
        {
            _db = db;
        }

        //public async Task<Login>
    }
}
