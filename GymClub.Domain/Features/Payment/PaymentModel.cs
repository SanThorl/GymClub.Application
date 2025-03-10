using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Features.Payment
{
    public class PaymentModel
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int WorkoutId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public string PaymentStatus { get; set; }
    }

    public class PaymentRequestModel : BaseRequestModel
    {
        public int WorkoutId { get; set; }
    }
    public class PaymentResponseModel
    {
        public List<PaymentModel> lstData { get; set; }
        public MessageResponseModel Response { get; set; }
        public int totalRowCount { get; set; }
    }
}
