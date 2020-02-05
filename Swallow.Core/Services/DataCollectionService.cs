using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swallow.Core.Services
{
    public class DataCollectionService : IDataCollectionService
    {
        const int DaysToFilterData = 2;
        private readonly IDataCollector _dataCollector;
        private readonly IUnitOfWork _unitOfWork;
        public DataCollectionService(IDataCollector dataCollector, IUnitOfWork unitOfWork)
        {
            _dataCollector = dataCollector;
            _unitOfWork = unitOfWork;
        }

        public bool UpdateMeasurments()
        {
            var sensors = _unitOfWork.Sensors.GetAll();
            List<DataMeasurment> dataMeasurments = new List<DataMeasurment>();
            foreach (var sensor in sensors)
            {
                var rawDataFromApi = _dataCollector.GetSensorData(sensor.Id, sensor.ExternalId).Result;
                var lastDayData = GetLastDayData(rawDataFromApi);
                var dataToAdd = GetFilteredData(lastDayData, sensor.Id);
                dataMeasurments.AddRange(dataToAdd);
            }
            _unitOfWork.Data.AddRange(dataMeasurments);
            _unitOfWork.SaveChanges();
            return true;
        }

        private ICollection<DataMeasurment> GetFilteredData(ICollection<DataMeasurment> dataMeasurments, int sensorId)
        {
            var dataFromDb = _unitOfWork.Data.GetSinceDate(DateTime.Now.AddDays(-DaysToFilterData), sensorId);
            var newRecords = new List<DataMeasurment>();

            foreach (var record in dataMeasurments)
            {
                var recordFromDb = dataFromDb.Where(x => x.CreationDate == record.CreationDate);
                if (!recordFromDb.Any())
                {
                    newRecords.Add(record);
                }
                else if (recordFromDb.Single().Value == null)
                {
                    recordFromDb.Single().Value = record.Value;
                }
            }
            return newRecords;
        }

        private ICollection<DataMeasurment> GetLastDayData(ICollection<DataMeasurment> dataMeasurments)
        {
            return dataMeasurments.Where(x => x.CreationDate > DateTime.Now.AddDays(-DaysToFilterData)).ToList();
        }

        public async Task<bool> UpdateSensors()
        {
            var stations = _unitOfWork.MeasurmentStations.GetAll();
            List<Sensor> sensors = await GetSensors(stations);

            var existingSensorsIds = _unitOfWork.Sensors.GetAllExternalIds();

            var sensorsToAdd = sensors.Where(x => !existingSensorsIds.Contains(x.ExternalId)).ToArray();

            if (sensorsToAdd.Any())
            {
                _unitOfWork.Sensors.AddRange(sensorsToAdd);
                _unitOfWork.SaveChanges();
            }
            return true;
        }

        public async Task<bool> UpdateStations()
        {
            var existingStationsIds = _unitOfWork.MeasurmentStations.GetAllExternalIds();
            var stations = await _dataCollector.GetStations();
            var stationsToAdd = stations.Where(x => !existingStationsIds.Contains(x.ExternalId)).ToList();

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
                var sensorsForStation = await _dataCollector.GetStationData(station.Id, station.ExternalId);

                sensors.AddRange(sensorsForStation);
            }

            return sensors;
        }



    }
}
