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
            },
             new User
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Username = "b",
                FirstName = "b",
                LastName = "b",
                PasswordHash = "0878CCA545CD1885D6AAAF7117199F0605802867",
                CreationDate = DateTime.Now,
                Email = "aaa",
                TelephoneNumber = "1234",
                UserRole = new UserRole { Name = "RegularUser", IsAccountActive = false }
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
            return _users.Single(x => x.Id.Equals(id));
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
            // throw new NotImplementedException();
            return 1;
        }
    }
}
