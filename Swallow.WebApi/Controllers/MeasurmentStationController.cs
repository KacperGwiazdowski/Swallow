﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swallow.Core.Repository;
using Swallow.Core.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Swallow.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MeasurmentStationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataCollectionService _dataCollectionService;
        public MeasurmentStationController(IUnitOfWork unitOfWork, IDataCollectionService dataCollectionService)
        {
            _dataCollectionService = dataCollectionService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet(nameof(GetAllStations))]
        public ActionResult GetAllStations()
        {
            return Ok(_unitOfWork.MeasurmentStations.GetAll().Select(x => new { id = x.Id, name = x.Name}));
        }

        [HttpGet(nameof(GetById) + "/{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_unitOfWork.MeasurmentStations.Get(id));
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpPost(nameof(UpdateStations))]
        public async Task<ActionResult> UpdateStations()
        {
            return Ok(await _dataCollectionService.UpdateStations());
        }


    }
}
