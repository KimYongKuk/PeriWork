using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CORE_HRProject.Data.Migrations
{
    public partial class newPeri2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "PeriWork",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "PeriWork",
                type: "nvarchar(20)"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "PeriWork");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PeriWork",
                newName: "id");
        }
    }
}
