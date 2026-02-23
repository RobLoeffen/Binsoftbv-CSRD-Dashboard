using Binsoft.CRSDdashboard.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Binsoft.CRSDdashboard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EcopartsController : ControllerBase
    {
        private readonly IEcopartService _ecopartService;

        public EcopartsController(IEcopartService ecopartService)
        {
            _ecopartService = ecopartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ecoparts = await _ecopartService.GetAllEcopartsAsync();
            return Ok(ecoparts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ecopart = await _ecopartService.GetEcopartByIdAsync(id);
            
            if (ecopart == null)
                return NotFound(new { message = $"Ecopart with ID {id} not found" });

            return Ok(ecopart);
        }

        [HttpGet("{id}/emissions")]
        public async Task<IActionResult> GetWithEmissions(int id)
        {
            try
            {
                var ecopart = await _ecopartService.GetEcopartWithEmissionsAsync(id);

                if (ecopart == null)
                    return NotFound(new { message = $"Ecopart with ID {id} not found" });

                return Ok(ecopart);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
