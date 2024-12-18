using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTMSProposalPCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AreaID",
                table: "TMS_Proposal",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BLNo",
                table: "TMS_Proposal",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PCCode",
                table: "TMS_Proposal",
                type: "varchar(10)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaID",
                table: "TMS_Proposal");

            migrationBuilder.DropColumn(
                name: "BLNo",
                table: "TMS_Proposal");

            migrationBuilder.DropColumn(
                name: "PCCode",
                table: "TMS_Proposal");
        }
    }
}
