using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs;

public class CreateAssuntoDTO
{
    public string Descricao { get; set; } = string.Empty;
}
