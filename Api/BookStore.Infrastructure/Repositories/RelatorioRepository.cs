using BookStore.Application.DTOs;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;

public class RelatorioRepository : IRelatorioRepository
{
    private readonly BookStoreDbContext _context;

    public RelatorioRepository(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RelatorioLivrosPorAutorDTO>> GetRelatorioLivrosPorAutorAsync()
    {
        var dados = await _context.VwRelatorioLivrosPorAutor.ToListAsync();
        
        var resultado = dados.Select(v => new RelatorioLivrosPorAutorDTO
        {
            NomeAutor = v.NomeAutor,
            Titulo = v.Titulo,
            Editora = v.Editora,
            Edicao = v.Edicao,
            AnoPublicacao = v.AnoPublicacao,
            Assunto = v.Assunto,
            ValorBalcao = v.ValorBalcao,
            ValorInternet = v.ValorInternet,
            ValorEvento = v.ValorEvento,
            ValorSelfService = v.ValorSelfService
        });

        return resultado;
    }
}