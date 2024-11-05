using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MallMedia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatescheduleentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Schedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Schedules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
