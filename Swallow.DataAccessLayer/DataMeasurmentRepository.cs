using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swallow.DataAccessLayer
{
    public class DataMeasurmentRepository : IRepository<DataMeasurment, long>
    {
        //private readonly SwallowCollectedDataDbContext context;
        public long Add(DataMeasurment instance)
        {
            throw new NotImplementedException();
        }

        public ICollection<long> AddRange(ICollection<DataMeasurment> instanceList)
        {
            //_dbMock.AddRange(instanceList);
            return instanceList.Select(x => x.Id).ToArray();
        }

        public DataMeasurment Get(long id)
        {
            throw new NotImplementedException();
        }

        public ICollection<DataMeasurment> GetAll()
        {
            return null;
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
