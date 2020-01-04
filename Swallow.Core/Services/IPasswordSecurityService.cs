namespace Swallow.Core.Services
{
    public interface IPasswordSecurityService
    {

        string HashPassword(string password);
        bool ValidatePassword(string password, string passwordHash);
       
    }
}
