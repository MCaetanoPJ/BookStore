namespace BookStore.Application.DTOs;

public class CreateLivroDTO
{
    public string Titulo { get; set; } = string.Empty;
    public string Editora { get; set; } = string.Empty;
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; } = string.Empty;
    public List<int> AutoresIds { get; set; } = new();
    public List<int> AssuntosIds { get; set; } = new();
    public List<LivroValorDTO> Valores { get; set; } = new();
}
