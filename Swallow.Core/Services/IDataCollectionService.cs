﻿using System.Threading.Tasks;

namespace Swallow.Core.Services
{
    public interface IDataCollectionService
    {
        Task<bool> UpdateStations();
        Task<bool> UpdateSensors();

        bool UpdateMeasurments();
    }
}
