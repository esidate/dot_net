using Microsoft.EntityFrameworkCore.Migrations;

namespace dot_net.Migrations
{
    public partial class addValidatedComlumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Validated",
                table: "Candidatures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Validated",
                table: "Candidatures");
        }
    }
}
