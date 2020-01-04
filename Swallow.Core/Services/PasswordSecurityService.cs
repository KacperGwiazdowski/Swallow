using System.Security.Cryptography;
using System.Text;

namespace Swallow.Core.Services
{
    public class PasswordSecurityService : IPasswordSecurityService
    {
        const int Iteration = 1000;
        public string HashPassword(string password)
        {
            string hashedPassword = password;
            for (int i = 0; i < Iteration; i++)
            {
                hashedPassword = Hash(hashedPassword);
            }
            return hashedPassword;
        }

        public bool ValidatePassword(string password, string passwordHash)
        {
            var passwordHashToValidate = HashPassword(password);
            return passwordHash == passwordHashToValidate;
        }

        private string Hash(string password)
        {
            byte[] hash;
            byte[] passwordtBytes = Encoding.UTF8.GetBytes(password);
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                hash = sha1.ComputeHash(passwordtBytes);
            }
            var sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
