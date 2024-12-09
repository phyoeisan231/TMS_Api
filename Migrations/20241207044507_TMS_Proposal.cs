using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class TMS_Proposal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TMS_Proposal",
                columns: table => new
                {
                    PropNo = table.Column<int>(type: "int", nullable: false),
                    Yard = table.Column<string>(type: "varchar(25)", nullable: true),
                    EstDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    JobCode = table.Column<string>(type: "varchar(50)", nullable: true),
                    JobDescription = table.Column<string>(type: "varchar(250)", nullable: true),
                    JobType = table.Column<string>(type: "varchar(10)", nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(250)", nullable: true),
                    NoOfTruck = table.Column<int>(type: "int", nullable: true),
                    NoOfTEU = table.Column<int>(type: "int", nullable: true),
                    NoOfFEU = table.Column<int>(type: "int", nullable: true),
                    LCLQty = table.Column<int>(type: "int", nullable: true),
                    CargoInfo = table.Column<string>(type: "varchar(250)", nullable: true),
                    CustomerId = table.Column<string>(type: "varchar(12)", nullable: true),
                    CustomerName = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMS_Proposal", x => x.PropNo);
                });

            migrationBuilder.CreateTable(
                name: "TMS_ProposalDetails",
                columns: table => new
                {
                    PropNo = table.Column<int>(type: "int", nullable: false),
                    TruckNo = table.Column<string>(type: "varchar(10)", nullable: false),
                    TruckAssignId = table.Column<string>(type: "varchar(25)", nullable: true),
                    TruckAssignOption = table.Column<string>(type: "varchar(30)", nullable: true),
                    DriverName = table.Column<string>(type: "varchar(50)", nullable: true),
                    DriverContact = table.Column<string>(type: "varchar(50)", nullable: true),
                    NightStop = table.Column<string>(type: "varchar(50)", nullable: true),
                    OtherInfo = table.Column<string>(type: "varchar(250)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMS_ProposalDetails", x => new { x.PropNo, x.TruckNo });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TMS_Proposal");

            migrationBuilder.DropTable(
                name: "TMS_ProposalDetails");
        }
    }
}
