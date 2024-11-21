using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMasterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransporterID",
                table: "Driver");

            migrationBuilder.RenameColumn(
                name: "GateID",
                table: "WeightBridge",
                newName: "YardID");

            migrationBuilder.AddColumn<bool>(
                name: "IsRGL",
                table: "Truck",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Gate",
                type: "varchar(25)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRGL",
                table: "Truck");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Gate");

            migrationBuilder.RenameColumn(
                name: "YardID",
                table: "WeightBridge",
                newName: "GateID");

            migrationBuilder.AddColumn<string>(
                name: "TransporterID",
                table: "Driver",
                type: "varchar(25)",
                nullable: true);
        }
    }
}
