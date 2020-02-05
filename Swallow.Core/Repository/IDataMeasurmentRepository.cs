using Swallow.Core.Domains.CollectedData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swallow.Core.Repository
{
    public interface IDataMeasurmentRepository : IRepository<DataMeasurment, long>
    {
        Task<ICollection<DataMeasurment>> GetSinceDate(DateTime sinceDate, int sensorId);
    }
}
