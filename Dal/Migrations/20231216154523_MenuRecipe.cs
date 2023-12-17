using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class MenuRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Menus_MenuId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_MenuId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Recipes");

            migrationBuilder.CreateTable(
                name: "MenuRecipe",
                columns: table => new
                {
                    MenusMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipesRecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRecipe", x => new { x.MenusMenuId, x.RecipesRecipeId });
                    table.ForeignKey(
                        name: "FK_MenuRecipe_Menus_MenusMenuId",
                        column: x => x.MenusMenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuRecipe_Recipes_RecipesRecipeId",
                        column: x => x.RecipesRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuRecipe_RecipesRecipeId",
                table: "MenuRecipe",
                column: "RecipesRecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuRecipe");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "Recipes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_MenuId",
                table: "Recipes",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Menus_MenuId",
                table: "Recipes",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "MenuId");
        }
    }
}
