using Microsoft.EntityFrameworkCore.Migrations;

namespace Trees.WebUI.Data.Migrations
{
    public partial class NodeNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NodeNumber",
                table: "Nodes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NodeNumber",
                table: "Nodes");
        }
    }
}
