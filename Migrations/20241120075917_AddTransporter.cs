using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTransporter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transporter",
                columns: table => new
                {
                    TransporterID = table.Column<string>(type: "varchar(25)", nullable: false),
                    TransporterName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Address = table.Column<string>(type: "varchar(250)", nullable: true),
                    ContactNo = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    ContactPerson = table.Column<string>(type: "varchar(50)", nullable: true),
                    TypeID = table.Column<string>(type: "varchar(25)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    IsBlack = table.Column<bool>(type: "bit", nullable: true),
                    BlackDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BlackReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    BlackRemovedReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    BlackRemovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    SAPID = table.Column<string>(type: "varchar(25)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transporter", x => x.TransporterID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transporter");
        }
    }
}
