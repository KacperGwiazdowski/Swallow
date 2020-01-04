using Swallow.Core.Domains.User;
using System;

namespace Swallow.Core.Repository
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        User GetByUsername(string name);
    }
}
