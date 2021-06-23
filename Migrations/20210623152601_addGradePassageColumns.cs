using Microsoft.EntityFrameworkCore.Migrations;

namespace dot_net.Migrations
{
    public partial class addGradePassageColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "Candidatures",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Passage",
                table: "Candidatures",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Candidatures");

            migrationBuilder.DropColumn(
                name: "Passage",
                table: "Candidatures");
        }
    }
}
