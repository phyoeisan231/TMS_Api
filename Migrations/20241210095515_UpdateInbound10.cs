using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInbound10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InWBOption",
                table: "ICD_TruckProcess",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutWBOption",
                table: "ICD_TruckProcess",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InWBOption",
                table: "ICD_InBoundCheck",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutWBOption",
                table: "ICD_InBoundCheck",
                type: "varchar(25)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InWBOption",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OutWBOption",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "InWBOption",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "OutWBOption",
                table: "ICD_InBoundCheck");
        }
    }
}
