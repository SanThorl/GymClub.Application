using GymClub.Database.DbModels;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(AppDbContext db, ILogger<PaymentService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Result<PaymentResponseModel>> PayforWorkout(PaymentRequestModel reqModel)
        {
            PaymentResponseModel model = new PaymentResponseModel();
            try
            {

                var workoutItem = await _db.TblPayments.AsNoTracking()
                    .FirstOrDefaultAsync(x =>x.UserId==reqModel.CurrentUserId
                    && x.WorkoutId == reqModel.WorkoutId);
                if (workoutItem is null)
                {
                    return Result<PaymentResponseModel>.FailureResult("Payment is required to access the workout!");
                }

                return Result<PaymentResponseModel>.SuccessResult("Let's Upgrade your body!");
            }
            catch (Exception ex)
            {
               _logger.LogError(ex, ex.Message);
                return Result<PaymentResponseModel>.FailureResult(ex);
            }
       
        }
    }
}
