using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cadasvan01.Migrations
{
    /// <inheritdoc />
    public partial class aaaaaaaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cidades_CidadeDestinoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cidades_CidadePartidaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CidadeDestinoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CidadePartidaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CidadeDestinoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CidadePartidaId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "CNH",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Itinerario",
                table: "AspNetUsers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Itinerario",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "CNH",
                table: "AspNetUsers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CidadeDestinoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CidadePartidaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CidadeDestinoId",
                table: "AspNetUsers",
                column: "CidadeDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CidadePartidaId",
                table: "AspNetUsers",
                column: "CidadePartidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cidades_CidadeDestinoId",
                table: "AspNetUsers",
                column: "CidadeDestinoId",
                principalTable: "Cidades",
                principalColumn: "CidadeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cidades_CidadePartidaId",
                table: "AspNetUsers",
                column: "CidadePartidaId",
                principalTable: "Cidades",
                principalColumn: "CidadeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
