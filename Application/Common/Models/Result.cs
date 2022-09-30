namespace Application.Common.Models;

public class Result
{
    protected Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; }

    protected string[] Errors { get; }

    public TReturn Match<TReturn>(Func<TReturn> success, Func<string[], TReturn> fail)
    {
        return Succeeded ? success() : fail(Errors);
    }

    public void Match(Action success, Action<string[]> fail)
    {
        if (Succeeded)
            success();
        else
            fail(Errors);
    }

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

public sealed class Result<TValue> : Result
{
    private Result(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
    {
        Value = default!;
    }

    private Result(TValue value, bool succeeded, IEnumerable<string> errors) : this(succeeded, errors)
    {
        Value = value;
    }

    private TValue Value { get; }

    public TReturn Match<TReturn>(Func<TValue, TReturn> success, Func<string[], TReturn> fail)
    {
        return Succeeded ? success(Value) : fail(Errors);
    }

    public void Match(Action<TValue> success, Action<string[]> fail)
    {
        if (Succeeded)
            success(Value);
        else
            fail(Errors);
    }

    public static Result<TValue> Success(TValue value)
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