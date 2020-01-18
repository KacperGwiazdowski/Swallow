using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Repository;

namespace Swallow.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IUserRepository userRepository,
            IRepository<MeasurmentStation, int> measurmentStationRepository,
            IRepository<Sensor, int> sensorRepository,
            IRepository<DataMeasurment, long> dataRepository
            )
        {
            Users = userRepository;
            MeasurmentStations = measurmentStationRepository;
            Sensors = sensorRepository;
            Data = dataRepository;
        }
        public IUserRepository Users { get; set ; }
        public IRepository<MeasurmentStation, int> MeasurmentStations { get; set; }
        public IRepository<Sensor, int> Sensors { get; set; }
        public IRepository<DataMeasurment, long> Data { get; set; }

        public void SaveChanges()
        {
            this.Users.SaveChanges();
            this.MeasurmentStations.SaveChanges();
            this.Sensors.SaveChanges();
            this.Data.SaveChanges();
        }
    }
}
