namespace BookStore.Domain.Entities;

public sealed class Livro
{
    public int CodL { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Editora { get; set; } = string.Empty;
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; } = string.Empty;
    public List<LivroAutor> LivroAutores { get; set; } = new();
    public List<LivroAssunto> LivroAssuntos { get; set; } = new();
    public List<LivroValor> LivroValores { get; set; } = new();
}
