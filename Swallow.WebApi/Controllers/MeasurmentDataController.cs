using Microsoft.AspNetCore.Mvc;
using Swallow.Core.Repository;
using System;

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
            return Ok(_unitOfWork.Data.GetSinceDate(sinceDate, sensorId));
        }
    }
}
