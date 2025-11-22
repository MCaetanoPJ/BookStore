namespace BookStore.Application.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();

    public static ApiResponse<T> Ok(T data, string message = "Operação realizada com sucesso") =>
        new() { Success = true, Data = data, Message = message };

    public static ApiResponse<T> Created(T data, string message = "Recurso criado com sucesso") =>
        new() { Success = true, Data = data, Message = message };

    public static ApiResponse<T> BadRequest(List<string> errors, string message = "Dados inválidos") =>
        new() { Success = false, Errors = errors, Message = message };

    public static ApiResponse<T> NotFound(string message = "Recurso não encontrado") =>
        new() { Success = false, Message = message };
}