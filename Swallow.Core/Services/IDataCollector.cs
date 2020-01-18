using Swallow.Core.Domains.CollectedData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swallow.Core.Services
{
    public interface IDataCollector
    {
        Task<ICollection<MeasurmentStation>> GetStations();

        ICollection<Sensor> GetStationData(int stationId);

        ICollection<DataMeasurment> GetSensorData(int sensorId);

    }
}
