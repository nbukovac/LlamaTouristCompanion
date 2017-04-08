using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LlamasTouristCompanion.Migrations
{
    public partial class migracija3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Reservations");

            migrationBuilder.CreateTable(
                name: "BotCaches",
                columns: table => new
                {
                    BotCacheId = table.Column<Guid>(nullable: false),
                    Answer = table.Column<string>(nullable: false),
                    ApartmentId = table.Column<Guid>(nullable: false),
                    Keyword = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotCaches", x => x.BotCacheId);
                    table.ForeignKey(
                        name: "FK_BotCaches_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "ApartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    PriceId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    ApartmentId = table.Column<Guid>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.PriceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BotCaches_ApartmentId",
                table: "BotCaches",
                column: "ApartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotCaches");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Reservations",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
