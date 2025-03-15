using GymClub.Database.DbModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
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

        //public async Task<Result<PaymentResponseModel>> PayforWorkout(PaymentRequestModel reqModel)
        //{
        //    PaymentResponseModel model = new PaymentResponseModel();
        //    try
        //    {

        //        var workoutItem = await _db.TblPayments.AsNoTracking()
        //            .FirstOrDefaultAsync(x =>x.UserId==reqModel.CurrentUserId
        //            && x.WorkoutId == reqModel.WorkoutId);
        //        if (workoutItem is null)
        //        {
        //            return Result<PaymentResponseModel>.FailureResult("Payment is required to access the workout!");
        //        }

        //        return Result<PaymentResponseModel>.SuccessResult("Let's Upgrade your body!");
        //    }
        //    catch (Exception ex)
        //    {
        //       _logger.LogError(ex, ex.Message);
        //        return Result<PaymentResponseModel>.FailureResult(ex);
        //    }

        //}

        public async Task<Result<PaymentRequestModel>> PayForWorkout(PaymentRequestModel reqModel)
        {
            try
            {
                var model = new PaymentResponseModel();
                var payment = new TblPayment
                {
                    UserId = reqModel.CurrentUserId,
                    WorkoutCode = reqModel.WorkoutCode,
                    Amount = reqModel.Amount,
                    PayDate = DateTime.Now
                };
                await _db.TblPayments.AddAsync(payment);
                await _db.SaveChangesAsync();
                return Result<PaymentRequestModel>.SuccessResult(reqModel, "Payment Successful!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<PaymentRequestModel>.FailureResult(ex);
            }
        }

        public async Task<bool> IsPaid(PaymentRequestModel reqModel)
        {
            var workoutItem = await _db.TblPayments.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == reqModel.CurrentUserId
                    && x.WorkoutCode == reqModel.WorkoutCode);
            return workoutItem is not null;
        }

    }
}
