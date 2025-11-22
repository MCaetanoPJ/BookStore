namespace BookStore.Domain.Entities;

public sealed class LivroAssunto
{
    public int Livro_CodL { get; set; }
    public int Assunto_CodAs { get; set; }
    public Livro Livro { get; set; } = null!;
    public Assunto Assunto { get; set; } = null!;
}
