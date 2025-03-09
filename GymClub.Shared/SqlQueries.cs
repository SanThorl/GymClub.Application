using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Shared
{
    public static class SqlQueries
    {
        public static string WorkoutList = @"SELECT *
                                            FROM Tbl_Workout with (nolock)";
    }
}
