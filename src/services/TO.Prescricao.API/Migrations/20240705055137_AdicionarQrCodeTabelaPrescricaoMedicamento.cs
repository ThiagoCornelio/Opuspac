using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TO.Prescricao.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarQrCodeTabelaPrescricaoMedicamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntervaloHoras",
                table: "PrescricaoMedicamentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QRCode",
                table: "PrescricaoMedicamentos",
                type: "varchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervaloHoras",
                table: "PrescricaoMedicamentos");

            migrationBuilder.DropColumn(
                name: "QRCode",
                table: "PrescricaoMedicamentos");
        }
    }
}
