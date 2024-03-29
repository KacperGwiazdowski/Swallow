﻿using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Swallow.DataAccessLayer
{
    public class MeasurmentStationRepository : IRepository<MeasurmentStation, int>
    {
        private readonly SwallowDataDbContext _context;
        public MeasurmentStationRepository(SwallowDataDbContext context)
        {
            _context = context;
        }
        public int Add(MeasurmentStation instance)
        {
            throw new NotImplementedException();
        }

        public ICollection<int> AddRange(ICollection<MeasurmentStation> instanceList)
        {
            _context.MeasurmentStations.AddRange(instanceList);
            return instanceList.Select(x => x.Id).ToArray();
        }

        public MeasurmentStation Get(int id)
        {
            return _context.MeasurmentStations.SingleOrDefault(x => x.Id == id);
        }

        public ICollection<MeasurmentStation> GetAll()
        {
            return _context.MeasurmentStations.ToArray();
        }

        public ICollection<int> GetAllExternalIds()
        {
            return _context.MeasurmentStations
                 .Select(x => x.ExternalId)
                 .ToArray();
        }

        public ICollection<int> GetAllIds()
        {
            return _context.MeasurmentStations
                .Select(x => x.Id)
                .ToArray();
        }

        public int SaveChanges()
        {

            return _context.SaveChanges();
        }
    }
}
