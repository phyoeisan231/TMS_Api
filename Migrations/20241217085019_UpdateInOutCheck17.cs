using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInOutCheck17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WBOption",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "WBOption",
                table: "ICD_InBoundCheck");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "ICD_TruckProcess",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUseWB",
                table: "ICD_TruckProcess",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "ICD_OutBoundCheck",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "ICD_InBoundCheck",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUseWB",
                table: "ICD_InBoundCheck",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropNo",
                table: "ICD_InBoundCheck",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "IsUseWB",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "ICD_OutBoundCheck");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "IsUseWB",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "PropNo",
                table: "ICD_InBoundCheck");

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
    }
}
