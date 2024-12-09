using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWBQueue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeightBridgeQueue");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "OperationArea",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InWeightDateTime",
                table: "ICD_TruckProcess",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OutWeightDateTime",
                table: "ICD_TruckProcess",
                type: "datetime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "OperationArea");

            migrationBuilder.DropColumn(
                name: "InWeightDateTime",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OutWeightDateTime",
                table: "ICD_TruckProcess");

            migrationBuilder.CreateTable(
                name: "WeightBridgeQueue",
                columns: table => new
                {
                    QueueNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    CargoInfo = table.Column<string>(type: "varchar(50)", nullable: true),
                    CargoType = table.Column<string>(type: "varchar(25)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    Customer = table.Column<string>(type: "varchar(150)", nullable: true),
                    DriverContactNo = table.Column<string>(type: "varchar(100)", nullable: true),
                    DriverLicenseNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    DriverName = table.Column<string>(type: "varchar(50)", nullable: true),
                    GateID = table.Column<string>(type: "varchar(25)", nullable: true),
                    InRegNo = table.Column<int>(type: "int", nullable: true),
                    JobCode = table.Column<string>(type: "varchar(25)", nullable: true),
                    JobDescription = table.Column<string>(type: "varchar(150)", nullable: true),
                    Status = table.Column<string>(type: "varchar(25)", nullable: true),
                    TrailerVehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    TruckVehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    Type = table.Column<string>(type: "varchar(15)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    WBillNo = table.Column<int>(type: "int", nullable: true),
                    WOption = table.Column<string>(type: "varchar(25)", nullable: true),
                    WeightBridgeID = table.Column<string>(type: "varchar(25)", nullable: true),
                    WeightDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    YardID = table.Column<string>(type: "varchar(25)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightBridgeQueue", x => x.QueueNo);
                });
        }
    }
}
