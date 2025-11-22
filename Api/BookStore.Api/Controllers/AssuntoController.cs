using BookStore.Application.DTOs;
using BookStore.Application.Interfaces.Services;
using BookStore.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssuntoController : ControllerBase
{
    private readonly IAssuntoService _assuntoService;

    public AssuntoController(IAssuntoService assuntoService)
    {
        _assuntoService = assuntoService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<AssuntoDTO>>>> GetAll()
    {
        var assuntos = await _assuntoService.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<AssuntoDTO>>.Ok(assuntos));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<AssuntoDTO>>> GetById(int id)
    {
        var assunto = await _assuntoService.GetByIdAsync(id);
        if (assunto == null)
            return NotFound(ApiResponse<AssuntoDTO>.NotFound());

        return Ok(ApiResponse<AssuntoDTO>.Ok(assunto));
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<IEnumerable<AssuntoDTO>>>> Search([FromQuery] string termo)
    {
        var assuntos = await _assuntoService.SearchAsync(termo);
        return Ok(ApiResponse<IEnumerable<AssuntoDTO>>.Ok(assuntos));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<AssuntoDTO>>> Create([FromBody] CreateAssuntoDTO createAssuntoDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<AssuntoDTO>.BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));

        var assunto = await _assuntoService.CreateAsync(createAssuntoDto);
        return CreatedAtAction(nameof(GetById), new { id = assunto.CodAs }, ApiResponse<AssuntoDTO>.Created(assunto));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<AssuntoDTO>>> Update(int id, [FromBody] CreateAssuntoDTO updateAssuntoDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<AssuntoDTO>.BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));

        var assunto = await _assuntoService.UpdateAsync(id, updateAssuntoDto);
        return Ok(ApiResponse<AssuntoDTO>.Ok(assunto, "Assunto atualizado com sucesso"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _assuntoService.DeleteAsync(id);
        return NoContent();
    }
}
