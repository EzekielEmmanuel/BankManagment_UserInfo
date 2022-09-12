namespace Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    public static Result Failure(Exception error)
    {
        return new Result(false, new[] {error.Message});
    }

    public static Result Failure(IEnumerable<Exception> errors)
    {
        return new Result(false, errors.Select(x => x.Message));
    }

    public static Result Failure(string error)
    {
        return new Result(false, new[] {error});
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}

public class Result<TValue> : Result
{
    internal Result(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
    {
    }

    internal Result(TValue value, bool succeeded, IEnumerable<string> errors) : this(succeeded, errors)
    {
        Value = value;
    }

    public TValue? Value { get; init; }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, Array.Empty<string>());
    }

    public new static Result<TValue> Failure(Exception error)
    {
        return new Result<TValue>(false, new[] {error.Message});
    }

    public new static Result<TValue> Failure(IEnumerable<Exception> errors)
    {
        return new Result<TValue>(false, errors.Select(x => x.Message));
    }

    public new static Result<TValue> Failure(string error)
    {
        return new Result<TValue>(false, new[] {error});
    }

    public new static Result<TValue> Failure(IEnumerable<string> errors)
    {
        return new Result<TValue>(false, errors);
    }
}