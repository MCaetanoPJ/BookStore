using BookStore.Application.DTOs;
using BookStore.Application.Common;

namespace BookStore.Application.Interfaces.Services;

public interface ILivroService
{
    Task<IEnumerable<LivroDTO>> GetAllAsync();
    Task<LivroDTO?> GetByIdAsync(int id);
    Task<IEnumerable<LivroDTO>> SearchAsync(string termo);
    Task<IEnumerable<LivroDTO>> GetByAutorAsync(int autorId);
    Task<IEnumerable<LivroDTO>> GetByAssuntoAsync(int assuntoId);
    Task<ValidationResult<LivroDTO>> CreateAsync(CreateLivroDTO createLivroDto);
    Task<ValidationResult<LivroDTO>> UpdateAsync(int id, CreateLivroDTO updateLivroDto);
    Task DeleteAsync(int id);
}