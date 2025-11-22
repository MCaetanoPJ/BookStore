namespace BookStore.Domain.Entities;

public sealed class LivroValor
{
    public int Livro_CodL { get; set; }
    public int TipoVenda_CodTv { get; set; }
    public decimal Valor { get; set; }
    public Livro Livro { get; set; } = null!;
    public TipoVenda TipoVenda { get; set; } = null!;
}
