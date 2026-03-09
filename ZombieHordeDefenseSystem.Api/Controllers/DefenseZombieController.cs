using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZombieHordeDefenseSystem.Application.Dtos;
using ZombieHordeDefenseSystem.Application.Services;

namespace ZombieHordeDefenseSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefenseZombieController : ControllerBase
    {
        public readonly IDefenseStrategyService _defenseStrategyService;
        public DefenseZombieController(IDefenseStrategyService defenseStrategyService)
        {
            _defenseStrategyService = defenseStrategyService;
        }
        [HttpPost("optimal-strategy")]

        public async Task<IActionResult> GetOptimalDefenseStrategy([FromBody] DefenseStrategyRequestDto request)
        {
            if (request == null || request.Bullets < 0 || request.SecondsAvailable < 0)
            {
                return BadRequest("Invalid request. Please provide valid bullets and seconds available.");
            }
            var result = await _defenseStrategyService.GetOptimalDefenseStrategyAsync(request);
            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetSimulationHistory()
        {
            var history = await _defenseStrategyService.GetSimulationHistoryAsync();
            if (history == null || !history.Any())
            {
                return NotFound("No simulation history found.");
            }
            return Ok(history);
        }
    }
}
