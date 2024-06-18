using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cadasvan01.Migrations
{
    /// <inheritdoc />
    public partial class PC1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Placa",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CorVan1",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorVan2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModeloVan1",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModeloVan2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlacaVan1",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlacaVan2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorVan1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CorVan2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModeloVan1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModeloVan2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlacaVan1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlacaVan2",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Placa",
                table: "AspNetUsers",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: true);
        }
    }
}
