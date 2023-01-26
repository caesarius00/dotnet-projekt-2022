using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_BeerTypes_BeerTypeId",
                table: "Beers");

            migrationBuilder.AlterColumn<int>(
                name: "BeerTypeId",
                table: "Beers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "BeerRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeerUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    NickName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    BeerRoleId = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeerUsers_BeerRoles_BeerRoleId",
                        column: x => x.BeerRoleId,
                        principalTable: "BeerRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BeerReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BeerId = table.Column<int>(type: "INTEGER", nullable: false),
                    BeerUserId = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Review = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeerReviews_BeerUsers_BeerUserId",
                        column: x => x.BeerUserId,
                        principalTable: "BeerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerReviews_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeerReviews_BeerId",
                table: "BeerReviews",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_BeerReviews_BeerUserId",
                table: "BeerReviews",
                column: "BeerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BeerUsers_BeerRoleId",
                table: "BeerUsers",
                column: "BeerRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_BeerTypes_BeerTypeId",
                table: "Beers",
                column: "BeerTypeId",
                principalTable: "BeerTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_BeerTypes_BeerTypeId",
                table: "Beers");

            migrationBuilder.DropTable(
                name: "BeerReviews");

            migrationBuilder.DropTable(
                name: "BeerUsers");

            migrationBuilder.DropTable(
                name: "BeerRoles");

            migrationBuilder.AlterColumn<int>(
                name: "BeerTypeId",
                table: "Beers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_BeerTypes_BeerTypeId",
                table: "Beers",
                column: "BeerTypeId",
                principalTable: "BeerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
