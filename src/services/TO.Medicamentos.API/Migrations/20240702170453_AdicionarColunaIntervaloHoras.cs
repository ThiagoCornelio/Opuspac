using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TO.Medicamentos.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarColunaIntervaloHoras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntervaloHoras",
                table: "Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervaloHoras",
                table: "Medicamentos");
        }
    }
}
