using Application.DataProvider;
using Microsoft.AspNetCore.Mvc;
using Simulator.Models;

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
        public async Task<ActionResult<List<ProductionData>>> GetCurrentProductionData()
        {
            var data = await _data.GetCurrentProductionData();
            return Ok(data);
        }
    }
}
