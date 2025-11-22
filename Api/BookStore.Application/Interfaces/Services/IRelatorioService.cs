using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces.Services;

public interface IRelatorioService
{
    Task<IEnumerable<RelatorioLivrosPorAutorDTO>> GetRelatorioLivrosPorAutorAsync();
    Task<byte[]> ExportarParaPdfAsync();
}