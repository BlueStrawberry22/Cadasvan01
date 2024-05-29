using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cadasvan01.Migrations
{
    /// <inheritdoc />
    public partial class Avisos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mensagem",
                table: "Avisos");

            migrationBuilder.RenameColumn(
                name: "DataDoAviso",
                table: "Avisos",
                newName: "DataPublicacao");

            migrationBuilder.AddColumn<string>(
                name: "Conteudo",
                table: "Avisos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotoristaId",
                table: "Avisos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Avisos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Avisos_MotoristaId",
                table: "Avisos",
                column: "MotoristaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avisos_AspNetUsers_MotoristaId",
                table: "Avisos",
                column: "MotoristaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avisos_AspNetUsers_MotoristaId",
                table: "Avisos");

            migrationBuilder.DropIndex(
                name: "IX_Avisos_MotoristaId",
                table: "Avisos");

            migrationBuilder.DropColumn(
                name: "Conteudo",
                table: "Avisos");

            migrationBuilder.DropColumn(
                name: "MotoristaId",
                table: "Avisos");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Avisos");

            migrationBuilder.RenameColumn(
                name: "DataPublicacao",
                table: "Avisos",
                newName: "DataDoAviso");

            migrationBuilder.AddColumn<string>(
                name: "Mensagem",
                table: "Avisos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
