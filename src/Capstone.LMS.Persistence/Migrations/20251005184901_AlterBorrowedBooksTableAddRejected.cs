using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.LMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterBorrowedBooksTableAddRejected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RejectedBy",
                table: "BorrowedBooks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedOnUtc",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectedReason",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectedBy",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "RejectedOnUtc",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "RejectedReason",
                table: "BorrowedBooks");
        }
    }
}
