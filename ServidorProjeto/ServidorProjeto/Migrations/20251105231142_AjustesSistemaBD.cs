using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServidorProjeto.Migrations
{
    /// <inheritdoc />
    public partial class AjustesSistemaBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Atividades",
                newName: "TipoAtividade");

            migrationBuilder.AddColumn<double>(
                name: "DinheiroDoacao",
                table: "Usuarios",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoUsuario",
                table: "Usuarios",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "TamanhoBytes",
                table: "Atividades",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AtividadeId",
                table: "Atividades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUpload",
                table: "Atividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Atividades",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Contatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Mensagem = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contatos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ouvidorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", nullable: false),
                    Assunto = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Mensagem = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ouvidorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposAtv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAtv", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransacoesDoacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataTransacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NomeBanco = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    CodigoBanco = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Agencia = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Conta = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Metodo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacoesDoacao", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Atividades_AtividadeId",
                table: "Atividades");

            migrationBuilder.DropTable(
                name: "Contatos");

            migrationBuilder.DropTable(
                name: "Ouvidorias");

            migrationBuilder.DropTable(
                name: "TiposAtv");

            migrationBuilder.DropTable(
                name: "TransacoesDoacao");

            migrationBuilder.DropIndex(
                name: "IX_Atividades_AtividadeId",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "DinheiroDoacao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TipoUsuario",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AtividadeId",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "DataUpload",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Atividades");

            migrationBuilder.RenameColumn(
                name: "TipoAtividade",
                table: "Atividades",
                newName: "Tipo");

            migrationBuilder.AlterColumn<long>(
                name: "TamanhoBytes",
                table: "Atividades",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "decimal(10,2)",
                oldNullable: true);
        }
    }
}
