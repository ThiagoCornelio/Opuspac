using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TO.Prescricao.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoverIntervaloHorasTabelaPrescricaoMedicamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervaloHoras",
                table: "PrescricaoMedicamentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntervaloHoras",
                table: "PrescricaoMedicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
