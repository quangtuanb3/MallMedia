using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MallMedia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDefaultToContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDefault",
                table: "Contents",
                newName: "IsDefault");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDefault",
                table: "Contents",
                newName: "isDefault");
        }
    }
}
