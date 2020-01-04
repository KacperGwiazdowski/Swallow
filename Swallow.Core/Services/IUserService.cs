using Swallow.Core.Domains.User;
using System;
using System.Collections.Generic;

namespace Swallow.Core.Services
{
    public interface IUserService
    {
        User GetUser(Guid userId);
        Guid AddUser(User user);
        KeyValuePair<User, string> SignInAsync(string username, string password);
    }
}
