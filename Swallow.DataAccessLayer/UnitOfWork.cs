using Swallow.Core.Repository;

namespace Swallow.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IUserRepository userRepository)
        {
            Users = userRepository;
        }
        public IUserRepository Users { get; set ; }

        public void SaveChanges()
        {
            this.Users.SaveChanges();
        }
    }
}
