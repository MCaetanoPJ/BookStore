using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces.Services;

public interface IAutorService
{
    Task<IEnumerable<AutorDTO>> GetAllAsync();
    Task<AutorDTO?> GetByIdAsync(int id);
    Task<IEnumerable<AutorDTO>> SearchAsync(string termo);
    Task<AutorDTO> CreateAsync(CreateAutorDTO createAutorDto);
    Task<AutorDTO> UpdateAsync(int id, CreateAutorDTO updateAutorDto);
    Task DeleteAsync(int id);
}