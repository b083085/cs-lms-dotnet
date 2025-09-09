namespace Capstone.LMS.Domain.Constants
{
    public abstract class EntityTables
    {
        #region Identity

        public const string Users = "Users";
        public const string Roles = "Roles";
        public const string UserRoles = "UserRoles";
        public const string RoleClaims = "RoleClaims";
        public const string UserClaims = "UserClaims";
        public const string UserLogins = "UserLogins";
        public const string UserTokens = "UserTokens";

        #endregion

        public const string AccessControls = "AccessControls";
        public const string Authors = "Authors";
        public const string Books = "Books";
        public const string BorrowedBooks = "BorrowedBooks";
        public const string Genres = "Genres";
        public const string DefaultPermissions = "DefaultPermissions";
        public const string Permissions = "Permissions";
        public const string SubPermissions = "SubPermissions";
    }
}
