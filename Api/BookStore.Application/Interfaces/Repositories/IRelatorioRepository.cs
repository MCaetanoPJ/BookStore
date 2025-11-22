using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces.Repositories;

public interface IRelatorioRepository
{
    Task<IEnumerable<RelatorioLivrosPorAutorDTO>> GetRelatorioLivrosPorAutorAsync();
}