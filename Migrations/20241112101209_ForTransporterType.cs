using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class ForTransporterType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransporterType",
                columns: table => new
                {
                    TypeCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "varchar(50)", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(100)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransporterType", x => x.TypeCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransporterType");
        }
    }
}
