﻿using Swallow.Core.Domains.Base;
using System.Collections.Generic;

namespace Swallow.Core.Domains.CollectedData
{
    public class MeasurmentStation : DomainBase<int>
    {
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }


        public virtual ICollection<Sensor> Sensors { get; set; }

        public int ExternalId { get; set; }
        public ExternalDataProvider ExternalDataProvider { get; set; }
    }
}
