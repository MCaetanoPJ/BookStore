using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Interfaces.Services;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services;

public class AutorService : IAutorService
{
    private readonly IAutorRepository _autorRepository;
    private readonly IMapper _mapper;

    public AutorService(IAutorRepository autorRepository, IMapper mapper)
    {
        _autorRepository = autorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AutorDTO>> GetAllAsync()
    {
        var autores = await _autorRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AutorDTO>>(autores);
    }

    public async Task<AutorDTO?> GetByIdAsync(int id)
    {
        try
        {
            var autor = await _autorRepository.GetByIdAsync(id);
            return autor != null ? _mapper.Map<AutorDTO>(autor) : null;
        }
        catch (Exception ex) when (ex.GetType().Name.Contains("Sql"))
        {
            throw new DatabaseException("Erro ao buscar autor", ex);
        }
    }

    public async Task<IEnumerable<AutorDTO>> SearchAsync(string termo)
    {
        var autores = await _autorRepository.SearchAsync(termo);
        return _mapper.Map<IEnumerable<AutorDTO>>(autores);
    }

    public async Task<AutorDTO> CreateAsync(CreateAutorDTO createAutorDto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(createAutorDto.Nome))
                throw new BusinessRuleException("Nome do autor é obrigatório");
                
            var autor = _mapper.Map<Autor>(createAutorDto);
            var createdAutor = await _autorRepository.CreateAsync(autor);
            return _mapper.Map<AutorDTO>(createdAutor);
        }
        catch (Exception ex) when (ex.GetType().Name.Contains("Sql"))
        {
            throw new DatabaseException("Erro ao criar autor", ex);
        }
    }

    public async Task<AutorDTO> UpdateAsync(int id, CreateAutorDTO updateAutorDto)
    {
        var autor = _mapper.Map<Autor>(updateAutorDto);
        autor.CodAu = id;
        var updatedAutor = await _autorRepository.UpdateAsync(autor);
        return _mapper.Map<AutorDTO>(updatedAutor);
    }

    public async Task DeleteAsync(int id)
    {
        await _autorRepository.DeleteAsync(id);
    }
}