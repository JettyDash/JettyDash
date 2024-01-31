namespace Schemes.Constants;

public static class Constants
{

    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Personnel = "Personnel";
        public const string Guest = "Guest";
        public const string AdminOrPersonnel = "Admin, Personnel";
        public const string AdminOrPersonnelOrGuest = "Admin, Personnel, Guest";
    }

    public static class VaultPath
    {
        public static readonly string Database = "users/{0}/databases/{1}";
    }
    public static class ContentType
    {
        public const string Json = "application/json";
    }
    public static class ClaimTypes
    {
        public const string UserId = "UserId";
        public const string Username = "Username";
        public const string Role = "Role";
    }

    public static class ErrorMessages
    {
        public const string UserIdNotFound = "UserId not found";
        public const string RoleNotFound = "Role not found";
        public const string IdLessThanZero = "Id must be greater than zero";
        public const string CredentialNotFound = "Credential not found";
        public const string InvalidUserInformation = "Invalid user information";
        public const string ContactAdministrator = "Please contact your administrator, your account is locked";
    }

    public static class ValidationMessages
    {
        public const string RequiredMessage = "{0} is required.";
        public static string Required(string fieldName) => string.Format(RequiredMessage, fieldName);
    }

    public static class DateFormats
    {
        public const string DateFormat = "dd/MM/yyyy";
        public const string DateTimeFormat = "dd/MM/yyyy HH:mm:ss";
    }

    public static class UserStatus
    {
        public const string Active = "Active";
        public const string Inactive = "Inactive";
    }
}