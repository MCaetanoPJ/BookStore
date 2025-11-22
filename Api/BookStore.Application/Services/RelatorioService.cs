using BookStore.Application.DTOs;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Interfaces.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;

namespace BookStore.Application.Services;

public class RelatorioService : IRelatorioService
{
    private readonly IRelatorioRepository _relatorioRepository;

    public RelatorioService(IRelatorioRepository relatorioRepository)
    {
        _relatorioRepository = relatorioRepository;
    }

    public async Task<IEnumerable<RelatorioLivrosPorAutorDTO>> GetRelatorioLivrosPorAutorAsync()
    {
        return await _relatorioRepository.GetRelatorioLivrosPorAutorAsync();
    }

    public async Task<byte[]> ExportarParaPdfAsync()
    {
        var dados = await GetRelatorioLivrosPorAutorAsync();
        var dadosAgrupados = dados.GroupBy(x => x.NomeAutor).OrderBy(g => g.Key);
        
        using var memoryStream = new MemoryStream();
        var document = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
        PdfWriter.GetInstance(document, memoryStream);
        
        document.Open();
        
        var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
        var title = new Paragraph("Relatório de Livros por Autor", titleFont)
        {
            Alignment = Element.ALIGN_CENTER,
            SpacingAfter = 20
        };
        document.Add(title);
        
        var authorFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
        var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
        var cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 8);
        
        foreach (var grupo in dadosAgrupados)
        {
            var authorTitle = new Paragraph($"Autor: {grupo.Key}", authorFont)
            {
                SpacingBefore = 15,
                SpacingAfter = 10
            };
            document.Add(authorTitle);
            
            // Tabela do livro pelo autur
            var table = new PdfPTable(6) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 30, 25, 10, 12, 18, 25 });
            
            // Cabeçalhos
            table.AddCell(new PdfPCell(new Phrase("Título", headerFont)) { BackgroundColor = BaseColor.LightGray });
            table.AddCell(new PdfPCell(new Phrase("Editora", headerFont)) { BackgroundColor = BaseColor.LightGray });
            table.AddCell(new PdfPCell(new Phrase("Edição", headerFont)) { BackgroundColor = BaseColor.LightGray });
            table.AddCell(new PdfPCell(new Phrase("Ano", headerFont)) { BackgroundColor = BaseColor.LightGray });
            table.AddCell(new PdfPCell(new Phrase("Assunto", headerFont)) { BackgroundColor = BaseColor.LightGray });
            table.AddCell(new PdfPCell(new Phrase("Valores", headerFont)) { BackgroundColor = BaseColor.LightGray });
            
            // Dados dos livros
            foreach (var item in grupo)
            {
                table.AddCell(new PdfPCell(new Phrase(item.Titulo, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(item.Editora, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(item.Edicao.ToString(), cellFont)));
                table.AddCell(new PdfPCell(new Phrase(item.AnoPublicacao, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(item.Assunto, cellFont)));
                
                var valores = new StringBuilder();
                if (item.ValorBalcao.HasValue) valores.AppendLine($"Balcão: R$ {item.ValorBalcao:F2}");
                if (item.ValorInternet.HasValue) valores.AppendLine($"Internet: R$ {item.ValorInternet:F2}");
                if (item.ValorEvento.HasValue) valores.AppendLine($"Evento: R$ {item.ValorEvento:F2}");
                if (item.ValorSelfService.HasValue) valores.AppendLine($"Self-Service: R$ {item.ValorSelfService:F2}");
                
                table.AddCell(new PdfPCell(new Phrase(valores.ToString().Trim(), cellFont)));
            }
            
            document.Add(table);
        }
        
        document.Close();
        return memoryStream.ToArray();
    }
}
