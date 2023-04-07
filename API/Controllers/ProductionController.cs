using Application.DataProvider;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly IDataService _data;
        private readonly ILogger<ProductionController> _logger;

        public ProductionController(IDataService data, ILogger<ProductionController> logger)
        {
            _data = data;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetCurrentProductionData()
        {
            var data = _data.GetCurrentProductionData();
            return Ok(data);
        }
    }
}
