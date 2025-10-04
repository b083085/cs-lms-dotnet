using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.LMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterBooksTableAddAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Availability",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Books");
        }
    }
}
