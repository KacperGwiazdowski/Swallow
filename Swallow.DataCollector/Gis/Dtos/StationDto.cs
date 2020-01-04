using System;
using System.Collections.Generic;
using System.Text;

namespace Swallow.DataCollector.Gis.Dtos
{
    public class StationDto
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public decimal GegrLat { get; set; }
        public decimal GegrLon { get; set; }
    }
}
