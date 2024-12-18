using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInCheckForTMS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlNo",
                table: "WeightBridgeQueue",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContainerNo",
                table: "WeightBridgeQueue",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlNo",
                table: "ICD_TruckProcess",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InContainerNo",
                table: "ICD_TruckProcess",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InContainerSize",
                table: "ICD_TruckProcess",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InContainerType",
                table: "ICD_TruckProcess",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobCode",
                table: "ICD_TruckProcess",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobDept",
                table: "ICD_TruckProcess",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobType",
                table: "ICD_TruckProcess",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutContainerNo",
                table: "ICD_TruckProcess",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutContainerSize",
                table: "ICD_TruckProcess",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutContainerType",
                table: "ICD_TruckProcess",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutContainerNo",
                table: "ICD_OutBoundCheck",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutContainerSize",
                table: "ICD_OutBoundCheck",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutContainerType",
                table: "ICD_OutBoundCheck",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlNo",
                table: "ICD_InBoundCheck",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InContainerNo",
                table: "ICD_InBoundCheck",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InContainerSize",
                table: "ICD_InBoundCheck",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InContainerType",
                table: "ICD_InBoundCheck",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobCode",
                table: "ICD_InBoundCheck",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobDept",
                table: "ICD_InBoundCheck",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobType",
                table: "ICD_InBoundCheck",
                type: "varchar(10)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlNo",
                table: "WeightBridgeQueue");

            migrationBuilder.DropColumn(
                name: "ContainerNo",
                table: "WeightBridgeQueue");

            migrationBuilder.DropColumn(
                name: "BlNo",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "InContainerNo",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "InContainerSize",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "InContainerType",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "JobCode",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "JobDept",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OutContainerNo",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OutContainerSize",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OutContainerType",
                table: "ICD_TruckProcess");

            migrationBuilder.DropColumn(
                name: "OutContainerNo",
                table: "ICD_OutBoundCheck");

            migrationBuilder.DropColumn(
                name: "OutContainerSize",
                table: "ICD_OutBoundCheck");

            migrationBuilder.DropColumn(
                name: "OutContainerType",
                table: "ICD_OutBoundCheck");

            migrationBuilder.DropColumn(
                name: "BlNo",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "InContainerNo",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "InContainerSize",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "InContainerType",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "JobCode",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "JobDept",
                table: "ICD_InBoundCheck");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "ICD_InBoundCheck");
        }
    }
}
