using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TMS_Api.Migrations
{
    /// <inheritdoc />
    public partial class InitData : Migration
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
                    TransporterCode = table.Column<string>(type: "varchar(25)", nullable: true),
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
                name: "Gate",
                columns: table => new
                {
                    GateID = table.Column<string>(type: "varchar(25)", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", nullable: true),
                    YardID = table.Column<string>(type: "varchar(25)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gate", x => x.GateID);
                });

            migrationBuilder.CreateTable(
                name: "Trailer",
                columns: table => new
                {
                    VehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: false),
                    ContainerType = table.Column<string>(type: "varchar(25)", nullable: true),
                    ContainerSize = table.Column<int>(type: "int", nullable: true),
                    TransporterCode = table.Column<string>(type: "varchar(25)", nullable: true),
                    TrailerWeight = table.Column<decimal>(type: "Decimal(18,5)", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(max)", nullable: true),
                    IsBlack = table.Column<bool>(type: "bit", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    DriverLicenseNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    LastPassedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    VehicleBackRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
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
                    table.PrimaryKey("PK_Trailer", x => x.VehicleRegNo);
                });

            migrationBuilder.CreateTable(
                name: "TransporterType",
                columns: table => new
                {
                    TypeID = table.Column<string>(type: "varchar(25)", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransporterType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Truck",
                columns: table => new
                {
                    VehicleRegNo = table.Column<string>(type: "varchar(25)", nullable: false),
                    ContainerType = table.Column<string>(type: "varchar(25)", nullable: true),
                    ContainerSize = table.Column<int>(type: "int", nullable: true),
                    TruckWeight = table.Column<decimal>(type: "decimal(18,5)", nullable: true),
                    TypeID = table.Column<string>(type: "varchar(30)", nullable: true),
                    TransporterCode = table.Column<string>(type: "varchar(25)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    DriverLicenseNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    LastPassedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    VehicleBackRegNo = table.Column<string>(type: "varchar(25)", nullable: true),
                    IsBlack = table.Column<bool>(type: "bit", nullable: true),
                    BlackDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BlackRemovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BlackReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    BlackRemovedReason = table.Column<string>(type: "varchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(max)", nullable: true),
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
                name: "TruckEntryType",
                columns: table => new
                {
                    TypeID = table.Column<string>(type: "varchar(25)", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckEntryType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "TruckJobType",
                columns: table => new
                {
                    TypeID = table.Column<string>(type: "varchar(25)", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckJobType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "TruckType",
                columns: table => new
                {
                    TypeID = table.Column<string>(type: "varchar(25)", nullable: false),
                    Description = table.Column<string>(type: "varchar(30)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "WeightBridge",
                columns: table => new
                {
                    WeightBridgeID = table.Column<string>(type: "varchar(25)", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", nullable: true),
                    GateID = table.Column<string>(type: "varchar(25)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightBridge", x => x.WeightBridgeID);
                });

            migrationBuilder.CreateTable(
                name: "Yard",
                columns: table => new
                {
                    YardID = table.Column<string>(type: "varchar(25)", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yard", x => x.YardID);
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
                    { "4748e16c-642e-4aff-9cf1-db0b86d523f1", null, "Admin", "ADMIN" },
                    { "898546bc-8ce0-467f-91bf-54a7ed88e818", null, "GateUser", "GATEUSER" },
                    { "e5d54789-70aa-414a-84e0-894857bd8e2d", null, "WBUser", "WBUSER" }
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
                name: "Gate");

            migrationBuilder.DropTable(
                name: "Trailer");

            migrationBuilder.DropTable(
                name: "TransporterType");

            migrationBuilder.DropTable(
                name: "Truck");

            migrationBuilder.DropTable(
                name: "TruckEntryType");

            migrationBuilder.DropTable(
                name: "TruckJobType");

            migrationBuilder.DropTable(
                name: "TruckType");

            migrationBuilder.DropTable(
                name: "WeightBridge");

            migrationBuilder.DropTable(
                name: "Yard");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
