using System;
using System.Collections.Generic;

namespace GymClub.Database.DbModels;

public partial class TblPayment
{
    public int PaymentId { get; set; }

    public int? WorkoutId { get; set; }

    public string? WorkoutName { get; set; }

    public decimal? Amount { get; set; }

    public string? UserId { get; set; }

    public DateTime? PayDate { get; set; }
}
