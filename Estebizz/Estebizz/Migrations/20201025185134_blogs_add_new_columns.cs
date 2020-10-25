using Microsoft.EntityFrameworkCore.Migrations;

namespace Estebizz.Migrations
{
    public partial class blogs_add_new_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlogUrl",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogUrl",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Blogs");
        }
    }
}
