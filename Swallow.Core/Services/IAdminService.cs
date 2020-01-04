using Swallow.Core.Domains.User;
using System;
using System.Collections.Generic;

namespace Swallow.Core.Services
{
    public interface IAdminService
    {
        void ActivateUserAccount(Guid userId);
        ICollection<User> GetAllUsers();
    }
}
