namespace BookStore.Application.Exceptions;

public class BookStoreException : Exception
{
    public BookStoreException(string message) : base(message) { }
    public BookStoreException(string message, Exception innerException) : base(message, innerException) { }
}

public class EntityNotFoundException : BookStoreException
{
    public EntityNotFoundException(string entityName, int id) 
        : base($"{entityName} com ID {id} não foi encontrado.") { }
}

public class BusinessRuleException : BookStoreException
{
    public BusinessRuleException(string message) : base(message) { }
}

public class DatabaseException : BookStoreException
{
    public DatabaseException(string message, Exception innerException) 
        : base($"Erro no banco de dados: {message}", innerException) { }
}