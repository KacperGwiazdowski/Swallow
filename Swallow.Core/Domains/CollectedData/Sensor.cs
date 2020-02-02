using Swallow.Core.Domains.Base;
using System.Collections.Generic;

namespace Swallow.Core.Domains.CollectedData
{
    public class Sensor : DomainBase<int>
    {
        public string ParameterName { get; set; }
        public string ChemicalFormula { get; set; }
        ICollection<DataMeasurment> DataMeasurments { get; set; }

        public virtual int MeasurmentStationId { get; set; }
        public virtual MeasurmentStation MeasurmentStation { get; set; }
    }
}
