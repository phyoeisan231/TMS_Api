using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTMSProposal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "TMS_Proposal");

            migrationBuilder.AddColumn<string>(
                name: "JobDept",
                table: "TMS_Proposal",
                type: "varchar(50)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobDept",
                table: "TMS_Proposal");

            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "TMS_Proposal",
                type: "varchar(250)",
                nullable: true);
        }
    }
}
