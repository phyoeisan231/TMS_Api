using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobCode",
                table: "ICD_OutBoundCheck");

            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "ICD_OutBoundCheck");

            migrationBuilder.DropColumn(
                name: "OutContainerSize",
                table: "ICD_OutBoundCheck");

            migrationBuilder.DropColumn(
                name: "OutNoOfContainer",
                table: "ICD_OutBoundCheck");

            migrationBuilder.RenameColumn(
                name: "OutContainerType",
                table: "ICD_OutBoundCheck",
                newName: "OutWeightBridgeID");

            migrationBuilder.AddColumn<string>(
                name: "DriverContactNo",
                table: "ICD_TruckProcess",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverContactNo",
                table: "ICD_OutBoundCheck",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OutboundWeight",
                table: "ICD_OutBoundCheck",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverContactNo",
                table: "ICD_InBoundCheck",
                type: "varchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverContactNo",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "DriverContactNo",
                table: "ICD_OutBoundCheck");

            migrationBuilder.DropColumn(
                name: "OutboundWeight",
                table: "ICD_OutBoundCheck");

            migrationBuilder.DropColumn(
                name: "DriverContactNo",
                table: "ICD_InBoundCheck");

            migrationBuilder.RenameColumn(
                name: "OutWeightBridgeID",
                table: "ICD_OutBoundCheck",
                newName: "OutContainerType");

            migrationBuilder.AddColumn<string>(
                name: "JobCode",
                table: "ICD_OutBoundCheck",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "ICD_OutBoundCheck",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutContainerSize",
                table: "ICD_OutBoundCheck",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutNoOfContainer",
                table: "ICD_OutBoundCheck",
                type: "int",
                nullable: true);
        }
    }
}
