using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateICD_TruckProcessandwbqueue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ICD_TruckProcess");

            migrationBuilder.CreateTable(
                name: "WeightBridgeQueue",
                columns: table => new
                {
                    QueueNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_WeightBridgeQueue", x => x.QueueNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeightBridgeQueue");

            migrationBuilder.CreateTable(
                name: "ICD_TruckProcess",
                columns: table => new
                {
                    InRegNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaID = table.Column<string>(type: "varchar(25)", nullable: true),
                    CardNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    Customer = table.Column<string>(type: "varchar(150)", nullable: true),
                    DriverContactNo = table.Column<string>(type: "varchar(100)", nullable: true),
                    DriverLicenseNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    DriverName = table.Column<string>(type: "varchar(50)", nullable: true),
                    InCargoInfo = table.Column<string>(type: "varchar(50)", nullable: true),
                    InCargoType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InCheckDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    InGateID = table.Column<string>(type: "varchar(25)", nullable: true),
                    InGatePassTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    InPCCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    InType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InWeightBridgeID = table.Column<string>(type: "varchar(25)", nullable: true),
                    InYard = table.Column<bool>(type: "bit", nullable: true),
                    InYardID = table.Column<string>(type: "varchar(25)", nullable: true),
                    InboundWeight = table.Column<bool>(type: "bit", nullable: true),
                    OutCargoInfo = table.Column<string>(type: "varchar(50)", nullable: true),
                    OutCargoType = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutCheckDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    OutGateID = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutGatePassTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    OutPCCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    OutRegNo = table.Column<int>(type: "int", nullable: true),
                    OutType = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutWeightBridgeID = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutYardID = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutboundWeight = table.Column<bool>(type: "bit", nullable: true),
                    Remark = table.Column<string>(type: "varchar(max)", nullable: true),
                    Status = table.Column<string>(type: "varchar(20)", nullable: true),
                    TrailerVehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    TransporterID = table.Column<string>(type: "varchar(25)", nullable: true),
                    TransporterName = table.Column<string>(type: "varchar(100)", nullable: true),
                    TruckType = table.Column<string>(type: "varchar(25)", nullable: true),
                    TruckVehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICD_TruckProcess", x => x.InRegNo);
                });
        }
    }
}
