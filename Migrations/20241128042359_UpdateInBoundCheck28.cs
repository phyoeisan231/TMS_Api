using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInBoundCheck28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InArrivalDateTime",
                table: "InBoundCheck");

            migrationBuilder.DropColumn(
                name: "InDepartureDateTime",
                table: "InBoundCheck");

            migrationBuilder.CreateTable(
                name: "DocumentSetting",
                columns: table => new
                {
                    DocCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    DocName = table.Column<string>(type: "varchar(50)", nullable: true),
                    PCCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    AttachRequired = table.Column<bool>(type: "bit", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSetting", x => x.DocCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentSetting");

            migrationBuilder.AddColumn<DateTime>(
                name: "InArrivalDateTime",
                table: "InBoundCheck",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InDepartureDateTime",
                table: "InBoundCheck",
                type: "datetime",
                nullable: true);
        }
    }
}
