using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swallow.WebApi.Models
{
    public class SensorDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string ParameterName { get; set; }
        public string ChemicalFormula { get; set; }
    }
}
