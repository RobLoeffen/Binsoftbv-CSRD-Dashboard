using Binsoft.Ecoparts.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Binsoft.Ecoparts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcopartController : ControllerBase
    {
        private readonly IEcopartService _ecopartService;

        public EcopartController(IEcopartService ecopartService)
        {
            _ecopartService = ecopartService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEcoparts()
        {
            var ecoparts = await _ecopartService.GetAllEcopartsAsync();
            return Ok(ecoparts);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEcopartById(Guid id)
        {
            var ecopart = await _ecopartService.GetEcopartByIdAsync(id);
            
            if (ecopart == null)
                return NotFound(new { message = $"Ecopart with ID '{id}' was not found." });

            return Ok(ecopart);
        }
    }
}
