namespace BookStore.Domain.Entities;

public sealed class Assunto
{
    public int CodAs { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public List<LivroAssunto> LivroAssuntos { get; set; } = new();
}
