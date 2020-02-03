using Swallow.Core.Domains.Base;

namespace Swallow.Core.Domains.CollectedData
{
    public class DataMeasurment : DomainBase<long>
    {
        public decimal? Value { get; set; }

        public virtual int SensorId { get; set; }
        public virtual Sensor Sensor { get; set; }
    }
}
