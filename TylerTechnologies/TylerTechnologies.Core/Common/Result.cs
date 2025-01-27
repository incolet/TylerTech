namespace TylerTechnologies.Core.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }
    public IEnumerable<string> Errors { get; }

    protected Result(bool isSuccess, string error, IEnumerable<string> errors)
    {
        IsSuccess = isSuccess;
        Error = error;
        Errors = errors;
    }

    public static Result Success() => new Result(true, string.Empty, new List<string>());
    public static Result Failure(string error) => new Result(false, error, new List<string> { error });

    public static implicit operator Result(string error) => Failure(error);
    public static implicit operator Result(bool success) => success ? Success() : Failure(string.Empty);
}

public class Result<T> : Result
{
    public T Value { get; }

    private Result(T value, bool isSuccess, string error, IEnumerable<string> errors) 
        : base(isSuccess, error, errors)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new Result<T>(value, true, string.Empty, new List<string>());
    public new static Result<T> Failure(string error) => new Result<T>(default!, false, error, new List<string> { error });

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(string error) => Failure(error);
}