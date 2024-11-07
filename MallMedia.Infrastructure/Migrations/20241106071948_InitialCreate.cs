using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MallMedia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "TimeFrameId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeFrameId",
                table: "Schedules");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
