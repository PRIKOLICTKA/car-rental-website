using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webcrp10._2.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusColumnToZayavki : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Статус",
                table: "ZAYAVKI",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Статус",
                table: "ZAYAVKI");
        }
    }
}
