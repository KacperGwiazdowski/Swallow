using Microsoft.AspNetCore.Mvc;
using Swallow.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swallow.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SensorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SensorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(nameof(test))]
        public ActionResult test()
        {
            return Ok(_unitOfWork.Sensors.GetAll());
        }









    }
}
