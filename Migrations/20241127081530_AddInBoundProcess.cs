using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddInBoundProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InBoundCheck",
                columns: table => new
                {
                    InRegNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InYardID = table.Column<string>(type: "varchar(25)", nullable: true),
                    InGateID = table.Column<string>(type: "varchar(25)", nullable: true),
                    InPCCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    InContainerType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InContainerSize = table.Column<int>(type: "int", nullable: true),
                    InType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InCargoType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InNoOfContainer = table.Column<int>(type: "int", nullable: true),
                    TruckType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InCheckDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    InArrivalDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    InDepartureDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    TruckVehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    TrailerVehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    DriverLicenseNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    DriverName = table.Column<string>(type: "varchar(50)", nullable: true),
                    JobCode = table.Column<string>(type: "varchar(30)", nullable: true),
                    JobDescription = table.Column<string>(type: "varchar(150)", nullable: true),
                    CardNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    Status = table.Column<string>(type: "varchar(15)", nullable: true),
                    Remark = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InBoundCheck", x => x.InRegNo);
                });

            migrationBuilder.CreateTable(
                name: "InBoundCheckDocument",
                columns: table => new
                {
                    InRegNo = table.Column<int>(type: "int", nullable: false),
                    DocCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    CheckStatus = table.Column<string>(type: "varchar(15)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InBoundCheckDocument", x => new { x.InRegNo, x.DocCode });
                });

            migrationBuilder.CreateTable(
                name: "TruckProcess",
                columns: table => new
                {
                    InRegNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InYardID = table.Column<string>(type: "varchar(25)", nullable: true),
                    InGateID = table.Column<string>(type: "varchar(25)", nullable: true),
                    InPCCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    InContainerType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InContainerSize = table.Column<int>(type: "int", nullable: true),
                    InType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InCargoType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InNoOfContainer = table.Column<int>(type: "int", nullable: true),
                    InBoundWeight = table.Column<decimal>(type: "decimal(18,5)", nullable: true),
                    TruckType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InCheckDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    InWeightTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    TruckVehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    TrailerVehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    DriverLicenseNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    DriverName = table.Column<string>(type: "varchar(50)", nullable: true),
                    JobCode = table.Column<string>(type: "varchar(30)", nullable: true),
                    JobDescription = table.Column<string>(type: "varchar(150)", nullable: true),
                    CardNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    InYard = table.Column<bool>(type: "bit", nullable: true),
                    OutRegNo = table.Column<int>(type: "int", nullable: true),
                    OutYardID = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutGateID = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutPCCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    OutContainerType = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutContainerSize = table.Column<int>(type: "int", nullable: true),
                    OutType = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutCargoType = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutNoOfContainer = table.Column<int>(type: "int", nullable: true),
                    OutCheckDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    OutBoundWeight = table.Column<decimal>(type: "decimal(18,5)", nullable: true),
                    OutWeightTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    OutArrivalDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    OutDepartureDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckProcess", x => x.InRegNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InBoundCheck");

            migrationBuilder.DropTable(
                name: "InBoundCheckDocument");

            migrationBuilder.DropTable(
                name: "TruckProcess");
        }
    }
}
