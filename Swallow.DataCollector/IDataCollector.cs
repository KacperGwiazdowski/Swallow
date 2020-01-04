using Swallow.Core.Domains.CollectedData;
using System.Collections.Generic;

namespace Swallow.DataCollector
{
    public interface IDataCollector
    {
        ICollection<MeasurmentStation> GetStations();

        ICollection<Sensor> GetStationData(int stationId);

        ICollection<DataMeasurment> GetSensorData(int sensorId);

    }
}
