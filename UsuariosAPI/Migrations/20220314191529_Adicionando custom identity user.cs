using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class Adicionandocustomidentityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "d1652b80-ae71-4707-8c0a-6edf291060a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "4f8bbb69-c411-4e73-9fe6-707bb82907fd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "297cb19a-417e-4c69-a9af-63af602c8333", "AQAAAAEAACcQAAAAEKWDan+KkJG3pJrxQEEYR9oYmC2EtQpjUivlazU6MXaTg9nWpysCW0FKt5wzGLgYrw==", "84927415-919c-4fef-b6ad-53fe37879d1f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "cace32e6-c29d-4f0b-b3e5-17b0ad14e13b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "fb5e33d7-0904-4854-a888-a1622624aa7e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf4e2597-39fc-45fe-b02d-d9dea4f784cf", "AQAAAAEAACcQAAAAEAq1HsMr/NNzwlvQPTbFJsJS5HjyLR5SbvdFy3u+JBEaWn8XgeSCb7tBhL09yYrstQ==", "a18e00aa-d783-46a0-b0ca-fac9960f19a3" });
        }
    }
}
