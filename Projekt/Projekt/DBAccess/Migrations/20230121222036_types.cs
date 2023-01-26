using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class types : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Beers");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Breweries",
                type: "TEXT",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "BeerTypeId",
                table: "Beers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BeerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    MinAlcohol = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxAlcohol = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BeerTypeId",
                table: "Beers",
                column: "BeerTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_BeerTypes_BeerTypeId",
                table: "Beers",
                column: "BeerTypeId",
                principalTable: "BeerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_BeerTypes_BeerTypeId",
                table: "Beers");

            migrationBuilder.DropTable(
                name: "BeerTypes");

            migrationBuilder.DropIndex(
                name: "IX_Beers_BeerTypeId",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "BeerTypeId",
                table: "Beers");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Breweries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Beers",
                type: "TEXT",
                nullable: true);
        }
    }
}
