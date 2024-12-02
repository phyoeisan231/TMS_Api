using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInBoundCheck30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InBoundWeight",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "InWeightTime",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OutBoundWeight",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OutWeightTime",
                table: "ICD_TruckProcess");

            migrationBuilder.RenameColumn(
                name: "OutRegNo",
                table: "ICD_TruckProcess",
                newName: "OutCheckNo");

            migrationBuilder.AddColumn<string>(
                name: "AreaID",
                table: "ICD_TruckProcess",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer",
                table: "ICD_TruckProcess",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InCargoInfo",
                table: "ICD_TruckProcess",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InCheckNo",
                table: "ICD_TruckProcess",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer",
                table: "ICD_InBoundCheck",
                type: "varchar(150)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaID",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "Customer",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "InCargoInfo",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "InCheckNo",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "Customer",
                table: "ICD_InBoundCheck");

            migrationBuilder.RenameColumn(
                name: "OutCheckNo",
                table: "ICD_TruckProcess",
                newName: "OutRegNo");

            migrationBuilder.AddColumn<decimal>(
                name: "InBoundWeight",
                table: "ICD_TruckProcess",
                type: "decimal(18,5)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InWeightTime",
                table: "ICD_TruckProcess",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OutBoundWeight",
                table: "ICD_TruckProcess",
                type: "decimal(18,5)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OutWeightTime",
                table: "ICD_TruckProcess",
                type: "datetime",
                nullable: true);
        }
    }
}
