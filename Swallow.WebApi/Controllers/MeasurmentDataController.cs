using Microsoft.AspNetCore.Authorization;
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
            return Ok(_unitOfWork.Data.GetSinceDate(sinceDate, sensorId).Select(x => new { id=x.Id, datetime=x.CreationDate, value = x.Value }).OrderBy(x => x.datetime));
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpPatch(nameof(UpdateRecord))]
        public ActionResult UpdateRecord(long id, decimal value)
        {
            _unitOfWork.Data.UpdateRecord(id, value);
            _unitOfWork.SaveChanges();
            return Ok();

        }
    }
}
