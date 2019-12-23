using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace APIPinellasAleTrail.Migrations
{
    public partial class AddedFirstTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeerStyle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Style = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StyleURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerStyle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Breweries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breweries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Brewery = table.Column<string>(nullable: true),
                    BreweryURL = table.Column<string>(nullable: true),
                    Style = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BeerURL = table.Column<string>(nullable: true),
                    ABV = table.Column<string>(nullable: true),
                    BeerStyleId = table.Column<int>(nullable: false),
                    BreweriesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beers_BeerStyle_BeerStyleId",
                        column: x => x.BeerStyleId,
                        principalTable: "BeerStyle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beers_Breweries_BreweriesId",
                        column: x => x.BreweriesId,
                        principalTable: "Breweries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BeerStyleId",
                table: "Beers",
                column: "BeerStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BreweriesId",
                table: "Beers",
                column: "BreweriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beers");

            migrationBuilder.DropTable(
                name: "BeerStyle");

            migrationBuilder.DropTable(
                name: "Breweries");
        }
    }
}
