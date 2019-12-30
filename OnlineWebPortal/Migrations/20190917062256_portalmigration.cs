using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineWebPortal.Migrations
{
    public partial class portalmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StreetName = table.Column<string>(maxLength: 100, nullable: true),
                    StreetNumber = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(nullable: true),
                    Town = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ChurchGroups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupName = table.Column<string>(maxLength: 60, nullable: true),
                    GroupDescription = table.Column<string>(nullable: true),
                    MeetingDay = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(maxLength: 60, nullable: true),
                    LeaderName = table.Column<string>(maxLength: 60, nullable: true),
                    LeaderProfile = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChurchGroups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventDate = table.Column<DateTime>(nullable: false),
                    EventName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RegUsers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 60, nullable: false),
                    LastName = table.Column<string>(maxLength: 60, nullable: false),
                    Username = table.Column<string>(maxLength: 60, nullable: false),
                    Password = table.Column<string>(maxLength: 60, nullable: false),
                    Roles = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: false),
                    DateOFBirth = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    MemType = table.Column<string>(nullable: true),
                    DateOfMembership = table.Column<DateTime>(nullable: false),
                    AddressID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegUsers_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enquiries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnquiryDate = table.Column<DateTime>(nullable: false),
                    EnquiryType = table.Column<string>(maxLength: 60, nullable: false),
                    EnquiryBody = table.Column<string>(nullable: true),
                    RegUser = table.Column<string>(nullable: true),
                    RegUserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enquiries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Enquiries_RegUsers_RegUserID",
                        column: x => x.RegUserID,
                        principalTable: "RegUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaymentType = table.Column<string>(maxLength: 30, nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    RegUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Payments_RegUsers_RegUserID",
                        column: x => x.RegUserID,
                        principalTable: "RegUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegUserChurchGroups",
                columns: table => new
                {
                    RegUserID = table.Column<int>(nullable: false),
                    ChurchGroupID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegUserChurchGroups", x => new { x.RegUserID, x.ChurchGroupID });
                    table.ForeignKey(
                        name: "FK_RegUserChurchGroups_ChurchGroups_ChurchGroupID",
                        column: x => x.ChurchGroupID,
                        principalTable: "ChurchGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegUserChurchGroups_RegUsers_RegUserID",
                        column: x => x.RegUserID,
                        principalTable: "RegUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_RegUserID",
                table: "Enquiries",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RegUserID",
                table: "Payments",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RegUserChurchGroups_ChurchGroupID",
                table: "RegUserChurchGroups",
                column: "ChurchGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_RegUsers_AddressID",
                table: "RegUsers",
                column: "AddressID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enquiries");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RegUserChurchGroups");

            migrationBuilder.DropTable(
                name: "ChurchGroups");

            migrationBuilder.DropTable(
                name: "RegUsers");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
