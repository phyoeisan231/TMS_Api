using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCheckWB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InboundWeight",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OutboundWeight",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "InboundWeight",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "OutboundWeight",
                table: "ICD_InBoundCheck");

            migrationBuilder.RenameColumn(
                name: "WOption",
                table: "WeightBridgeQueue",
                newName: "WBOption");

            migrationBuilder.RenameColumn(
                name: "OutWBOption",
                table: "ICD_TruckProcess",
                newName: "OutWBBillOption");

            migrationBuilder.RenameColumn(
                name: "InWBOption",
                table: "ICD_TruckProcess",
                newName: "InWBBillOption");

            migrationBuilder.RenameColumn(
                name: "OutWBOption",
                table: "ICD_InBoundCheck",
                newName: "OutWBBillOption");

            migrationBuilder.RenameColumn(
                name: "InWBOption",
                table: "ICD_InBoundCheck",
                newName: "InWBBillOption");

            migrationBuilder.AddColumn<string>(
                name: "BillOption",
                table: "WeightBridgeQueue",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WBOption",
                table: "ICD_TruckProcess",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WBOption",
                table: "ICD_InBoundCheck",
                type: "varchar(20)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillOption",
                table: "WeightBridgeQueue");

            migrationBuilder.DropColumn(
                name: "WBOption",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "WBOption",
                table: "ICD_InBoundCheck");

            migrationBuilder.RenameColumn(
                name: "WBOption",
                table: "WeightBridgeQueue",
                newName: "WOption");

            migrationBuilder.RenameColumn(
                name: "OutWBBillOption",
                table: "ICD_TruckProcess",
                newName: "OutWBOption");

            migrationBuilder.RenameColumn(
                name: "InWBBillOption",
                table: "ICD_TruckProcess",
                newName: "InWBOption");

            migrationBuilder.RenameColumn(
                name: "OutWBBillOption",
                table: "ICD_InBoundCheck",
                newName: "OutWBOption");

            migrationBuilder.RenameColumn(
                name: "InWBBillOption",
                table: "ICD_InBoundCheck",
                newName: "InWBOption");

            migrationBuilder.AddColumn<bool>(
                name: "InboundWeight",
                table: "ICD_TruckProcess",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OutboundWeight",
                table: "ICD_TruckProcess",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InboundWeight",
                table: "ICD_InBoundCheck",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OutboundWeight",
                table: "ICD_InBoundCheck",
                type: "bit",
                nullable: true);
        }
    }
}
