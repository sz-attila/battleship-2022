﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleship.Migrations
{
    /// <inheritdoc />
    public partial class FullDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "Winner",
               table: "Games",
               type: "nvarchar(max)",
               nullable: false,
               defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Games");
        }
    }
}
