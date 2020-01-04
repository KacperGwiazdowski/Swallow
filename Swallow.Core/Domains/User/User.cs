using Swallow.Core.Domains.Base;
using System;

namespace Swallow.Core.Domains.User
{
    public class User : DomainBase<Guid>
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string PasswordHash { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}