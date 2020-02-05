using Swallow.Core.Domains.CollectedData;
using System;
using System.Collections.Generic;

namespace Swallow.Core.Repository
{
    public interface IDataMeasurmentRepository : IRepository<DataMeasurment, long>
    {
        ICollection<DataMeasurment> GetSinceDate(DateTime sinceDate, int sensorId);
        bool UpdateRecord(long id, decimal value);
    }
}
