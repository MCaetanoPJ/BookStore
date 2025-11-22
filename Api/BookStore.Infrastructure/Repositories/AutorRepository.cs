using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Exceptions;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BookStore.Infrastructure.Repositories;

public class AutorRepository : IAutorRepository
{
    private readonly BookStoreDbContext _context;

    public AutorRepository(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Autor>> GetAllAsync()
    {
        return await _context.Autores.ToListAsync();
    }

    public async Task<Autor?> GetByIdAsync(int id)
    {
        return await _context.Autores.FindAsync(id);
    }

    public async Task<IEnumerable<Autor>> SearchAsync(string termo)
    {
        return await _context.Autores
            .Where(a => a.Nome.Contains(termo))
            .ToListAsync();
    }

    public async Task<Autor> CreateAsync(Autor autor)
    {
        try
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
            return autor;
        }
        catch (PostgresException ex) when (ex.SqlState == "23505")
        {
            throw new BusinessRuleException("Já existe um autor com esse nome.");
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseException("Erro ao salvar o autor", ex);
        }
    }

    public async Task<Autor> UpdateAsync(Autor autor)
    {
        try
        {
            var existingAutor = await _context.Autores.FindAsync(autor.CodAu);
            if (existingAutor == null)
                throw new EntityNotFoundException("Autor", autor.CodAu);

            _context.Entry(existingAutor).CurrentValues.SetValues(autor);
            await _context.SaveChangesAsync();
            return existingAutor;
        }
        catch (PostgresException ex) when (ex.SqlState == "23505")
        {
            throw new BusinessRuleException("Já existe um autor com esse nome.");
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseException("Erro ao atualizar o autor", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null)
                throw new EntityNotFoundException("Autor", id);

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
        }
        catch (PostgresException ex) when (ex.SqlState == "23503")
        {
            throw new BusinessRuleException("Não é possível excluir o autor pois ele possui livros associados.");
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseException("Erro ao excluir o autor", ex);
        }
    }
}