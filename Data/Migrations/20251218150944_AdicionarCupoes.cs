using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoPartsShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCupoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cupoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Codigo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PercentagemDesconto = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "TEXT", nullable: false),
                    LimiteUtilizacoes = table.Column<int>(type: "INTEGER", nullable: true),
                    UtilizacoesAtuais = table.Column<int>(type: "INTEGER", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupoes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cupoes");
        }
    }
}
