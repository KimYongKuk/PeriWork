using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CORE_HRProject.Data.Migrations
{
    public partial class newPeri_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalRestDay",
                table: "PeriWork");

            migrationBuilder.DropColumn(
                name: "usedRestDay",
                table: "PeriWork");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "totalRestDay",
                table: "PeriWork",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usedRestDay",
                table: "PeriWork",
                nullable: false,
                defaultValue: 0);
        }
    }
}
