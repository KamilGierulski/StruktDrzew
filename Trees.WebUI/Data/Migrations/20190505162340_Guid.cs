using Microsoft.EntityFrameworkCore.Migrations;

namespace Trees.WebUI.Data.Migrations
{
    public partial class Guid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NodeNumber",
                table: "Nodes");

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Nodes",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "To",
                table: "Edges",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "From",
                table: "Edges",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Nodes");

            migrationBuilder.AddColumn<int>(
                name: "NodeNumber",
                table: "Nodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "To",
                table: "Edges",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "From",
                table: "Edges",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
