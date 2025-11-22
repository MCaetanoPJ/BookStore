namespace BookStore.Domain.Entities;

public sealed class TipoVenda
{
    public int CodTv { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public List<LivroValor> LivroValores { get; set; } = new();
}
