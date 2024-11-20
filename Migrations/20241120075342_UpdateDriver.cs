using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDriver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4748e16c-642e-4aff-9cf1-db0b86d523f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "898546bc-8ce0-467f-91bf-54a7ed88e818");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5d54789-70aa-414a-84e0-894857bd8e2d");

            migrationBuilder.RenameColumn(
                name: "TransporterCode",
                table: "Truck",
                newName: "TransporterID");

            migrationBuilder.RenameColumn(
                name: "TransporterCode",
                table: "Trailer",
                newName: "TransporterID");

            migrationBuilder.RenameColumn(
                name: "TransporterCode",
                table: "Driver",
                newName: "TransporterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransporterID",
                table: "Truck",
                newName: "TransporterCode");

            migrationBuilder.RenameColumn(
                name: "TransporterID",
                table: "Trailer",
                newName: "TransporterCode");

            migrationBuilder.RenameColumn(
                name: "TransporterID",
                table: "Driver",
                newName: "TransporterCode");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4748e16c-642e-4aff-9cf1-db0b86d523f1", null, "Admin", "ADMIN" },
                    { "898546bc-8ce0-467f-91bf-54a7ed88e818", null, "GateUser", "GATEUSER" },
                    { "e5d54789-70aa-414a-84e0-894857bd8e2d", null, "WBUser", "WBUSER" }
                });
        }
    }
}
