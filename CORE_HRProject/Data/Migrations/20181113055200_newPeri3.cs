using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CORE_HRProject.Data.Migrations
{
    public partial class newPeri3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "etc",
                table: "PeriWork");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "etc",
                table: "PeriWork",
                nullable: true);
        }
    }
}
