using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cadasvan01.Migrations
{
    /// <inheritdoc />
    public partial class vinculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotoristaId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CodigosVinculacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    MotoristaId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigosVinculacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodigosVinculacao_AspNetUsers_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MotoristaId",
                table: "AspNetUsers",
                column: "MotoristaId");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosVinculacao_MotoristaId",
                table: "CodigosVinculacao",
                column: "MotoristaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_MotoristaId",
                table: "AspNetUsers",
                column: "MotoristaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_MotoristaId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CodigosVinculacao");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MotoristaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MotoristaId",
                table: "AspNetUsers");
        }
    }
}
