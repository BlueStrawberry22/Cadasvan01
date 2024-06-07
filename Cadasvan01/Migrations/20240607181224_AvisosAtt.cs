using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cadasvan01.Migrations
{
    /// <inheritdoc />
    public partial class AvisosAtt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Lido",
                table: "Avisos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lido",
                table: "Avisos");
        }
    }
}
