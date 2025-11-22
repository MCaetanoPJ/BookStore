using BookStore.Application.DTOs;
using BookStore.Application.Interfaces.Services;
using BookStore.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutorController : ControllerBase
{
    private readonly IAutorService _autorService;

    public AutorController(IAutorService autorService)
    {
        _autorService = autorService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<AutorDTO>>>> GetAll()
    {
        var autores = await _autorService.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<AutorDTO>>.Ok(autores));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<AutorDTO>>> GetById(int id)
    {
        var autor = await _autorService.GetByIdAsync(id);
        if (autor == null)
            return NotFound(ApiResponse<AutorDTO>.NotFound());

        return Ok(ApiResponse<AutorDTO>.Ok(autor));
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<IEnumerable<AutorDTO>>>> Search([FromQuery] string termo)
    {
        var autores = await _autorService.SearchAsync(termo);
        return Ok(ApiResponse<IEnumerable<AutorDTO>>.Ok(autores));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<AutorDTO>>> Create([FromBody] CreateAutorDTO createAutorDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<AutorDTO>.BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));

        var autor = await _autorService.CreateAsync(createAutorDto);
        return CreatedAtAction(nameof(GetById), new { id = autor.CodAu }, ApiResponse<AutorDTO>.Created(autor));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<AutorDTO>>> Update(int id, [FromBody] CreateAutorDTO updateAutorDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<AutorDTO>.BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));

        var autor = await _autorService.UpdateAsync(id, updateAutorDto);
        return Ok(ApiResponse<AutorDTO>.Ok(autor, "Autor atualizado com sucesso"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _autorService.DeleteAsync(id);
        return NoContent();
    }
}
