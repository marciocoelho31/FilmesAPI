using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class Criandoroleregular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "fb5e33d7-0904-4854-a888-a1622624aa7e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99998, "cace32e6-c29d-4f0b-b3e5-17b0ad14e13b", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf4e2597-39fc-45fe-b02d-d9dea4f784cf", "AQAAAAEAACcQAAAAEAq1HsMr/NNzwlvQPTbFJsJS5HjyLR5SbvdFy3u+JBEaWn8XgeSCb7tBhL09yYrstQ==", "a18e00aa-d783-46a0-b0ca-fac9960f19a3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "aadb326c-6a28-45a5-b9f1-80484e620943");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7337600-d667-4840-b09c-65641f75d3fd", "AQAAAAEAACcQAAAAEKqEe/ft65MOmQw7Qm5H/P03hoVcznS+o7+nb39ufylarAlRYZoTDSRnu9zXG+HOug==", "892d5e46-a228-4033-ad05-3cc850bc529b" });
        }
    }
}
