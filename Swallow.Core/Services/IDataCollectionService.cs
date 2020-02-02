using Swallow.Core.Domains.CollectedData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swallow.Core.Services
{
    public interface IDataCollectionService
    {
        Task<bool> UpdateStations();
        Task<bool> UpdateSensors();

        Task<bool> UpdateMeasurments();
    }
}
