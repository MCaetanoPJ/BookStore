namespace BookStore.Domain.Entities;

public sealed class Autor
{
    public int CodAu { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<LivroAutor> LivroAutores { get; set; } = new();
}
