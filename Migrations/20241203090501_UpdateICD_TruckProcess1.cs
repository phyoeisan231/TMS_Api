using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateICD_TruckProcess1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InContainerSize",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "InContainerType",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "JobCode",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OutContainerSize",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OutContainerType",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OutNoOfContainer",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "InContainerSize",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "InContainerType",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "InNoOfContainer",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "JobCode",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "ICD_InBoundCheck");

            migrationBuilder.AlterColumn<bool>(
                name: "OutboundWeight",
                table: "PCategory",
                type: "bit",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "InboundWeight",
                table: "PCategory",
                type: "bit",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,5)",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InboundWeight",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "OutboundWeight",
                table: "ICD_InBoundCheck");

            migrationBuilder.AlterColumn<decimal>(
                name: "OutboundWeight",
                table: "PCategory",
                type: "decimal(18,5)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InboundWeight",
                table: "PCategory",
                type: "decimal(18,5)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InContainerSize",
                table: "ICD_TruckProcess",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InContainerType",
                table: "ICD_TruckProcess",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobCode",
                table: "ICD_TruckProcess",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "ICD_TruckProcess",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutContainerSize",
                table: "ICD_TruckProcess",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutContainerType",
                table: "ICD_TruckProcess",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutNoOfContainer",
                table: "ICD_TruckProcess",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InContainerSize",
                table: "ICD_InBoundCheck",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InContainerType",
                table: "ICD_InBoundCheck",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InNoOfContainer",
                table: "ICD_InBoundCheck",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobCode",
                table: "ICD_InBoundCheck",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "ICD_InBoundCheck",
                type: "varchar(150)",
                nullable: true);
        }
    }
}
