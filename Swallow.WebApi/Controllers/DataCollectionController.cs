using Microsoft.AspNetCore.Mvc;
using Swallow.Core.Repository;
using Swallow.Core.Services;
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
        private readonly IDataCollectionService _dataCollectionService;

        public DataCollectionController(IDataCollectionService dataCollectionService)
        {
            _dataCollectionService = dataCollectionService;
        }

        [HttpGet("test")]
        public async Task<ActionResult> test()
        {
            var a = await _dataCollectionService.UpdateStations();
            var b = await _dataCollectionService.UpdateSensors();
            //_dataCollectionService.UpdateMeasurments();
            return Ok();
        }

    }
}
