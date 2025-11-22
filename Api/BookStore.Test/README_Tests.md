# Documentação dos Testes Unitários - BookStore

### Tecnologias Utilizadas
- **xUnit**: Framework de testes principal
- **Moq**: Biblioteca para criação de mocks
- **AutoMapper**: Para testes de mapeamento entre DTOs e entidades

## Executando os Testes

### Via CLI
```bash
# Executar todos os testes
dotnet test

# Executar testes específicos
dotnet test --filter "LivroServiceTests"

# Com cobertura de código
dotnet test --collect:"XPlat Code Coverage"
```
