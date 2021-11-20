using Microsoft.EntityFrameworkCore.Migrations;

namespace ITechArtPizzaDelivery.Infrastructure.Migrations
{
    public partial class FixUserCartRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Promocodes_PromocodeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Carts_CartId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "PromocodeId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Promocodes_PromocodeId",
                table: "Orders",
                column: "PromocodeId",
                principalTable: "Promocodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Carts_CartId",
                table: "Users",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Promocodes_PromocodeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Carts_CartId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "PromocodeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Promocodes_PromocodeId",
                table: "Orders",
                column: "PromocodeId",
                principalTable: "Promocodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Carts_CartId",
                table: "Users",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
