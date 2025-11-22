using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Interfaces.Services;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services;

public class AssuntoService : IAssuntoService
{
    private readonly IAssuntoRepository _assuntoRepository;
    private readonly IMapper _mapper;

    public AssuntoService(IAssuntoRepository assuntoRepository, IMapper mapper)
    {
        _assuntoRepository = assuntoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AssuntoDTO>> GetAllAsync()
    {
        var assuntos = await _assuntoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AssuntoDTO>>(assuntos);
    }

    public async Task<AssuntoDTO?> GetByIdAsync(int id)
    {
        var assunto = await _assuntoRepository.GetByIdAsync(id);
        return assunto != null ? _mapper.Map<AssuntoDTO>(assunto) : null;
    }

    public async Task<IEnumerable<AssuntoDTO>> SearchAsync(string termo)
    {
        var assuntos = await _assuntoRepository.SearchAsync(termo);
        return _mapper.Map<IEnumerable<AssuntoDTO>>(assuntos);
    }

    public async Task<AssuntoDTO> CreateAsync(CreateAssuntoDTO createAssuntoDto)
    {
        var assunto = _mapper.Map<Assunto>(createAssuntoDto);
        var createdAssunto = await _assuntoRepository.CreateAsync(assunto);
        return _mapper.Map<AssuntoDTO>(createdAssunto);
    }

    public async Task<AssuntoDTO> UpdateAsync(int id, CreateAssuntoDTO updateAssuntoDto)
    {
        var assunto = _mapper.Map<Assunto>(updateAssuntoDto);
        assunto.CodAs = id;
        var updatedAssunto = await _assuntoRepository.UpdateAsync(assunto);
        return _mapper.Map<AssuntoDTO>(updatedAssunto);
    }

    public async Task DeleteAsync(int id)
    {
        await _assuntoRepository.DeleteAsync(id);
    }
}