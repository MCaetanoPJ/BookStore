using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces.Repositories;

public interface IAssuntoRepository
{
    Task<IEnumerable<Assunto>> GetAllAsync();
    Task<Assunto?> GetByIdAsync(int id);
    Task<IEnumerable<Assunto>> SearchAsync(string termo);
    Task<Assunto> CreateAsync(Assunto assunto);
    Task<Assunto> UpdateAsync(Assunto assunto);
    Task DeleteAsync(int id);
}