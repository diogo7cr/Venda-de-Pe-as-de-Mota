using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoPartsShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    PrimeiroNome = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UltimoNome = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Morada = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CodigoPostal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Cidade = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    FotoPerfilUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
