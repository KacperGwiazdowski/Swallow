using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swallow.Core.Services;
using System.Threading.Tasks;

namespace Swallow.WebApi.Controllers
{
    [Authorize(Policy = "RequireAdmin")]
    [ApiController]
    [Route("[controller]")]
    public class DataCollectionController : Controller
    {
        private readonly IDataCollectionService _dataCollectionService;

        public DataCollectionController(IDataCollectionService dataCollectionService)
        {
            _dataCollectionService = dataCollectionService;
        }

        [HttpPost(nameof(CollectData))]
        public async Task<ActionResult> CollectData()
        {
            var c = _dataCollectionService.UpdateMeasurments();
            return Ok();
        }

    }
}
