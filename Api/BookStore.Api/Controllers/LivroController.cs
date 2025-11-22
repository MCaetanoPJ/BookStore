using BookStore.Application.DTOs;
using BookStore.Application.Interfaces.Services;
using BookStore.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
    private readonly ILivroService _livroService;

    public LivroController(ILivroService livroService)
    {
        _livroService = livroService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<LivroDTO>>>> GetAll()
    {
        var livros = await _livroService.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<LivroDTO>>.Ok(livros));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<LivroDTO>>> GetById(int id)
    {
        var livro = await _livroService.GetByIdAsync(id);
        if (livro == null)
            return NotFound(ApiResponse<LivroDTO>.NotFound());

        return Ok(ApiResponse<LivroDTO>.Ok(livro));
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<IEnumerable<LivroDTO>>>> Search([FromQuery] string termo)
    {
        var livros = await _livroService.SearchAsync(termo);
        return Ok(ApiResponse<IEnumerable<LivroDTO>>.Ok(livros));
    }

    [HttpGet("autor/{autorId}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<LivroDTO>>>> GetByAutor(int autorId)
    {
        var livros = await _livroService.GetByAutorAsync(autorId);
        return Ok(ApiResponse<IEnumerable<LivroDTO>>.Ok(livros));
    }

    [HttpGet("assunto/{assuntoId}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<LivroDTO>>>> GetByAssunto(int assuntoId)
    {
        var livros = await _livroService.GetByAssuntoAsync(assuntoId);
        return Ok(ApiResponse<IEnumerable<LivroDTO>>.Ok(livros));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<LivroDTO>>> Create([FromBody] CreateLivroDTO createLivroDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<LivroDTO>.BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));

        var result = await _livroService.CreateAsync(createLivroDto);
        if (!result.IsValid)
            return BadRequest(ApiResponse<LivroDTO>.BadRequest(result.Errors));
            
        return CreatedAtAction(nameof(GetById), new { id = result.Data!.CodL }, ApiResponse<LivroDTO>.Created(result.Data));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<LivroDTO>>> Update(int id, [FromBody] CreateLivroDTO updateLivroDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<LivroDTO>.BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));

        var result = await _livroService.UpdateAsync(id, updateLivroDto);
        if (!result.IsValid)
            return BadRequest(ApiResponse<LivroDTO>.BadRequest(result.Errors));
            
        return Ok(ApiResponse<LivroDTO>.Ok(result.Data!, "Livro atualizado com sucesso"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _livroService.DeleteAsync(id);
        return NoContent();
    }
}
