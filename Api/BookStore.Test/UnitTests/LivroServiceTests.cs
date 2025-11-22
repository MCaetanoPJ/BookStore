using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Mappings;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using Moq;
using Xunit;

namespace BookStore.Test.UnitTests;

/// <summary>
/// Testes unitários para o LivroService
/// Valida operações CRUD, regras de negócio e mapeamentos
/// </summary>
public class LivroServiceTests
{
    private readonly Mock<ILivroRepository> _mockRepository;
    private readonly IMapper _mapper;
    private readonly LivroService _service;

    /// <summary>
    /// Configuração inicial dos mocks e dependências para cada teste
    /// </summary>
    public LivroServiceTests()
    {
        _mockRepository = new Mock<ILivroRepository>();
        
        var config = new MapperConfiguration(cfg => cfg.AddProfile<BookStoreMappingProfile>());
        _mapper = config.CreateMapper();
        
        _service = new LivroService(_mockRepository.Object, _mapper);
    }

    /// <summary>
    /// Testa se GetAllAsync retorna todos os livros do repositório
    /// </summary>
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllLivros()
    {
        // Arrange
        var livros = new List<Livro>
        {
            new() { CodL = 1, Titulo = "Livro 1", Editora = "Editora 1", Edicao = 1, AnoPublicacao = "2023" },
            new() { CodL = 2, Titulo = "Livro 2", Editora = "Editora 2", Edicao = 1, AnoPublicacao = "2024" }
        };
        
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(livros);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    /// <summary>
    /// Testa se GetAllAsync retorna lista vazia quando não há livros
    /// </summary>
    [Fact]
    public async Task GetAllAsync_WhenNoBooks_ShouldReturnEmptyList()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Livro>());

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    /// <summary>
    /// Testa se GetByIdAsync retorna o livro correto quando ID é válido
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnLivro()
    {
        // Arrange
        var livro = new Livro 
        { 
            CodL = 1, 
            Titulo = "Livro Teste", 
            Editora = "Editora Teste", 
            Edicao = 1, 
            AnoPublicacao = "2023" 
        };
        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(livro);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.CodL);
        Assert.Equal("Livro Teste", result.Titulo);
        _mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
    }

    /// <summary>
    /// Testa se GetByIdAsync retorna null quando livro não existe
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Livro?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(r => r.GetByIdAsync(999), Times.Once);
    }

    /// <summary>
    /// Testa se CreateAsync cria livro com dados válidos
    /// </summary>
    [Fact]
    public async Task CreateAsync_WithValidData_ShouldCreateLivro()
    {
        // Arrange
        var createLivroDto = new CreateLivroDTO 
        { 
            Titulo = "Novo Livro", 
            Editora = "Nova Editora", 
            Edicao = 1, 
            AnoPublicacao = "2023",
            AutoresIds = new List<int> { 1 },
            AssuntosIds = new List<int> { 1 },
            Valores = new List<LivroValorDTO> 
            {
                new() { TipoVendaId = 1, Valor = 25.90m }
            }
        };
        var livro = new Livro 
        { 
            CodL = 1, 
            Titulo = "Novo Livro", 
            Editora = "Nova Editora", 
            Edicao = 1, 
            AnoPublicacao = "2023" 
        };
        
        _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Livro>())).ReturnsAsync(livro);

        // Act
        var result = await _service.CreateAsync(createLivroDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsValid);
        Assert.Equal("Novo Livro", result.Data!.Titulo);
        _mockRepository.Verify(r => r.CreateAsync(It.IsAny<Livro>()), Times.Once);
    }

    /// <summary>
    /// Testa se CreateAsync falha quando ano de publicação é futuro
    /// </summary>
    [Fact]
    public async Task CreateAsync_WithFutureYear_ShouldReturnFailure()
    {
        // Arrange
        var futureYear = (DateTime.Now.Year + 1).ToString();
        var createLivroDto = new CreateLivroDTO 
        { 
            Titulo = "Livro Futuro", 
            Editora = "Editora", 
            Edicao = 1, 
            AnoPublicacao = futureYear,
            AutoresIds = new List<int> { 1 },
            AssuntosIds = new List<int> { 1 },
            Valores = new List<LivroValorDTO>()
        };

        // Act
        var result = await _service.CreateAsync(createLivroDto);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsValid);
        Assert.Contains("Ano de publicação não pode ser maior que o ano atual", result.Errors.First());
        _mockRepository.Verify(r => r.CreateAsync(It.IsAny<Livro>()), Times.Never);
    }

    /// <summary>
    /// Testa se SearchAsync retorna livros que correspondem ao termo de busca
    /// </summary>
    [Fact]
    public async Task SearchAsync_WithTerm_ShouldReturnMatchingLivros()
    {
        // Arrange
        var livros = new List<Livro>
        {
            new() { CodL = 1, Titulo = "Dom Casmurro", Editora = "Editora A", Edicao = 1, AnoPublicacao = "2020" }
        };
        
        _mockRepository.Setup(r => r.SearchAsync("Dom")).ReturnsAsync(livros);

        // Act
        var result = await _service.SearchAsync("Dom");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Contains("Dom", result.First().Titulo);
        _mockRepository.Verify(r => r.SearchAsync("Dom"), Times.Once);
    }

    /// <summary>
    /// Testa se SearchAsync retorna lista vazia quando nenhum livro corresponde
    /// </summary>
    [Fact]
    public async Task SearchAsync_WithNoMatches_ShouldReturnEmptyList()
    {
        // Arrange
        _mockRepository.Setup(r => r.SearchAsync("XYZ")).ReturnsAsync(new List<Livro>());

        // Act
        var result = await _service.SearchAsync("XYZ");

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockRepository.Verify(r => r.SearchAsync("XYZ"), Times.Once);
    }

    /// <summary>
    /// Testa se UpdateAsync atualiza livro com dados válidos
    /// </summary>
    [Fact]
    public async Task UpdateAsync_WithValidData_ShouldUpdateLivro()
    {
        // Arrange
        var updateLivroDto = new CreateLivroDTO 
        { 
            Titulo = "Livro Atualizado", 
            Editora = "Editora Atualizada", 
            Edicao = 2, 
            AnoPublicacao = "2023",
            AutoresIds = new List<int> { 1 },
            AssuntosIds = new List<int> { 1 },
            Valores = new List<LivroValorDTO>()
        };
        var livro = new Livro 
        { 
            CodL = 1, 
            Titulo = "Livro Atualizado", 
            Editora = "Editora Atualizada", 
            Edicao = 2, 
            AnoPublicacao = "2023" 
        };
        
        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Livro>())).ReturnsAsync(livro);

        // Act
        var result = await _service.UpdateAsync(1, updateLivroDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsValid);
        Assert.Equal("Livro Atualizado", result.Data!.Titulo);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Livro>()), Times.Once);
    }

    /// <summary>
    /// Testa se DeleteAsync chama o repositório corretamente
    /// </summary>
    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldCallRepository()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAsync(1);

        // Assert
        _mockRepository.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    /// <summary>
    /// Testa se GetByAutorAsync retorna livros do autor especificado
    /// </summary>
    [Fact]
    public async Task GetByAutorAsync_WithValidAutorId_ShouldReturnLivros()
    {
        // Arrange
        var livros = new List<Livro>
        {
            new() { CodL = 1, Titulo = "Livro do Autor", Editora = "Editora", Edicao = 1, AnoPublicacao = "2023" }
        };
        
        _mockRepository.Setup(r => r.GetByAutorAsync(1)).ReturnsAsync(livros);

        // Act
        var result = await _service.GetByAutorAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _mockRepository.Verify(r => r.GetByAutorAsync(1), Times.Once);
    }

    /// <summary>
    /// Testa se GetByAssuntoAsync retorna livros do assunto especificado
    /// </summary>
    [Fact]
    public async Task GetByAssuntoAsync_WithValidAssuntoId_ShouldReturnLivros()
    {
        // Arrange
        var livros = new List<Livro>
        {
            new() { CodL = 1, Titulo = "Livro do Assunto", Editora = "Editora", Edicao = 1, AnoPublicacao = "2023" }
        };
        
        _mockRepository.Setup(r => r.GetByAssuntoAsync(1)).ReturnsAsync(livros);

        // Act
        var result = await _service.GetByAssuntoAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _mockRepository.Verify(r => r.GetByAssuntoAsync(1), Times.Once);
    }
}
