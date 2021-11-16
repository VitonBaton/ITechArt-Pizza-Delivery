using Microsoft.EntityFrameworkCore.Migrations;

namespace ITechArtPizzaDelivery.Infrastructure.Migrations
{
    public partial class AddCartsPizzasTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Pizzas_PizzaId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_PizzaId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "PizzaCount",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "Carts");

            migrationBuilder.CreateTable(
                name: "carts_pizzas",
                columns: table => new
                {
                    CartId = table.Column<long>(type: "bigint", nullable: false),
                    PizzaId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts_pizzas", x => new { x.CartId, x.PizzaId });
                    table.ForeignKey(
                        name: "FK_carts_pizzas_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carts_pizzas_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carts_pizzas_PizzaId",
                table: "carts_pizzas",
                column: "PizzaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carts_pizzas");

            migrationBuilder.AddColumn<int>(
                name: "PizzaCount",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "PizzaId",
                table: "Carts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_PizzaId",
                table: "Carts",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Pizzas_PizzaId",
                table: "Carts",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
