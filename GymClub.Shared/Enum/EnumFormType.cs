using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Shared.Enum
{
    public enum EnumFormType
    {
        WorkoutList,
        [Description("Update")]
        Edit,
        [Description("Detail")]
        Detail,
        ExerciseList,
        DayList
    }
}
