using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Interfaces.Services;
using BookStore.Domain.Entities;
using BookStore.Application.Common;

namespace BookStore.Application.Services;

public class LivroService : ILivroService
{
    private readonly ILivroRepository _livroRepository;
    private readonly IMapper _mapper;

    public LivroService(ILivroRepository livroRepository, IMapper mapper)
    {
        _livroRepository = livroRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LivroDTO>> GetAllAsync()
    {
        var livros = await _livroRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<LivroDTO>>(livros);
    }

    public async Task<LivroDTO?> GetByIdAsync(int id)
    {
        var livro = await _livroRepository.GetByIdAsync(id);
        return livro != null ? _mapper.Map<LivroDTO>(livro) : null;
    }

    public async Task<IEnumerable<LivroDTO>> SearchAsync(string termo)
    {
        var livros = await _livroRepository.SearchAsync(termo);
        return _mapper.Map<IEnumerable<LivroDTO>>(livros);
    }

    public async Task<IEnumerable<LivroDTO>> GetByAutorAsync(int autorId)
    {
        var livros = await _livroRepository.GetByAutorAsync(autorId);
        return _mapper.Map<IEnumerable<LivroDTO>>(livros);
    }

    public async Task<IEnumerable<LivroDTO>> GetByAssuntoAsync(int assuntoId)
    {
        var livros = await _livroRepository.GetByAssuntoAsync(assuntoId);
        return _mapper.Map<IEnumerable<LivroDTO>>(livros);
    }

    public async Task<ValidationResult<LivroDTO>> CreateAsync(CreateLivroDTO createLivroDto)
    {
        if (int.TryParse(createLivroDto.AnoPublicacao, out int ano) && ano > DateTime.Now.Year)
            return ValidationResult<LivroDTO>.Failure("Ano de publicação não pode ser maior que o ano atual");
            
        var livro = _mapper.Map<Livro>(createLivroDto);
        
        // Criar relacionamentos com autores
        livro.LivroAutores = createLivroDto.AutoresIds.Select(autorId => new LivroAutor
        {
            Autor_CodAu = autorId
        }).ToList();
        
        // Criar relacionamentos com assuntos
        livro.LivroAssuntos = createLivroDto.AssuntosIds.Select(assuntoId => new LivroAssunto
        {
            Assunto_CodAs = assuntoId
        }).ToList();
        
        // Criar valores por tipo de venda
        livro.LivroValores = createLivroDto.Valores.Select(valor => new LivroValor
        {
            TipoVenda_CodTv = valor.TipoVendaId,
            Valor = valor.Valor
        }).ToList();
        
        var createdLivro = await _livroRepository.CreateAsync(livro);
        return ValidationResult<LivroDTO>.Success(_mapper.Map<LivroDTO>(createdLivro));
    }

    public async Task<ValidationResult<LivroDTO>> UpdateAsync(int id, CreateLivroDTO updateLivroDto)
    {
        if (int.TryParse(updateLivroDto.AnoPublicacao, out int ano) && ano > DateTime.Now.Year)
            return ValidationResult<LivroDTO>.Failure("Ano de publicação não pode ser maior que o ano atual");
            
        var livro = _mapper.Map<Livro>(updateLivroDto);
        livro.CodL = id;
        
        // Criar relacionamentos com autores
        livro.LivroAutores = updateLivroDto.AutoresIds.Select(autorId => new LivroAutor
        {
            Livro_CodL = id,
            Autor_CodAu = autorId
        }).ToList();
        
        // Criar relacionamentos com assuntos
        livro.LivroAssuntos = updateLivroDto.AssuntosIds.Select(assuntoId => new LivroAssunto
        {
            Livro_CodL = id,
            Assunto_CodAs = assuntoId
        }).ToList();
        
        // Criar valores por tipo de venda
        livro.LivroValores = updateLivroDto.Valores.Select(valor => new LivroValor
        {
            Livro_CodL = id,
            TipoVenda_CodTv = valor.TipoVendaId,
            Valor = valor.Valor
        }).ToList();
        
        var updatedLivro = await _livroRepository.UpdateAsync(livro);
        return ValidationResult<LivroDTO>.Success(_mapper.Map<LivroDTO>(updatedLivro));
    }

    public async Task DeleteAsync(int id)
    {
        await _livroRepository.DeleteAsync(id);
    }
}