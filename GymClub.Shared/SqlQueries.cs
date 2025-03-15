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
                                            FROM Tbl_Workout
                                            WITH (NOLOCK)";

        public static string FinishedExercises = @"UPDATE TblExercises
                                                    SET IsDone = 1
                                                    WHERE WorkoutId = @workoutId AND Day = @day";

        public static string RegisterNewUser = @"INSERT INTO [dbo].[Tbl_User]
                                                   ([UserId]
                                                   ,[UserName]
                                                   ,[PhoneNo]
                                                   ,[Password]
                                                   ,[Gender]
                                                   ,[DateOfBirth]
                                                   ,[JoinDate]
                                                   ,[DelFlag])
     VALUES(@UserId,@UserName,@PhoneNo,@Password,@Gender,@DateOfBirth,@JoinDate,@DelFlag)";
    }
}
