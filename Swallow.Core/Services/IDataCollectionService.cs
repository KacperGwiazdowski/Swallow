using Swallow.Core.Domains.CollectedData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swallow.Core.Services
{
    public interface IDataCollectionService
    {
        Task<bool> UpdateStations();
        bool UpdateSensors();

        bool UpdateMeasurments();
    }
}
