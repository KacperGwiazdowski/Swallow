using System;
using System.Collections.Generic;
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

        public bool UpdateMeasurments()
        {
            var sensors = _unitOfWork.Sensors.GetAll();
            List<DataMeasurment> dataMeasurments = new List<DataMeasurment>();
            foreach (var sensor in sensors)
            {
                dataMeasurments.AddRange(_dataCollector.GetSensorData(sensor.Id));
            }
            _unitOfWork.Data.AddRange(dataMeasurments);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateSensors()
        {
            var stations = _unitOfWork.MeasurmentStations.GetAll();
            List<Sensor> sensors = GetSensors(stations);
            _unitOfWork.Sensors.AddRange(sensors);
            _unitOfWork.SaveChanges();

            return true;
        }

        public async Task<bool> UpdateStations()
        {
            var stations = await _dataCollector.GetStations();
            _unitOfWork.MeasurmentStations.AddRange(stations);
            _unitOfWork.SaveChanges();

            return true;
        }



        private List<Sensor> GetSensors(ICollection<MeasurmentStation> stations)
        {
            var sensors = new List<Sensor>();
            foreach (var station in stations)
            {
                sensors.AddRange(_dataCollector.GetStationData(station.Id));
            }

            return sensors;
        }


    }
}
