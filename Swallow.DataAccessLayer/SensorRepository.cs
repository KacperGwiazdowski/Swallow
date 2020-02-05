using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swallow.DataAccessLayer
{
    public class SensorRepository : ISensorRepository
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
            return _context.Sensors.SingleOrDefault(x => x.Id == id);
        }

        public ICollection<Sensor> GetAll()
        {
            return _context.Sensors.ToArray();
        }

        public ICollection<int> GetAllExternalIds()
        {
            return _context.Sensors.Select(x => x.ExternalId).ToArray();
        }

        public ICollection<int> GetAllIds()
        {
            return _context.Sensors.Select(x => x.Id).ToArray();
        }

        public ICollection<Sensor> GetByStationId(int stationId)
        {
            return _context.Sensors.Where(x => x.MeasurmentStationId == stationId).ToList();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
