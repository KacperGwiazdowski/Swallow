using Microsoft.AspNetCore.Mvc;
using Swallow.Core.Repository;
using System;
using System.Linq;

namespace Swallow.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasurmentDataController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MeasurmentDataController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(nameof(GetSinceDate))]
        public ActionResult GetSinceDate(DateTime sinceDate, int sensorId)
        {
            return Ok(_unitOfWork.Data.GetSinceDate(sinceDate, sensorId).Select(x => new { datetime=x.CreationDate, value = x.Value }));
        }
    }
}
