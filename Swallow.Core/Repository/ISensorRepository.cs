using Swallow.Core.Domains.CollectedData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swallow.Core.Repository
{
    public interface ISensorRepository : IRepository<Sensor, int>
    {
        ICollection<Sensor> GetByStationId(int stationId);
    }
}
