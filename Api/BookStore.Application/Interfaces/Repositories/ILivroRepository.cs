using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces.Repositories;

public interface ILivroRepository
{
    Task<IEnumerable<Livro>> GetAllAsync();
    Task<Livro?> GetByIdAsync(int id);
    Task<IEnumerable<Livro>> SearchAsync(string termo);
    Task<IEnumerable<Livro>> GetByAutorAsync(int autorId);
    Task<IEnumerable<Livro>> GetByAssuntoAsync(int assuntoId);
    Task<Livro> CreateAsync(Livro livro);
    Task<Livro> UpdateAsync(Livro livro);
    Task DeleteAsync(int id);
}