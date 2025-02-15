
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Models
{
    public class MessageResponseModel
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
