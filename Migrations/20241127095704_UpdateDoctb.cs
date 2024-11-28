using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDoctb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "CheckStatus",
                table: "InBoundCheckDocument",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocName",
                table: "InBoundCheckDocument",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "InBoundCheckDocument",
                type: "varchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocName",
                table: "InBoundCheckDocument");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "InBoundCheckDocument");

            migrationBuilder.AlterColumn<string>(
                name: "CheckStatus",
                table: "InBoundCheckDocument",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
