using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOutCheckForTMS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlNo",
                table: "ICD_OutBoundCheck",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobCode",
                table: "ICD_OutBoundCheck",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobDept",
                table: "ICD_OutBoundCheck",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobType",
                table: "ICD_OutBoundCheck",
                type: "varchar(10)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlNo",
                table: "ICD_OutBoundCheck");

            migrationBuilder.DropColumn(
                name: "JobCode",
                table: "ICD_OutBoundCheck");

            migrationBuilder.DropColumn(
                name: "JobDept",
                table: "ICD_OutBoundCheck");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "ICD_OutBoundCheck");
        }
    }
}
