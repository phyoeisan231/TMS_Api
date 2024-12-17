using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class upyard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Gate");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Gate");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Gate");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Yard",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Yard",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Yard",
                type: "varchar(50)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Yard");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Yard");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Yard");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Gate",
                type: "varchar(250)",
                nullable: true);

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
        }
    }
}
