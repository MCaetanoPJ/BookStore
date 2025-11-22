using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces.Repositories;

public interface IAutorRepository
{
    Task<IEnumerable<Autor>> GetAllAsync();
    Task<Autor?> GetByIdAsync(int id);
    Task<IEnumerable<Autor>> SearchAsync(string termo);
    Task<Autor> CreateAsync(Autor autor);
    Task<Autor> UpdateAsync(Autor autor);
    Task DeleteAsync(int id);
}