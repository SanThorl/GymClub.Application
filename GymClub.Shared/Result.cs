using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Shared;

public class Result<T>
{
    public bool Success { get; set; }
    public bool Error => !Success;

    public string Message { get; set; }

    public T? Data { get; set; }

    public static Result<T> DuplicateValidateResult(string message)
    {
        return new Result<T> { Success = false, Message = message };
    }
    public static Result<T> NoDataFoundResult(string message)
    {
        return new Result<T> { Success = false, Message = message };
    }
    public static Result<T> SuccessResult(T data, string message)
    {
        return new Result<T> { Success = true, Data = data, Message = message };
    }
    public static Result<T> SuccessResult(string message)
    {
        return new Result<T> { Success = true, Message = message };
    }
    public static Result<T> FailureResult(string message)
    {
        return new Result<T> { Success = false, Message = message };
    }
}
