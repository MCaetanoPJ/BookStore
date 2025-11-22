using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Mappings;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using Moq;
using Xunit;

namespace BookStore.Test.UnitTests;

public class AutorServiceTests
{
    private readonly Mock<IAutorRepository> _mockRepository;
    private readonly IMapper _mapper;
    private readonly AutorService _service;

    public AutorServiceTests()
    {
        _mockRepository = new Mock<IAutorRepository>();
        
        var config = new MapperConfiguration(cfg => cfg.AddProfile<BookStoreMappingProfile>());
        _mapper = config.CreateMapper();
        
        _service = new AutorService(_mockRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllAutores()
    {
        // Arrange
        var autores = new List<Autor>
        {
            new() { CodAu = 1, Nome = "Autor 1" },
            new() { CodAu = 2, Nome = "Autor 2" }
        };
        
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(autores);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnAutor()
    {
        // Arrange
        var autor = new Autor { CodAu = 1, Nome = "Autor Teste" };
        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(autor);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.CodAu);
        Assert.Equal("Autor Teste", result.Nome);
        _mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Autor?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(r => r.GetByIdAsync(999), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WithValidData_ShouldCreateAutor()
    {
        // Arrange
        var createAutorDto = new CreateAutorDTO { Nome = "Novo Autor" };
        var autor = new Autor { CodAu = 1, Nome = "Novo Autor" };
        
        _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Autor>())).ReturnsAsync(autor);

        // Act
        var result = await _service.CreateAsync(createAutorDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Novo Autor", result.Nome);
        _mockRepository.Verify(r => r.CreateAsync(It.IsAny<Autor>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithValidData_ShouldUpdateAutor()
    {
        // Arrange
        var updateAutorDto = new CreateAutorDTO { Nome = "Autor Atualizado" };
        var autor = new Autor { CodAu = 1, Nome = "Autor Atualizado" };
        
        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Autor>())).ReturnsAsync(autor);

        // Act
        var result = await _service.UpdateAsync(1, updateAutorDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.CodAu);
        Assert.Equal("Autor Atualizado", result.Nome);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Autor>()), Times.Once);
    }

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

    [Fact]
    public async Task SearchAsync_WithTerm_ShouldReturnMatchingAutores()
    {
        // Arrange
        var autores = new List<Autor>
        {
            new() { CodAu = 1, Nome = "João Silva" },
            new() { CodAu = 2, Nome = "João Santos" }
        };
        
        _mockRepository.Setup(r => r.SearchAsync("João")).ReturnsAsync(autores);

        // Act
        var result = await _service.SearchAsync("João");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, a => Assert.Contains("João", a.Nome));
        _mockRepository.Verify(r => r.SearchAsync("João"), Times.Once);
    }
}