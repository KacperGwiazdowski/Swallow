using Swallow.Core.Domains.Base;
using System.Collections.Generic;

namespace Swallow.Core.Domains.CollectedData
{
    public class MeasurmentStation: DomainBase<int>
    {
        public string Name { get; set; }
        public Coordinates Coordinates { get; set; }
        ICollection<Sensor> Sensors { get; set; }
    }
}
