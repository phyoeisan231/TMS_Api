﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateICDTruckProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ICD_TruckProcess",
                columns: table => new
                {
                    InNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InRegNo = table.Column<int>(type: "int", nullable: true),
                    InYardID = table.Column<string>(type: "varchar(25)", nullable: true),
                    InGateID = table.Column<string>(type: "varchar(25)", nullable: true),
                    InPCCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    InContainerType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InContainerSize = table.Column<int>(type: "int", nullable: true),
                    InType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InCargoType = table.Column<string>(type: "varchar(25)", nullable: true),
                    InCargoInfo = table.Column<string>(type: "varchar(50)", nullable: true),
                    InNoOfContainer = table.Column<int>(type: "int", nullable: true),
                    InCheckDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    AreaID = table.Column<string>(type: "varchar(25)", nullable: true),
                    TruckType = table.Column<string>(type: "varchar(25)", nullable: true),
                    TruckVehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    TrailerVehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    DriverLicenseNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    DriverName = table.Column<string>(type: "varchar(50)", nullable: true),
                    JobCode = table.Column<string>(type: "varchar(30)", nullable: true),
                    JobDescription = table.Column<string>(type: "varchar(150)", nullable: true),
                    CardNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    TransporterID = table.Column<string>(type: "varchar(25)", nullable: true),
                    TransporterName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Customer = table.Column<string>(type: "varchar(150)", nullable: true),
                    InYard = table.Column<bool>(type: "bit", nullable: true),
                    OutRegNo = table.Column<int>(type: "int", nullable: true),
                    OutYardID = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutGateID = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutPCCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    OutContainerType = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutContainerSize = table.Column<int>(type: "int", nullable: true),
                    OutType = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutCargoType = table.Column<string>(type: "varchar(25)", nullable: true),
                    OutCargoInfo = table.Column<string>(type: "varchar(50)", nullable: true),
                    OutNoOfContainer = table.Column<int>(type: "int", nullable: true),
                    OutCheckDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    InGatePassTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    OutGatePassTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Remark = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICD_TruckProcess", x => x.InNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ICD_TruckProcess");
        }
    }
}