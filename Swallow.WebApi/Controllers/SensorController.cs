using Microsoft.AspNetCore.Mvc;
using Swallow.Core.Repository;
using Swallow.Core.Services;
using System.Threading.Tasks;

namespace Swallow.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SensorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataCollectionService _dataCollectionService;
        public SensorController(IUnitOfWork unitOfWork, IDataCollectionService dataCollectionService)
        {
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

        [HttpPost(nameof(UpdateStations))]
        public async Task<ActionResult> UpdateStations()
        {
            return Ok(await _dataCollectionService.UpdateSensors());
        }
    }
}
