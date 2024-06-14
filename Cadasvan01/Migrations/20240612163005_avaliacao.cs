using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cadasvan01.Migrations
{
    /// <inheritdoc />
    public partial class avaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    AvaliacaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotoristaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AlunoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AvaliacaoEstrelas = table.Column<int>(type: "int", nullable: false),
                    Opiniao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAvaliacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.AvaliacaoId);
                    table.ForeignKey(
                        name: "FK_Avaliacoes_AspNetUsers_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Avaliacoes_AspNetUsers_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_AlunoId",
                table: "Avaliacoes",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_MotoristaId",
                table: "Avaliacoes",
                column: "MotoristaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacoes");
        }
    }
}
