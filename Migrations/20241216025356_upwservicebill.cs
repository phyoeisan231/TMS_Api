using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class upwservicebill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BLNo",
                table: "WeightServiceBill",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContainerNo",
                table: "WeightServiceBill",
                type: "varchar(15)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DONo",
                table: "WeightServiceBill",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "WeightServiceBill",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransporterID",
                table: "WeightServiceBill",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VesselName",
                table: "WeightServiceBill",
                type: "varchar(20)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BLNo",
                table: "WeightServiceBill");

            migrationBuilder.DropColumn(
                name: "ContainerNo",
                table: "WeightServiceBill");

            migrationBuilder.DropColumn(
                name: "DONo",
                table: "WeightServiceBill");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "WeightServiceBill");

            migrationBuilder.DropColumn(
                name: "TransporterID",
                table: "WeightServiceBill");

            migrationBuilder.DropColumn(
                name: "VesselName",
                table: "WeightServiceBill");
        }
    }
}
