using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTruckProcessCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InboundWeight",
                table: "PCategory");

            migrationBuilder.DropColumn(
                name: "OutboundWeight",
                table: "PCategory");

            migrationBuilder.AddColumn<int>(
                name: "GDNNo",
                table: "ICD_TruckProcess",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GRNNo",
                table: "ICD_TruckProcess",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OptEndDate",
                table: "ICD_TruckProcess",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OptStartDate",
                table: "ICD_TruckProcess",
                type: "datetime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GDNNo",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "GRNNo",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OptEndDate",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OptStartDate",
                table: "ICD_TruckProcess");

            migrationBuilder.AddColumn<bool>(
                name: "InboundWeight",
                table: "PCategory",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OutboundWeight",
                table: "PCategory",
                type: "bit",
                nullable: true);
        }
    }
}
