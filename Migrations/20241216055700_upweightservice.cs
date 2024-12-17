using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class upweightservice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "WeightServiceBill",
                newName: "OutWeight");

            migrationBuilder.RenameColumn(
                name: "ServiceBillDate",
                table: "WeightServiceBill",
                newName: "OutWeightTime");

            migrationBuilder.AddColumn<string>(
                name: "BillOption",
                table: "WeightServiceBill",
                type: "varchar(15)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GateID",
                table: "WeightServiceBill",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InWeight",
                table: "WeightServiceBill",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InWeightTime",
                table: "WeightServiceBill",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NetWeight",
                table: "WeightServiceBill",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YardID",
                table: "WeightServiceBill",
                type: "varchar(25)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillOption",
                table: "WeightServiceBill");

            migrationBuilder.DropColumn(
                name: "GateID",
                table: "WeightServiceBill");

            migrationBuilder.DropColumn(
                name: "InWeight",
                table: "WeightServiceBill");

            migrationBuilder.DropColumn(
                name: "InWeightTime",
                table: "WeightServiceBill");

            migrationBuilder.DropColumn(
                name: "NetWeight",
                table: "WeightServiceBill");

            migrationBuilder.DropColumn(
                name: "YardID",
                table: "WeightServiceBill");

            migrationBuilder.RenameColumn(
                name: "OutWeightTime",
                table: "WeightServiceBill",
                newName: "ServiceBillDate");

            migrationBuilder.RenameColumn(
                name: "OutWeight",
                table: "WeightServiceBill",
                newName: "Weight");
        }
    }
}
