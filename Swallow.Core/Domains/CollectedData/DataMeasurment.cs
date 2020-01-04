using Swallow.Core.Domains.Base;

namespace Swallow.Core.Domains.CollectedData
{
    public class DataMeasurment : DomainBase<long>
    {
        public decimal Value { get; set; }
    }
}
