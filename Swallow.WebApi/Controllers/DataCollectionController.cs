using Microsoft.AspNetCore.Mvc;
using Swallow.DataCollector.Gis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swallow.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataCollectionController : Controller
    {


        [HttpGet("test")]
        public void test()
        {
            GisDataCollector dataCollector = new GisDataCollector();
            var aa = dataCollector.GetStations();
            var bb = dataCollector.GetStationData(114);
        }

    }
}
