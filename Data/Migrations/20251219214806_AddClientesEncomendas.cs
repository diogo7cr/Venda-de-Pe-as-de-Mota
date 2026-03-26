using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoPartsShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClientesEncomendas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modelos_Marcas_MarcaId",
                table: "Modelos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pecas_Categorias_CategoriaId",
                table: "Pecas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pecas_Modelos_ModeloId",
                table: "Pecas");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Morada = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    NIF = table.Column<string>(type: "TEXT", maxLength: 9, nullable: false),
                    DataRegisto = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Encomendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataEncomenda = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Observacoes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encomendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encomendas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItensEncomenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EncomendaId = table.Column<int>(type: "INTEGER", nullable: false),
                    PecaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensEncomenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensEncomenda_Encomendas_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "Encomendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensEncomenda_Pecas_PecaId",
                        column: x => x.PecaId,
                        principalTable: "Pecas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pecas_Referencia",
                table: "Pecas",
                column: "Referencia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Email",
                table: "Clientes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_NIF",
                table: "Clientes",
                column: "NIF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Encomendas_ClienteId",
                table: "Encomendas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Encomendas_DataEncomenda",
                table: "Encomendas",
                column: "DataEncomenda");

            migrationBuilder.CreateIndex(
                name: "IX_Encomendas_Estado",
                table: "Encomendas",
                column: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_ItensEncomenda_EncomendaId",
                table: "ItensEncomenda",
                column: "EncomendaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensEncomenda_PecaId",
                table: "ItensEncomenda",
                column: "PecaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modelos_Marcas_MarcaId",
                table: "Modelos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pecas_Categorias_CategoriaId",
                table: "Pecas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pecas_Modelos_ModeloId",
                table: "Pecas",
                column: "ModeloId",
                principalTable: "Modelos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modelos_Marcas_MarcaId",
                table: "Modelos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pecas_Categorias_CategoriaId",
                table: "Pecas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pecas_Modelos_ModeloId",
                table: "Pecas");

            migrationBuilder.DropTable(
                name: "ItensEncomenda");

            migrationBuilder.DropTable(
                name: "Encomendas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Pecas_Referencia",
                table: "Pecas");

            migrationBuilder.AddForeignKey(
                name: "FK_Modelos_Marcas_MarcaId",
                table: "Modelos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pecas_Categorias_CategoriaId",
                table: "Pecas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pecas_Modelos_ModeloId",
                table: "Pecas",
                column: "ModeloId",
                principalTable: "Modelos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
