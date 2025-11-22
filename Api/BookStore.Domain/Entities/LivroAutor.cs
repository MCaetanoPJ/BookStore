namespace BookStore.Domain.Entities;

public sealed class LivroAutor
{
    public int Livro_CodL { get; set; }
    public int Autor_CodAu { get; set; }
    public Livro Livro { get; set; } = null!;
    public Autor Autor { get; set; } = null!;
}
