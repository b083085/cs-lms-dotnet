using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.LMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterBorrowedBooksReferenceApprover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_ApprovedBy",
                table: "BorrowedBooks",
                column: "ApprovedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Users_ApprovedBy",
                table: "BorrowedBooks",
                column: "ApprovedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Users_ApprovedBy",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_ApprovedBy",
                table: "BorrowedBooks");
        }
    }
}
