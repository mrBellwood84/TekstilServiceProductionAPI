using Application.DataProvider;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IDataService _data;
        private readonly ILogger _logger;

        public MachineController(IDataService data, ILogger<MachineController> logger)
        {
            _data = data;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllMachines()
        {
            var machines = _data.GetMachines();
            return Ok(machines);
        }
    }
}
