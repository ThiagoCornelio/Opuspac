using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TO.Prescricao.API.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTamanhoCampoQrCodeTabelaPrescricaoMedicamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "QRCode",
                table: "PrescricaoMedicamentos",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "QRCode",
                table: "PrescricaoMedicamentos",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);
        }
    }
}
