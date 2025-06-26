namespace eItems.Catalog.Application.Common;

public class Result<T>
{
    public T? Value { get; }
    public bool IsSuccess { get; }
    public bool IsNotFound => !IsSuccess && Value == null;

    private Result(T? value, bool isSuccess)
    {
        Value = value;
        IsSuccess = isSuccess;
    }

    public static Result<T> Success(T value) => new(value, true);
    public static Result<T> NotFound() => new(default, false);

    public static implicit operator Result<T>(T value) => Success(value);
}