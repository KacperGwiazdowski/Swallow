using System;
using System.Collections.Generic;
using System.Text;

namespace Swallow.DataCollector.Gis.Dtos
{
    public class MeasurmentDataDto
    {
        public string Key { get; set; }
        public ICollection<SingleCheckDataDto> Values { get; set; }
    }
}
