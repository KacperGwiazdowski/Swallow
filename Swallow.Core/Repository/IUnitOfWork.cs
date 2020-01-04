namespace Swallow.Core.Repository
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; set; }

        void SaveChanges();
    }
}
