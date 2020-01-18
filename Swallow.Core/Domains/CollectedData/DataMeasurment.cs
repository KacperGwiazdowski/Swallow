using Swallow.Core.Domains.Base;

namespace Swallow.Core.Domains.CollectedData
{
    public class DataMeasurment : DomainBase<long>
    {
        public decimal Value { get; set; }

        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }
    }
}
