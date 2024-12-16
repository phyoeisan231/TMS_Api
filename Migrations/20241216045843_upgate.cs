using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class upgate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Gate",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Gate",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Gate",
                type: "varchar(250)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Gate");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Gate");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Gate");
        }
    }
}
