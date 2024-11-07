using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MallMedia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDefaultColumnToContents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Contents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Contents");
        }
    }
}
