using Binsoft.Ecoparts.Application.Requests;
using Binsoft.Ecoparts.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Binsoft.Ecoparts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMaterials()
        {
            var materials = await _materialService.GetAllMaterialsAsync();
            return Ok(materials);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMaterialById(Guid id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);

            if (material is null)
                return NotFound(new { message = $"Material with ID '{id}' was not found." });

            return Ok(material);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaterial([FromBody] CreateMaterialRequest request)
        {
            try
            {
                var material = await _materialService.AddMaterialAsync(request);
                return CreatedAtAction(nameof(GetMaterialById), new { id = material.Id }, material);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
