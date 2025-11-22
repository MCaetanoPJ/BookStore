namespace BookStore.Domain.Entities;

public class VwRelatorioLivrosPorAutor
{
    public string NomeAutor { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Editora { get; set; } = string.Empty;
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; } = string.Empty;
    public string Assunto { get; set; } = string.Empty;
    public decimal? ValorBalcao { get; set; }
    public decimal? ValorInternet { get; set; }
    public decimal? ValorEvento { get; set; }
    public decimal? ValorSelfService { get; set; }
}