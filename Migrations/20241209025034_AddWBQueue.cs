using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddWBQueue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeightBridgeQueue",
                columns: table => new
                {
                    RegNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QueueNo = table.Column<int>(type: "int", nullable: true),
                    InRegNo = table.Column<int>(type: "int", nullable: true),
                    YardID = table.Column<string>(type: "varchar(25)", nullable: true),
                    GateID = table.Column<string>(type: "varchar(25)", nullable: true),
                    Type = table.Column<string>(type: "varchar(15)", nullable: true),
                    CargoType = table.Column<string>(type: "varchar(25)", nullable: true),
                    CargoInfo = table.Column<string>(type: "varchar(50)", nullable: true),
                    TruckVehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    TrailerVehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    DriverLicenseNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    DriverName = table.Column<string>(type: "varchar(50)", nullable: true),
                    DriverContactNo = table.Column<string>(type: "varchar(100)", nullable: true),
                    CardNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    WeightBridgeID = table.Column<string>(type: "varchar(25)", nullable: true),
                    WOption = table.Column<string>(type: "varchar(25)", nullable: true),
                    WBillNo = table.Column<int>(type: "int", nullable: true),
                    Customer = table.Column<string>(type: "varchar(150)", nullable: true),
                    JobCode = table.Column<string>(type: "varchar(25)", nullable: true),
                    JobDescription = table.Column<string>(type: "varchar(150)", nullable: true),
                    WeightDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "varchar(25)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightBridgeQueue", x => x.RegNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeightBridgeQueue");
        }
    }
}
