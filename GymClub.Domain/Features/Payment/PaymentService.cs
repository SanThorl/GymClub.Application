using GymClub.Database.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Features.Payment
{
    public class PaymentService
    {
        private readonly AppDbContext _db;

        public PaymentService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<PaymentResponseModel> PayforWorkout(PaymentRequestModel reqModel)
        {
            PaymentResponseModel model = new PaymentResponseModel();
            try
            {

                var workoutItem = await _db.TblPayments.AsNoTracking()
                    .FirstOrDefaultAsync(x =>x.UserId==reqModel.CurrentUserId
                    && x.WorkoutId == reqModel.WorkoutId);
                if (workoutItem is null)
                {
                    model.Response = new MessageResponseModel
                    {
                        IsSuccess = false,
                        Message = "Payment is required!"
                    };
                    goto result;
                }

                model.Response = new MessageResponseModel
                {
                    IsSuccess = true,
                    Message = "Let's Update your Body!"
                };
                goto result;
            }
            catch (Exception ex)
            {
                model.Response = new MessageResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        result:
            return model;
        }
    }
}
