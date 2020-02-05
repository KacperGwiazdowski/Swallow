using Swallow.Core.Domains.CollectedData;

namespace Swallow.Core.Repository
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; set; }
        IRepository<MeasurmentStation, int> MeasurmentStations { get; set; }
        IRepository<Sensor, int> Sensors { get; set; }
        IDataMeasurmentRepository Data { get; set; }

        void SaveChanges();
    }
}
