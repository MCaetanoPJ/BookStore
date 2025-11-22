using BookStore.Application.DTOs;
using BookStore.Application.Interfaces.Services;
using BookStore.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatorioController : ControllerBase
{
    private readonly IRelatorioService _relatorioService;

    public RelatorioController(IRelatorioService relatorioService)
    {
        _relatorioService = relatorioService;
    }

    [HttpGet("livros-por-autor")]
    public async Task<ActionResult<ApiResponse<IEnumerable<RelatorioLivrosPorAutorDTO>>>> GetRelatorioLivrosPorAutor()
    {
        var dados = await _relatorioService.GetRelatorioLivrosPorAutorAsync();
        return Ok(ApiResponse<IEnumerable<RelatorioLivrosPorAutorDTO>>.Ok(dados));
    }

    [HttpGet("livros-por-autor/pdf")]
    public async Task<IActionResult> ExportarPdf()
    {
        try
        {
            var pdfBytes = await _relatorioService.ExportarParaPdfAsync();
            return File(pdfBytes, "application/pdf", $"relatorio-livros-por-autor-{DateTime.Now:yyyyMMdd}.pdf");
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<object>.BadRequest(new List<string> { ex.Message }, "Erro ao gerar PDF"));
        }
    }
}
