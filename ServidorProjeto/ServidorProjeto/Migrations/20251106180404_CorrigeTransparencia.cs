using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServidorProjeto.Migrations
{
    /// <inheritdoc />
    public partial class CorrigeTransparencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Atividades_AtividadeId",
                table: "Atividades");

            migrationBuilder.DropIndex(
                name: "IX_Atividades_AtividadeId",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "AtividadeId",
                table: "Atividades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AtividadeId",
                table: "Atividades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_AtividadeId",
                table: "Atividades",
                column: "AtividadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Atividades_AtividadeId",
                table: "Atividades",
                column: "AtividadeId",
                principalTable: "Atividades",
                principalColumn: "Id");
        }
    }
}
