using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoPartsShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarPedidosFavoritosAvaliacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PecaId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    NomeUtilizador = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Estrelas = table.Column<int>(type: "INTEGER", nullable: false),
                    Comentario = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    DataAvaliacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Verificado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Pecas_PecaId",
                        column: x => x.PecaId,
                        principalTable: "Pecas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    PecaId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataAdicao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favoritos_Pecas_PecaId",
                        column: x => x.PecaId,
                        principalTable: "Pecas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificacoesStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PecaId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DataRegisto = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notificado = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataNotificacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacoesStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificacoesStock_Pecas_PecaId",
                        column: x => x.PecaId,
                        principalTable: "Pecas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    NomeCliente = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Morada = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    CodigoPostal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MetodoPagamento = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidoItens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PedidoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PecaId = table.Column<int>(type: "INTEGER", nullable: false),
                    NomePeca = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Referencia = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoItens_Pecas_PecaId",
                        column: x => x.PecaId,
                        principalTable: "Pecas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoItens_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_PecaId",
                table: "Avaliacoes",
                column: "PecaId");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_PecaId",
                table: "Favoritos",
                column: "PecaId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacoesStock_PecaId",
                table: "NotificacoesStock",
                column: "PecaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_PecaId",
                table: "PedidoItens",
                column: "PecaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_PedidoId",
                table: "PedidoItens",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropTable(
                name: "NotificacoesStock");

            migrationBuilder.DropTable(
                name: "PedidoItens");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
