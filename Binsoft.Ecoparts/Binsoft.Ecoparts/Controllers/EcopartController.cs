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

        [HttpGet("by-material")]
        public async Task<IActionResult> GetEcopartsByMaterial([FromQuery] string materialName, [FromQuery] string? shapeType)
        {
            try
            {
                var result = await _ecopartService.GetEcopartsByMaterialAsync(materialName, shapeType);

                if (result == null)
                    return NotFound(new { message = $"Material '{materialName}' was not found." });

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEcopartById(Guid id)
        {
            var ecopart = await _ecopartService.GetEcopartByIdAsync(id);
            
            if (ecopart == null)
                return NotFound(new { message = $"Ecopart with ID '{id}' was not found." });

            return Ok(ecopart);
        }
    }
}
