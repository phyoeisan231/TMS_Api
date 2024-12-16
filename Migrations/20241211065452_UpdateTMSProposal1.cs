using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTMSProposal1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TMS_Proposal",
                columns: table => new
                {
                    PropNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Yard = table.Column<string>(type: "varchar(25)", nullable: true),
                    EstDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    JobDept = table.Column<string>(type: "varchar(50)", nullable: true),
                    JobCode = table.Column<string>(type: "varchar(50)", nullable: true),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TMS_Proposal");
        }
    }
}
