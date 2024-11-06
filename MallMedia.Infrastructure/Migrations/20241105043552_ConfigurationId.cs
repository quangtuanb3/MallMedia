using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MallMedia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfigurationId",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Configuration_Id",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfigurationId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Configuration_Id",
                table: "Devices");
        }
    }
}
