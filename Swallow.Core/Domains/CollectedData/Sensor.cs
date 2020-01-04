using Swallow.Core.Domains.Base;
using System.Collections.Generic;

namespace Swallow.Core.Domains.CollectedData
{
    public class Sensor : DomainBase<int>
    {
        public string ParameterName { get; set; }
        public string ChemicalFormula { get; set; }
        public MeasurmentStation MeasurmentStation { get; set; }
        ICollection<DataMeasurment> DataMeasurments { get; set; }
    }
}
