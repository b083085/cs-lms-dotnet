using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.LMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterBorrowedBooksAddApprovedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                table: "BorrowedBooks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOnUtc",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "ApprovedOnUtc",
                table: "BorrowedBooks");
        }
    }
}
