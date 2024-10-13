using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webcrp10._2.Migrations
{
    /// <inheritdoc />
    public partial class UsZaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ZAYAVKI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_avto = table.Column<int>(type: "int", nullable: false),
                    ID_user = table.Column<int>(type: "int", nullable: false),
                    Дата_Начала_Аренды = table.Column<DateOnly>(type: "date", nullable: false),
                    Дата_Конца_Аренды = table.Column<DateOnly>(type: "date", nullable: false),
                    Время_Вывоза = table.Column<TimeSpan>(type: "time", nullable: false),
                    Время_Возврата = table.Column<TimeSpan>(type: "time", nullable: false),
                    Получение_авто = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Сдача_авто = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Фамилия = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Имя = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Отчество = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Телефон = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZAYAVKI", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "ZAYAVKI");
        }
    }
}
