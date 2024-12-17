using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTMS_ProposalDetail1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "WeightServiceBill",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "NightStop",
                table: "TMS_ProposalDetails",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "WeightServiceBill");

            migrationBuilder.AlterColumn<string>(
                name: "NightStop",
                table: "TMS_ProposalDetails",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
