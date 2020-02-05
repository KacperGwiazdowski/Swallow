using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swallow.Core.Repository;
using Swallow.Core.Services;
using Swallow.DataCollector.Gis.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swallow.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SensorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataCollectionService _dataCollectionService;
        private readonly IMapper _mapper;
        public SensorController(IUnitOfWork unitOfWork, IDataCollectionService dataCollectionService, IMapper mapper)
        {
            _mapper = mapper;
            _dataCollectionService = dataCollectionService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet(nameof(GetAllSensors))]
        public ActionResult GetAllSensors()
        {
            return Ok(_unitOfWork.Sensors.GetAll());
        }

        [HttpGet(nameof(GetById) + "/{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_unitOfWork.Sensors.Get(id));
        }

        [HttpGet(nameof(GetByStationId) + "/{id}")]
        public ActionResult GetByStationId(int id)
        {
            var sensorsToMap = _unitOfWork.Sensors.GetByStationId(id);

            //var sensorsToReturn = _mapper.Map<ICollection<SensorDto>>(sensorsToMap);

            return Ok(sensorsToMap.Select(x => new { id = x.Id, param = x.ParameterName }));
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpPost(nameof(UpdateSensors))]
        public async Task<ActionResult> UpdateSensors()
        {
            return Ok(await _dataCollectionService.UpdateSensors());
        }
    }
}
