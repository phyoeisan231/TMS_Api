using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTMSInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PCard",
                columns: table => new
                {
                    CardNo = table.Column<string>(type: "varchar(25)", nullable: false),
                    YardID = table.Column<string>(type: "varchar(25)", nullable: true),
                    GroupName = table.Column<string>(type: "varchar(10)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    IsUse = table.Column<bool>(type: "bit", nullable: true),
                    VehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    CardIssueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CardReturnDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCard", x => x.CardNo);
                });

            migrationBuilder.CreateTable(
                name: "PCategory",
                columns: table => new
                {
                    PCCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    CategoryName = table.Column<string>(type: "varchar(50)", nullable: true),
                    InboundWeight = table.Column<decimal>(type: "decimal(18,5)", nullable: true),
                    OutboundWeight = table.Column<decimal>(type: "decimal(18,5)", nullable: true),
                    GroupName = table.Column<string>(type: "varchar(10)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCategory", x => x.PCCode);
                });

            migrationBuilder.CreateTable(
                name: "WaitingArea",
                columns: table => new
                {
                    AreaID = table.Column<string>(type: "varchar(25)", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", nullable: true),
                    YardID = table.Column<string>(type: "varchar(25)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaitingArea", x => x.AreaID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PCard");

            migrationBuilder.DropTable(
                name: "PCategory");

            migrationBuilder.DropTable(
                name: "WaitingArea");
        }
    }
}
