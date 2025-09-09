using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.LMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterAuditTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuspendedReason",
                table: "Users",
                newName: "SuspendReason");

            migrationBuilder.RenameColumn(
                name: "SuspendedAtUtc",
                table: "Users",
                newName: "SuspendedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Users",
                newName: "ModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Users",
                newName: "DeletedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Users",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ActivatedAtUtc",
                table: "Users",
                newName: "ActivatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "SubPermissions",
                newName: "ModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "SubPermissions",
                newName: "DeletedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "SubPermissions",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Roles",
                newName: "ModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Roles",
                newName: "DeletedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Roles",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Permissions",
                newName: "ModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Permissions",
                newName: "DeletedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Permissions",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Genres",
                newName: "ModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Genres",
                newName: "DeletedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Genres",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "DefaultPermissions",
                newName: "ModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "DefaultPermissions",
                newName: "DeletedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "DefaultPermissions",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ReturnedAtUtc",
                table: "BorrowedBooks",
                newName: "ReturnedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "BorrowedBooks",
                newName: "ModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "IssuedAtUtc",
                table: "BorrowedBooks",
                newName: "DueOnUtc");

            migrationBuilder.RenameColumn(
                name: "DueAtUtc",
                table: "BorrowedBooks",
                newName: "DeletedOnUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "BorrowedBooks",
                newName: "BorrowedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "BorrowedBooks",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Books",
                newName: "ModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Books",
                newName: "DeletedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Books",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "Authors",
                newName: "ModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "Authors",
                newName: "DeletedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Authors",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                table: "AccessControls",
                newName: "ModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedAtUtc",
                table: "AccessControls",
                newName: "DeletedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "AccessControls",
                newName: "CreatedOnUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuspendedOnUtc",
                table: "Users",
                newName: "SuspendedAtUtc");

            migrationBuilder.RenameColumn(
                name: "SuspendReason",
                table: "Users",
                newName: "SuspendedReason");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                table: "Users",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedOnUtc",
                table: "Users",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Users",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ActivatedOnUtc",
                table: "Users",
                newName: "ActivatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                table: "SubPermissions",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedOnUtc",
                table: "SubPermissions",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "SubPermissions",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                table: "Roles",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedOnUtc",
                table: "Roles",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Roles",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                table: "Permissions",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedOnUtc",
                table: "Permissions",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Permissions",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                table: "Genres",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedOnUtc",
                table: "Genres",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Genres",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                table: "DefaultPermissions",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedOnUtc",
                table: "DefaultPermissions",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "DefaultPermissions",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ReturnedOnUtc",
                table: "BorrowedBooks",
                newName: "ReturnedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                table: "BorrowedBooks",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DueOnUtc",
                table: "BorrowedBooks",
                newName: "IssuedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedOnUtc",
                table: "BorrowedBooks",
                newName: "DueAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "BorrowedBooks",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "BorrowedOnUtc",
                table: "BorrowedBooks",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                table: "Books",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedOnUtc",
                table: "Books",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Books",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                table: "Authors",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedOnUtc",
                table: "Authors",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Authors",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                table: "AccessControls",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "DeletedOnUtc",
                table: "AccessControls",
                newName: "DeletedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "AccessControls",
                newName: "CreatedAtUtc");
        }
    }
}
