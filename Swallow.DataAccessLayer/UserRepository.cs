using Swallow.Core.Domains.User;
using Swallow.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swallow.DataAccessLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly SwallowDataDbContext _context;
        public UserRepository(SwallowDataDbContext context)
        {
            _context = context;
        }

        public Guid Add(User instance)
        {
            instance.Id = Guid.NewGuid();
            _context.Users.Add(instance);
            return instance.Id;
        }

        public ICollection<Guid> AddRange(ICollection<User> instanceList)
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            return _context.Users.Single(x => x.Id.Equals(id));
        }

        public ICollection<User> GetAll()
        {
            return _context.Users.ToArray();
        }

        public ICollection<Guid> GetAllIds()
        {
            throw new NotImplementedException();
        }

        public User GetByUsername(string name)
        {
            return _context.Users.Single(x => x.Username.Equals(name));
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
