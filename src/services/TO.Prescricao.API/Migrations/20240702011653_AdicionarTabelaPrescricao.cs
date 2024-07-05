using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TO.Prescricao.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarTabelaPrescricao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrescricaoPaciente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomePaciente = table.Column<string>(type: "varchar(100)", nullable: false),
                    NomeMedico = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescricaoPaciente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrescricaoMedicamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrescricaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescricaoMedicamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescricaoMedicamentos_PrescricaoPaciente_PrescricaoId",
                        column: x => x.PrescricaoId,
                        principalTable: "PrescricaoPaciente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrescricaoMedicamentos_PrescricaoId",
                table: "PrescricaoMedicamentos",
                column: "PrescricaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescricaoMedicamentos");

            migrationBuilder.DropTable(
                name: "PrescricaoPaciente");
        }
    }
}
