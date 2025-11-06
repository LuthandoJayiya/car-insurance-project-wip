using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLJ_CarInsuranceMS_RESTAPI.Migrations
{
    public partial class initial : Migration
    {
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
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(150)", nullable: true),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                    { "290d17b6-0a93-4800-a5aa-957c3996c916", null, "RepairShop", "REPAIRSHOP" },
                    { "89e19ada-ca96-4854-8d54-c40ad04df6d6", null, "Administrator", "ADMINISTRATOR" },
                    { "8f126a21-98e1-41cd-af74-596d51449489", null, "InsuranceAgent", "INSURANCEAGENT" },
                    { "9ce59f37-adec-4762-999c-c9312e347816", null, "PotentialCustomer", "POTENTIALCUSTOMER" },
                    { "a086a404-1d39-46fa-ba83-01ff79ef152b", null, "ClaimSurveyor", "CLAIMSURVEYOR" },
                    { "c24097dd-392e-43df-adc3-afba803404ed", null, "Driver", "DRIVER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LicenseNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "339ce2d1-c665-4b77-8b02-fbb76ced6d59", 0, "d33417e5-258f-4f5e-9ebc-655a1593db38", "ApplicationUser", "Surveyor_1@cims.gmail.com", true, "Lani", "KL1617", false, null, null, "SLANI", "AQAAAAIAAYagAAAAECZhCNgpN8VT1qbodXTDOfXMtBeV7wwzC9jc7SAHTZ7K2MDLs7mTmZ47Gpr3/3WesQ==", "212-797-2569", true, "92f13575-b783-45c0-99a8-98e6589ebb79", false, "SLani" },
                    { "42ab0ca5-ed74-4834-a2d8-4135fe765114", 0, "6ab1b66f-b38f-4aef-8561-5399a32089d6", "ApplicationUser", "john123@gmail.com", true, "John", "WK1647", false, null, null, "CJOHN", "AQAAAAIAAYagAAAAEITPNj8Olk+NOFIUbrdiXrEWm5u3+5pzklE+YuN+8UDwjvrCChvJVQDLSIJ0+V4CMQ==", "027-816-3587", true, "94fdf1d4-deca-4df0-9929-e8486ce4711d", false, "CJohn" },
                    { "686de8e1-5d8d-4086-acca-119422f8f438", 0, "19b17137-9812-46c0-b613-2d14c1c7469b", "ApplicationUser", "Insurance_1Agent@cims.gmail.com", true, "Neil", "GP1234", false, null, null, "AGENTNEIL", "AQAAAAIAAYagAAAAEMtDQVUegiGyU0mnK9nnX3VvNERuhyo1Cp/8ucS9rjP5+FZJVezljIDC5rOfPHqYqQ==", "026-548-9752", true, "cb60e340-fd76-410b-8eec-51d8b81ec9b0", false, "AgentNeil" },
                    { "8ee2e870-e891-4726-b9fa-4f8518d8ee24", 0, "350f9c3e-42f8-4edf-95fa-98b7ebdc76c3", "ApplicationUser", "Driver_1@cims.gmail.com", true, "Seth", "SA8976", false, null, null, "DSETH", "AQAAAAIAAYagAAAAEBw/nvjT0lMjHa4BwKt2vBGdjMH5SXqHXK1VnapkVELl8RKHyDu/e+GRP77x8ez48Q==", "086-765-4548", true, "01c4ee58-1d47-409c-b4c2-c7f71c431cb4", false, "DSeth" },
                    { "c910c7cf-50a8-4ebf-b1b9-1a4d0a7b40c4", 0, "fac68621-65ce-4452-99fc-d2a33381feeb", "ApplicationUser", "tjauto@gmail.com", true, "TJ Auto Repairs", "TJ5655", false, null, null, "TJREPAIRS", "AQAAAAIAAYagAAAAEJntlz+y49ODCyTuuES6vpbc/Hf2ik8+NXVLj/l4ir9e/MdbC7Rk5wYiE8HjD7AFBA==", "057-643-4654", true, "cd779fdc-fcac-4621-a4ba-dc040f522fc1", false, "TJRepairs" },
                    { "e7c6545b-4ee0-4220-8a00-c679bb394123", 0, "ce526088-0c6c-44f8-8329-b482cc817724", "ApplicationUser", "Admin_1@cims.gmail.com", true, "CarInsuranceMSAdmin", "CA1207", false, null, null, "HIGHLORDCIMSADMIN", "AQAAAAIAAYagAAAAEL0GHuBrNNLcDxZYaucZl/Uc3WFWybctJ5SvFMcDYYt6bEXpoYXxkPdExwXSw14pUQ==", "025-897-6314", true, "58c20601-0714-4508-9d7c-f8b8c9b2fe17", false, "HighLordCIMSAdmin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "a086a404-1d39-46fa-ba83-01ff79ef152b", "339ce2d1-c665-4b77-8b02-fbb76ced6d59" },
                    { "9ce59f37-adec-4762-999c-c9312e347816", "42ab0ca5-ed74-4834-a2d8-4135fe765114" },
                    { "8f126a21-98e1-41cd-af74-596d51449489", "686de8e1-5d8d-4086-acca-119422f8f438" },
                    { "c24097dd-392e-43df-adc3-afba803404ed", "8ee2e870-e891-4726-b9fa-4f8518d8ee24" },
                    { "290d17b6-0a93-4800-a5aa-957c3996c916", "c910c7cf-50a8-4ebf-b1b9-1a4d0a7b40c4" },
                    { "89e19ada-ca96-4854-8d54-c40ad04df6d6", "e7c6545b-4ee0-4220-8a00-c679bb394123" }
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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
