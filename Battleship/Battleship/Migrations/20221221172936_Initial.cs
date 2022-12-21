﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleship.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Player1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rounds = table.Column<int>(type: "int", nullable: false),
                    Player1Hits = table.Column<int>(type: "int", nullable: false),
                    Player2Hits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
