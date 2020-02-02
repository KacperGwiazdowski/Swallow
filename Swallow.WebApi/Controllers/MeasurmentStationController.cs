using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swallow.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swallow.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MeasurmentStationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MeasurmentStationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(nameof(GetAllStations))]
        public ActionResult GetAllStations()
        {
            return Ok(_unitOfWork.MeasurmentStations.GetAll());
        }

        [HttpGet(nameof(GetById)+"/{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_unitOfWork.MeasurmentStations.Get(id));
        }


    }
}
