using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class initdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    LicenseNo = table.Column<string>(type: "varchar(25)", nullable: false),
                    NRC = table.Column<string>(type: "varchar(25)", nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    Address = table.Column<string>(type: "varchar(250)", nullable: true),
                    ContactNo = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    LicenseExpiration = table.Column<DateTime>(type: "datetime", nullable: true),
                    LicenseClass = table.Column<string>(type: "varchar(2)", nullable: true),
                    Transporter = table.Column<string>(type: "varchar(100)", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(max)", nullable: true),
                    IsBlack = table.Column<bool>(type: "bit", nullable: true),
                    BlackDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BlackRemovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BlackReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    BlackRemovedReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.LicenseNo);
                });

            migrationBuilder.CreateTable(
                name: "Trailer",
                columns: table => new
                {
                    VehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: false),
                    UnladenWeight = table.Column<decimal>(type: "Decimal(18,0)", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(max)", nullable: true),
                    Flowrate = table.Column<double>(type: "float", nullable: true),
                    TrailerType = table.Column<string>(type: "varchar(30)", nullable: true),
                    Transporter = table.Column<string>(type: "varchar(10)", nullable: true),
                    IsBlack = table.Column<bool>(type: "bit", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    LoadingType = table.Column<string>(type: "varchar(50)", nullable: true),
                    BlackDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BlackRemovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CompartmentNo = table.Column<int>(type: "int", nullable: true),
                    DriverLicenseNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    LastLoadingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    VehicleBackRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    BlackReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    BlackRemovedReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trailer", x => x.VehicleRegNo);
                });

            migrationBuilder.CreateTable(
                name: "TrailerType",
                columns: table => new
                {
                    TypeCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(30)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrailerType", x => x.TypeCode);
                });

            migrationBuilder.CreateTable(
                name: "Transporter",
                columns: table => new
                {
                    TransporterCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    TransporterName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Address = table.Column<string>(type: "varchar(250)", nullable: true),
                    ContactNo = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    ContactPerson = table.Column<string>(type: "varchar(50)", nullable: true),
                    TransporterType = table.Column<string>(type: "varchar(50)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    IsBlack = table.Column<bool>(type: "bit", nullable: true),
                    BlackDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BlackRemovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(max)", nullable: true),
                    BlackReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    BlackRemovedReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    NotUseReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transporter", x => x.TransporterCode);
                });

            migrationBuilder.CreateTable(
                name: "Truck",
                columns: table => new
                {
                    VehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: false),
                    UnladenWeight = table.Column<decimal>(type: "Decimal(18,0)", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(max)", nullable: true),
                    Flowrate = table.Column<double>(type: "float", nullable: true),
                    TruckType = table.Column<string>(type: "varchar(30)", nullable: true),
                    Transporter = table.Column<string>(type: "varchar(10)", nullable: true),
                    IsBlack = table.Column<bool>(type: "bit", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    LoadingType = table.Column<string>(type: "varchar(50)", nullable: true),
                    BlackDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BlackRemovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DriverLicenseNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    LastLoadingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    VehicleBackRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    TotalNrDomeCover = table.Column<int>(type: "int", nullable: false),
                    TotalNrLoadCoupling = table.Column<int>(type: "int", nullable: false),
                    TotalNrFootValve = table.Column<int>(type: "int", nullable: false),
                    TotalNrCabinet = table.Column<int>(type: "int", nullable: false),
                    OtherSealPlace = table.Column<string>(type: "varchar(50)", nullable: true),
                    BlackReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    BlackRemovedReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    CompartmentNo = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.VehicleRegNo);
                });

            migrationBuilder.CreateTable(
                name: "TruckType",
                columns: table => new
                {
                    TypeCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(30)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckType", x => x.TypeCode);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6189b896-baaf-42a6-bb5e-8b93f565b05a", null, "WBUser", "WBUSER" },
                    { "89860b07-a585-4614-9f68-586b8d06be6a", null, "Admin", "ADMIN" },
                    { "bb7ee60a-d625-4d56-940e-1fa3a994d8dc", null, "GateUser", "GATEUSER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropTable(
                name: "Trailer");

            migrationBuilder.DropTable(
                name: "TrailerType");

            migrationBuilder.DropTable(
                name: "Transporter");

            migrationBuilder.DropTable(
                name: "Truck");

            migrationBuilder.DropTable(
                name: "TruckType");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
