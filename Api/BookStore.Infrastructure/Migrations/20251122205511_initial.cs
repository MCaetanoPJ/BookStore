using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assuntos",
                columns: table => new
                {
                    CodAs = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assuntos", x => x.CodAs);
                });

            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    CodAu = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.CodAu);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    CodL = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Editora = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Edicao = table.Column<int>(type: "integer", nullable: false),
                    AnoPublicacao = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.CodL);
                });

            migrationBuilder.CreateTable(
                name: "TiposVenda",
                columns: table => new
                {
                    CodTv = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposVenda", x => x.CodTv);
                });

            migrationBuilder.CreateTable(
                name: "LivroAssuntos",
                columns: table => new
                {
                    Livro_CodL = table.Column<int>(type: "integer", nullable: false),
                    Assunto_CodAs = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroAssuntos", x => new { x.Livro_CodL, x.Assunto_CodAs });
                    table.ForeignKey(
                        name: "FK_LivroAssuntos_Assuntos_Assunto_CodAs",
                        column: x => x.Assunto_CodAs,
                        principalTable: "Assuntos",
                        principalColumn: "CodAs",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivroAssuntos_Livros_Livro_CodL",
                        column: x => x.Livro_CodL,
                        principalTable: "Livros",
                        principalColumn: "CodL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivroAutores",
                columns: table => new
                {
                    Livro_CodL = table.Column<int>(type: "integer", nullable: false),
                    Autor_CodAu = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroAutores", x => new { x.Livro_CodL, x.Autor_CodAu });
                    table.ForeignKey(
                        name: "FK_LivroAutores_Autores_Autor_CodAu",
                        column: x => x.Autor_CodAu,
                        principalTable: "Autores",
                        principalColumn: "CodAu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivroAutores_Livros_Livro_CodL",
                        column: x => x.Livro_CodL,
                        principalTable: "Livros",
                        principalColumn: "CodL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivroValores",
                columns: table => new
                {
                    Livro_CodL = table.Column<int>(type: "integer", nullable: false),
                    TipoVenda_CodTv = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroValores", x => new { x.Livro_CodL, x.TipoVenda_CodTv });
                    table.ForeignKey(
                        name: "FK_LivroValores_Livros_Livro_CodL",
                        column: x => x.Livro_CodL,
                        principalTable: "Livros",
                        principalColumn: "CodL",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivroValores_TiposVenda_TipoVenda_CodTv",
                        column: x => x.TipoVenda_CodTv,
                        principalTable: "TiposVenda",
                        principalColumn: "CodTv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TiposVenda",
                columns: new[] { "CodTv", "Descricao" },
                values: new object[,]
                {
                    { 1, "Balcão" },
                    { 2, "Self-Service" },
                    { 3, "Internet" },
                    { 4, "Evento" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LivroAssuntos_Assunto_CodAs",
                table: "LivroAssuntos",
                column: "Assunto_CodAs");

            migrationBuilder.CreateIndex(
                name: "IX_LivroAutores_Autor_CodAu",
                table: "LivroAutores",
                column: "Autor_CodAu");

            migrationBuilder.CreateIndex(
                name: "IX_LivroValores_TipoVenda_CodTv",
                table: "LivroValores",
                column: "TipoVenda_CodTv");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivroAssuntos");

            migrationBuilder.DropTable(
                name: "LivroAutores");

            migrationBuilder.DropTable(
                name: "LivroValores");

            migrationBuilder.DropTable(
                name: "Assuntos");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "TiposVenda");
        }
    }
}
