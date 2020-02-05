using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swallow.DataAccessLayer
{
    public class DataMeasurmentRepository : IDataMeasurmentRepository
    {
        private readonly SwallowDataDbContext _context;

        public DataMeasurmentRepository(SwallowDataDbContext context)
        {
            _context = context;
        }
        public long Add(DataMeasurment instance)
        {
            throw new NotImplementedException();
        }

        public ICollection<long> AddRange(ICollection<DataMeasurment> instanceList)
        {
            _context.DataMeasurments.AddRange(instanceList);
            return instanceList.Select(x => x.Id).ToArray();
        }

        public DataMeasurment Get(long id)
        {
            throw new NotImplementedException();
        }

        public ICollection<DataMeasurment> GetAll()
        {
            return _context.DataMeasurments.ToList();
        }

        public ICollection<long> GetAllExternalIds()
        {
            throw new NotImplementedException();
        }

        public ICollection<long> GetAllIds()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public ICollection<DataMeasurment> GetSinceDate(DateTime sinceDate, int sensorId)
        {
            ICollection<DataMeasurment> data;

            try
            {
                data = _context.DataMeasurments.Where(x => x.SensorId == sensorId && x.CreationDate < sinceDate).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
            return data;
        }
    }
}
