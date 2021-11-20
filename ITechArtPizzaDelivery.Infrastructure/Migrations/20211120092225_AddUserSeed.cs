using Microsoft.EntityFrameworkCore.Migrations;

namespace ITechArtPizzaDelivery.Infrastructure.Migrations
{
    public partial class AddUserSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CartId", "FirstName", "LastName", "Role" },
                values: new object[] { 1, null, "test", "user", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
