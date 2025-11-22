using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Interfaces.Services;
using BookStore.Application.Mappings;
using BookStore.Application.Services;
using BookStore.Application.Validations;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BookStore.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<BookStoreDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<ILivroRepository, LivroRepository>();
        services.AddScoped<IAutorRepository, AutorRepository>();
        services.AddScoped<IAssuntoRepository, AssuntoRepository>();
        services.AddScoped<IRelatorioRepository, RelatorioRepository>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Services
        services.AddScoped<ILivroService, LivroService>();
        services.AddScoped<IAutorService, AutorService>();
        services.AddScoped<IAssuntoService, AssuntoService>();
        services.AddScoped<IRelatorioService, RelatorioService>();
        services.AddAutoMapper(typeof(BookStoreMappingProfile));
        services.AddValidatorsFromAssemblyContaining<LivroValidator>();

        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BookStore API",
                Version = "v1",
                Description = "Desafio TJRJ - Sistema de Cadastro de Livros",
                Contact = new OpenApiContact
                {
                    Name = "BookStore",
                    Email = string.Empty
                }
            });
        });

        return services;
    }
}
