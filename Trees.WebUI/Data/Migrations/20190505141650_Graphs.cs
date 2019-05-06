using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trees.WebUI.Data.Migrations
{
    public partial class Graphs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GraphId",
                table: "Nodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GraphId",
                table: "Edges",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Graphs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graphs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_GraphId",
                table: "Nodes",
                column: "GraphId");

            migrationBuilder.CreateIndex(
                name: "IX_Edges_GraphId",
                table: "Edges",
                column: "GraphId");

            migrationBuilder.AddForeignKey(
                name: "FK_Edges_Graphs_GraphId",
                table: "Edges",
                column: "GraphId",
                principalTable: "Graphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_Graphs_GraphId",
                table: "Nodes",
                column: "GraphId",
                principalTable: "Graphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edges_Graphs_GraphId",
                table: "Edges");

            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_Graphs_GraphId",
                table: "Nodes");

            migrationBuilder.DropTable(
                name: "Graphs");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_GraphId",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Edges_GraphId",
                table: "Edges");

            migrationBuilder.DropColumn(
                name: "GraphId",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "GraphId",
                table: "Edges");
        }
    }
}
