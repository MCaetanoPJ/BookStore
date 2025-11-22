using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Exceptions;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BookStore.Infrastructure.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly BookStoreDbContext _context;

    public LivroRepository(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Livro>> GetAllAsync()
    {
        return await _context.Livros
            .Include(l => l.LivroAutores).ThenInclude(la => la.Autor)
            .Include(l => l.LivroAssuntos).ThenInclude(la => la.Assunto)
            .Include(l => l.LivroValores).ThenInclude(lv => lv.TipoVenda)
            .ToListAsync();
    }

    public async Task<Livro?> GetByIdAsync(int id)
    {
        return await _context.Livros
            .Include(l => l.LivroAutores).ThenInclude(la => la.Autor)
            .Include(l => l.LivroAssuntos).ThenInclude(la => la.Assunto)
            .Include(l => l.LivroValores).ThenInclude(lv => lv.TipoVenda)
            .FirstOrDefaultAsync(l => l.CodL == id);
    }

    public async Task<IEnumerable<Livro>> SearchAsync(string termo)
    {
        return await _context.Livros
            .Include(l => l.LivroAutores).ThenInclude(la => la.Autor)
            .Include(l => l.LivroAssuntos).ThenInclude(la => la.Assunto)
            .Include(l => l.LivroValores).ThenInclude(lv => lv.TipoVenda)
            .Where(l => l.Titulo.Contains(termo) || l.Editora.Contains(termo))
            .ToListAsync();
    }

    public async Task<IEnumerable<Livro>> GetByAutorAsync(int autorId)
    {
        return await _context.Livros
            .Include(l => l.LivroAutores).ThenInclude(la => la.Autor)
            .Include(l => l.LivroAssuntos).ThenInclude(la => la.Assunto)
            .Include(l => l.LivroValores).ThenInclude(lv => lv.TipoVenda)
            .Where(l => l.LivroAutores.Any(la => la.Autor_CodAu == autorId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Livro>> GetByAssuntoAsync(int assuntoId)
    {
        return await _context.Livros
            .Include(l => l.LivroAutores).ThenInclude(la => la.Autor)
            .Include(l => l.LivroAssuntos).ThenInclude(la => la.Assunto)
            .Include(l => l.LivroValores).ThenInclude(lv => lv.TipoVenda)
            .Where(l => l.LivroAssuntos.Any(la => la.Assunto_CodAs == assuntoId))
            .ToListAsync();
    }

    public async Task<Livro> CreateAsync(Livro livro)
    {
        try
        {
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();
            return livro;
        }
        catch (PostgresException ex) when (ex.SqlState == "23503")
        {
            throw new BusinessRuleException("Autor ou Assunto informado não existe.");
        }
        catch (PostgresException ex) when (ex.SqlState == "23505")
        {
            throw new BusinessRuleException("Já existe um livro com essas informações.");
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseException("Erro ao salvar o livro", ex);
        }
    }

    public async Task<Livro> UpdateAsync(Livro livro)
    {
        try
        {
            var existingLivro = await _context.Livros
                .Include(l => l.LivroAutores)
                .Include(l => l.LivroAssuntos)
                .Include(l => l.LivroValores)
                .FirstOrDefaultAsync(l => l.CodL == livro.CodL);

            if (existingLivro == null)
                throw new EntityNotFoundException("Livro", livro.CodL);

            // Atualizar propriedades básicas
            _context.Entry(existingLivro).CurrentValues.SetValues(livro);
            
            // Remover relacionamentos antigos
            _context.RemoveRange(existingLivro.LivroAutores);
            _context.RemoveRange(existingLivro.LivroAssuntos);
            _context.RemoveRange(existingLivro.LivroValores);
            
            // Adicionar novos relacionamentos
            existingLivro.LivroAutores = livro.LivroAutores;
            existingLivro.LivroAssuntos = livro.LivroAssuntos;
            existingLivro.LivroValores = livro.LivroValores;
            
            await _context.SaveChangesAsync();
            return existingLivro;
        }
        catch (PostgresException ex) when (ex.SqlState == "23503")
        {
            throw new BusinessRuleException("Autor ou Assunto informado não existe.");
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseException("Erro ao atualizar o livro", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
                throw new EntityNotFoundException("Livro", id);

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
        }
        catch (PostgresException ex) when (ex.SqlState == "23503")
        {
            throw new BusinessRuleException("Não é possível excluir o livro pois ele possui relacionamentos.");
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseException("Erro ao excluir o livro", ex);
        }
    }
}