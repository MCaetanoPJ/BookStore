using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

    public DbSet<Livro> Livros { get; set; }
    public DbSet<Autor> Autores { get; set; }
    public DbSet<Assunto> Assuntos { get; set; }
    public DbSet<TipoVenda> TiposVenda { get; set; }
    public DbSet<LivroAutor> LivroAutores { get; set; }
    public DbSet<LivroAssunto> LivroAssuntos { get; set; }
    public DbSet<LivroValor> LivroValores { get; set; }
    public DbSet<VwRelatorioLivrosPorAutor> VwRelatorioLivrosPorAutor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Livro>(entity =>
        {
            entity.HasKey(e => e.CodL);
            entity.Property(e => e.Titulo).HasMaxLength(40).IsRequired();
            entity.Property(e => e.Editora).HasMaxLength(40).IsRequired();
            entity.Property(e => e.AnoPublicacao).HasMaxLength(4).IsRequired();
        });

        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.CodAu);
            entity.Property(e => e.Nome).HasMaxLength(40).IsRequired();
        });

        modelBuilder.Entity<Assunto>(entity =>
        {
            entity.HasKey(e => e.CodAs);
            entity.Property(e => e.Descricao).HasMaxLength(20).IsRequired();
        });

        modelBuilder.Entity<TipoVenda>(entity =>
        {
            entity.HasKey(e => e.CodTv);
            entity.Property(e => e.Descricao).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<LivroAutor>(entity =>
        {
            entity.HasKey(e => new { e.Livro_CodL, e.Autor_CodAu });
            
            entity.HasOne(e => e.Livro)
                .WithMany(l => l.LivroAutores)
                .HasForeignKey(e => e.Livro_CodL);
                
            entity.HasOne(e => e.Autor)
                .WithMany(a => a.LivroAutores)
                .HasForeignKey(e => e.Autor_CodAu);
        });

        modelBuilder.Entity<LivroAssunto>(entity =>
        {
            entity.HasKey(e => new { e.Livro_CodL, e.Assunto_CodAs });
            
            entity.HasOne(e => e.Livro)
                .WithMany(l => l.LivroAssuntos)
                .HasForeignKey(e => e.Livro_CodL);
                
            entity.HasOne(e => e.Assunto)
                .WithMany(a => a.LivroAssuntos)
                .HasForeignKey(e => e.Assunto_CodAs);
        });

        modelBuilder.Entity<LivroValor>(entity =>
        {
            entity.HasKey(e => new { e.Livro_CodL, e.TipoVenda_CodTv });
            entity.Property(e => e.Valor).HasColumnType("numeric(18,2)");
            
            entity.HasOne(e => e.Livro)
                .WithMany(l => l.LivroValores)
                .HasForeignKey(e => e.Livro_CodL);
                
            entity.HasOne(e => e.TipoVenda)
                .WithMany(t => t.LivroValores)
                .HasForeignKey(e => e.TipoVenda_CodTv);
        });

        modelBuilder.Entity<VwRelatorioLivrosPorAutor>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("VwRelatorioLivrosPorAutor");
        });

        modelBuilder.Entity<TipoVenda>().HasData(
            new TipoVenda { CodTv = 1, Descricao = "Balcão" },
            new TipoVenda { CodTv = 2, Descricao = "Self-Service" },
            new TipoVenda { CodTv = 3, Descricao = "Internet" },
            new TipoVenda { CodTv = 4, Descricao = "Evento" }
        );
    }
}
