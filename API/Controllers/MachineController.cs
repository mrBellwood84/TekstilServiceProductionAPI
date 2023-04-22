using Application.DataProvider;
using Microsoft.AspNetCore.Mvc;
using Simulator.Models;

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
        public async Task<ActionResult<List<Machine>>> GetAllMachines()
        {
            var machines = await _data.GetMachines();
            return Ok(machines);
        }
    }
}
