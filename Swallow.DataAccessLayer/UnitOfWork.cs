using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Repository;

namespace Swallow.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IUserRepository userRepository,
            IRepository<MeasurmentStation, int> measurmentStationRepository,
            ISensorRepository sensorRepository,
            IDataMeasurmentRepository dataRepository
            )
        {
            Users = userRepository;
            MeasurmentStations = measurmentStationRepository;
            Sensors = sensorRepository;
            Data = dataRepository;
        }
        public IUserRepository Users { get; set; }
        public IRepository<MeasurmentStation, int> MeasurmentStations { get; set; }
        public ISensorRepository Sensors { get; set; }
        public IDataMeasurmentRepository Data { get; set; }

        public void SaveChanges()
        {
            this.Users.SaveChanges();
            this.MeasurmentStations.SaveChanges();
            this.Sensors.SaveChanges();
            this.Data.SaveChanges();
        }
    }
}
