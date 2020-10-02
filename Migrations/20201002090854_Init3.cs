using Microsoft.EntityFrameworkCore.Migrations;

namespace aspcore_watchshop.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Promtion",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Promotion",
                table: "Orders",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Promotion",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Promtion",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
