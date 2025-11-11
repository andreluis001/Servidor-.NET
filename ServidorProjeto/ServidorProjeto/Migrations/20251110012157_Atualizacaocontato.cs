using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServidorProjeto.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacaocontato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Assunto",
                table: "Contatos",
                type: "TEXT",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Contatos",
                type: "TEXT",
                maxLength: 11,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assunto",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Contatos");
        }
    }
}
