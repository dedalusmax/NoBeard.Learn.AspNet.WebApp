using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoBeard.Learn.AspNet.WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class MovieActivate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Movies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Movies");
        }
    }
}
