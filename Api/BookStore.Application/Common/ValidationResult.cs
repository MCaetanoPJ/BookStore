namespace BookStore.Application.Common;

public class ValidationResult<T>
{
    public bool IsValid { get; set; }
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();

    public static ValidationResult<T> Success(T data) => new() { IsValid = true, Data = data };
    public static ValidationResult<T> Failure(params string[] errors) => new() { IsValid = false, Errors = errors.ToList() };
}