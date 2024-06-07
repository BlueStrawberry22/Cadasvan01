using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cadasvan01.Migrations
{
    /// <inheritdoc />
    public partial class NomeSobrenome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeCompleto",
                table: "AspNetUsers",
                newName: "Sobrenome");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Sobrenome",
                table: "AspNetUsers",
                newName: "NomeCompleto");
        }
    }
}
