# BookStore API

## Descrição
API REST desenvolvida em .NET 8 para gerenciamento de uma livraria, implementando operações CRUD para livros, autores e assuntos, além de geração de relatórios.

## Tecnologias Utilizadas

### Framework e Versão
- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - Para criação da API REST

### Arquitetura
- **Clean Architecture** - Separação em camadas (Domain, Application, Infrastructure, API)
- **Domain-Driven Design (DDD)** - Modelagem orientada ao domínio

### Banco de Dados
- **PostgreSQL** - Sistema de gerenciamento de banco de dados
- **Entity Framework Core 8.0** - ORM para acesso aos dados
- **Npgsql.EntityFrameworkCore.PostgreSQL** - Provider do PostgreSQL para EF Core

### Validação e Mapeamento
- **FluentValidation 11.9.0** - Validação de dados de entrada
- **AutoMapper 13.0.1** - Mapeamento entre objetos (DTOs e Entidades)

### Documentação
- **Swagger/OpenAPI** - Documentação interativa da API
- **Swashbuckle.AspNetCore** - Geração automática da documentação

### Geração de Relatórios
- **iTextSharp.LGPLv2.Core** - Geração de relatórios em PDF
- **EPPlus 7.0.0** - Geração de relatórios em Excel

### Testes
- **xUnit** - Framework de testes unitários
- **Moq** - Framework para criação de mocks
- **Entity Framework InMemory** - Banco em memória para testes

### Estrutura do Projeto

```
Api/
├── BookStore.Api/              # Camada de apresentação (Controllers, Middleware)
├── BookStore.Application/      # Camada de aplicação (Services, DTOs, Validations)
├── BookStore.Domain/          # Camada de domínio (Entities, Interfaces)
├── BookStore.Infrastructure/  # Camada de infraestrutura (Repositories, DbContext)
├── BookStore.Infrastructure.IoC/ # Injeção de dependência
└── BookStore.Test/           # Testes unitários
```

### Funcionalidades
- **CRUD de Autores** - Criação, leitura, atualização e exclusão
- **CRUD de Assuntos** - Gerenciamento de categorias de livros
- **CRUD de Livros** - Gerenciamento completo de livros com relacionamentos
- **Relatórios** - Geração de relatórios em PDF e Excel
- **Validação** - Validação robusta de dados de entrada
- **Tratamento de Erros** - Middleware personalizado para tratamento de exceções
- **CORS** - Configurado para permitir requisições do frontend

### Como Executar

1. **Pré-requisitos:**
   - .NET 8 SDK
   - PostgreSQL

2. **Configuração:**
   ```bash
   # Restaurar dependências
   dotnet restore
   
   # Aplicar migrations
   dotnet ef database update --project BookStore.Infrastructure --startup-project BookStore.Api
   ```

3. **Executar:**
   ```bash
   dotnet run --project BookStore.Api
   ```

4. **Acessar:**
   - API: `https://localhost:7000`
   - Swagger: `https://localhost:7000/swagger`

### Configurações
- **Connection String:** Configurada em `appsettings.json`
- **CORS:** Configurado para aceitar qualquer origem (desenvolvimento)
- **Swagger:** Habilitado apenas em ambiente de desenvolvimento