using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swallow.DataAccessLayer
{
    public class SensorRepository : IRepository<Sensor, int>
    {
        private readonly SwallowDataDbContext _context;
        public SensorRepository(SwallowDataDbContext context)
        {
            _context = context;
        }
        public int Add(Sensor instance)
        {
            throw new NotImplementedException();
        }

        public ICollection<int> AddRange(ICollection<Sensor> instanceList)
        {
            _context.Sensors.AddRange(instanceList);
            return instanceList.Select(x => x.Id).ToArray();
        }

        public Sensor Get(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Sensor> GetAll()
        {
            return _context.Sensors.ToArray();
        }

        public ICollection<int> GetAllIds()
        {
            return _context.Sensors.Select(x => x.Id).ToArray();
        }

        public int SaveChanges()
        {
           return _context.SaveChanges();
        }
    }
}
