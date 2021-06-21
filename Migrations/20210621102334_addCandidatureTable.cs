using Microsoft.EntityFrameworkCore.Migrations;

namespace dot_net.Migrations
{
    public partial class addCandidatureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Archived",
                table: "Candidatures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Candidatures");
        }
    }
}
