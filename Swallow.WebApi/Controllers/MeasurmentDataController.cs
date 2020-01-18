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
    public class MeasurmentDataController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MeasurmentDataController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(nameof(test))]
        public ActionResult test()
        {
            return Ok(_unitOfWork.Data.GetAll());
        }
    }
}
