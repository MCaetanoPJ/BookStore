using BookStore.Application.Interfaces.Repositories;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;

public class AssuntoRepository : IAssuntoRepository
{
    private readonly BookStoreDbContext _context;

    public AssuntoRepository(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Assunto>> GetAllAsync()
    {
        return await _context.Assuntos.ToListAsync();
    }

    public async Task<Assunto?> GetByIdAsync(int id)
    {
        return await _context.Assuntos.FindAsync(id);
    }

    public async Task<IEnumerable<Assunto>> SearchAsync(string termo)
    {
        return await _context.Assuntos
            .Where(a => a.Descricao.Contains(termo))
            .ToListAsync();
    }

    public async Task<Assunto> CreateAsync(Assunto assunto)
    {
        _context.Assuntos.Add(assunto);
        await _context.SaveChangesAsync();
        return assunto;
    }

    public async Task<Assunto> UpdateAsync(Assunto assunto)
    {
        _context.Entry(assunto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return assunto;
    }

    public async Task DeleteAsync(int id)
    {
        var assunto = await _context.Assuntos.FindAsync(id);
        if (assunto != null)
        {
            _context.Assuntos.Remove(assunto);
            await _context.SaveChangesAsync();
        }
    }
}