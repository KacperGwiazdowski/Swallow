using Swallow.Core.Domains.Base;
using System.Collections.Generic;

namespace Swallow.Core.Repository
{
    public interface IRepository<T, TId> where T : DomainBase<TId>
    {
        ICollection<T> GetAll();
        T Get(TId id);

        TId Add(T instance);
        ICollection<TId> AddRange(ICollection<T> instanceList);

        int SaveChanges();

    }
}
