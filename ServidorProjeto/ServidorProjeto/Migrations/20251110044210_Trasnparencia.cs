using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServidorProjeto.Migrations
{
    /// <inheritdoc />
    public partial class Trasnparencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TamanhoBytes",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "TipoMime",
                table: "Atividades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TamanhoBytes",
                table: "Atividades",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoMime",
                table: "Atividades",
                type: "TEXT",
                maxLength: 100,
                nullable: true);
        }
    }
}
