﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Repository;

namespace Swallow.Core.Services
{
    public class DataCollectionService : IDataCollectionService
    {
        private readonly IDataCollector _dataCollector;
        private readonly IUnitOfWork _unitOfWork;
        public DataCollectionService(IDataCollector dataCollector, IUnitOfWork unitOfWork)
        {
            _dataCollector = dataCollector;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UpdateMeasurments()
        {
            var sensors = _unitOfWork.Sensors.GetAll();
            List<DataMeasurment> dataMeasurments = new List<DataMeasurment>();
            foreach (var sensor in sensors)
            {
                dataMeasurments.AddRange(await _dataCollector.GetSensorData(sensor.Id));
            }
            _unitOfWork.Data.AddRange(dataMeasurments);
            _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateSensors()
        {
            var stations = _unitOfWork.MeasurmentStations.GetAll();
            List<Sensor> sensors = await GetSensors(stations);

            var existingSensorsIds = _unitOfWork.Sensors.GetAllIds();

            var sensorsToAdd = sensors.Where(x => !existingSensorsIds.Contains(x.Id)).ToArray();

            if (sensorsToAdd.Any())
            {
                _unitOfWork.Sensors.AddRange(sensorsToAdd);
                _unitOfWork.SaveChanges();
            }

            return true;
        }

        public async Task<bool> UpdateStations()
        {
            var existingStationsIds = _unitOfWork.MeasurmentStations.GetAllIds();
            var stations = await _dataCollector.GetStations();
            var stationsToAdd = stations.Where(x => !existingStationsIds.Contains(x.Id)).ToList();

            if (stationsToAdd.Any())
            {
                _unitOfWork.MeasurmentStations.AddRange(stationsToAdd);
                _unitOfWork.SaveChanges();
            }
            return true;
        }



        private async Task<List<Sensor>> GetSensors(ICollection<MeasurmentStation> stations)
        {
            var sensors = new List<Sensor>();
            foreach (var station in stations)
            {
                var sensorsForStation = await _dataCollector.GetStationData(station.Id);
                sensors.AddRange(sensorsForStation);
            }

            return sensors;
        }


    }
}
