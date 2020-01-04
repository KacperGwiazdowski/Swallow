using Swallow.Core.Domains.User;
using Swallow.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swallow.DataAccessLayer
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users = new List<User>()
        {
            new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                FirstName = "Admin",
                LastName = "Admin",
                PasswordHash = "0878CCA545CD1885D6AAAF7117199F0605802867",
                CreationDate = DateTime.Now,
                Email = "aaa",
                TelephoneNumber = "1234",
                UserRole = new UserRole { Name = "Admin", IsAccountActive = true }
            }
        };

        public Guid Add(User instance)
        {
            instance.Id = Guid.NewGuid();
            _users.Add(instance);
            return instance.Id;
        }

        public ICollection<Guid> AddRange(ICollection<User> instanceList)
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetAll()
        {
            return _users;
        }

        public User GetByUsername(string name)
        {
            return _users.Single(x => x.Username.Equals(name));
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
