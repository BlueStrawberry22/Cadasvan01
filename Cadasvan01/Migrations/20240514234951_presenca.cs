using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cadasvan01.Migrations
{
    /// <inheritdoc />
    public partial class presenca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presencas_AspNetUsers_UsuarioId",
                table: "Presencas");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Presencas",
                newName: "AlunoId");

            migrationBuilder.RenameIndex(
                name: "IX_Presencas_UsuarioId",
                table: "Presencas",
                newName: "IX_Presencas_AlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presencas_AspNetUsers_AlunoId",
                table: "Presencas",
                column: "AlunoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presencas_AspNetUsers_AlunoId",
                table: "Presencas");

            migrationBuilder.RenameColumn(
                name: "AlunoId",
                table: "Presencas",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Presencas_AlunoId",
                table: "Presencas",
                newName: "IX_Presencas_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presencas_AspNetUsers_UsuarioId",
                table: "Presencas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
