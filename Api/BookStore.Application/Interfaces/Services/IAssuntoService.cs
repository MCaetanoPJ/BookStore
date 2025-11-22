using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces.Services;

public interface IAssuntoService
{
    Task<IEnumerable<AssuntoDTO>> GetAllAsync();
    Task<AssuntoDTO?> GetByIdAsync(int id);
    Task<IEnumerable<AssuntoDTO>> SearchAsync(string termo);
    Task<AssuntoDTO> CreateAsync(CreateAssuntoDTO createAssuntoDto);
    Task<AssuntoDTO> UpdateAsync(int id, CreateAssuntoDTO updateAssuntoDto);
    Task DeleteAsync(int id);
}