﻿using Swallow.Core.Domains.CollectedData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swallow.Core.Services
{
    public interface IDataCollector
    {
        Task<ICollection<MeasurmentStation>> GetStations();

        Task<ICollection<Sensor>> GetStationData(int stationId);

        Task<ICollection<DataMeasurment>> GetSensorData(int sensorId);

    }
}
