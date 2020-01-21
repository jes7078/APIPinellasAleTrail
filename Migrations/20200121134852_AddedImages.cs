using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace APIPinellasAleTrail.Migrations
{
    public partial class AddedImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brewery",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "BreweryURL",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "Beers");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Brewery",
                table: "Beers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BreweryURL",
                table: "Beers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "Beers",
                type: "text",
                nullable: true);
        }
    }
}
