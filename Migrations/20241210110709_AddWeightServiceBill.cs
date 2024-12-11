using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddWeightServiceBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeightServiceBill",
                columns: table => new
                {
                    ServiceBillNo = table.Column<string>(type: "varchar(20)", nullable: false),
                    WeightBridgeID = table.Column<string>(type: "varchar(25)", nullable: true),
                    ServiceBillDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TruckNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    TrailerNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    QRegNo = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WeightType = table.Column<string>(type: "varchar(50)", nullable: true),
                    CargoInfo = table.Column<string>(type: "varchar(250)", nullable: true),
                    WeightOption = table.Column<string>(type: "varchar(50)", nullable: true),
                    CustomerId = table.Column<string>(type: "varchar(50)", nullable: true),
                    CustomerName = table.Column<string>(type: "varchar(50)", nullable: true),
                    CashAmt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CheckInRegNo = table.Column<int>(type: "int", nullable: true),
                    WeightCategory = table.Column<string>(type: "varchar(100)", nullable: true),
                    DriverName = table.Column<string>(type: "varchar(250)", nullable: true),
                    DriverLicense = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightServiceBill", x => x.ServiceBillNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeightServiceBill");
        }
    }
}
