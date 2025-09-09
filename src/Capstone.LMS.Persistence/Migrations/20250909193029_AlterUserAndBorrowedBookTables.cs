using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.LMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterUserAndBorrowedBookTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActivatedAtUtc",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActiveStatus",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BorrowingLimit",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SuspendedAtUtc",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuspendedReason",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssuedAtUtc",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueAtUtc",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "BookCondition",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivatedAtUtc",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ActiveStatus",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BorrowingLimit",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SuspendedAtUtc",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SuspendedReason",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BookCondition",
                table: "BorrowedBooks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssuedAtUtc",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueAtUtc",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
