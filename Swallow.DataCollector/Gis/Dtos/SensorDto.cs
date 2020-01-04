using System;
using System.Collections.Generic;
using System.Text;

namespace Swallow.DataCollector.Gis.Dtos
{
    public class SensorDto
    {
        public int Id { get; set; }
        public int StationId { get; set; }
        public ParamDto Param { get; set; }
    }
}
