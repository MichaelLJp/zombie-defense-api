using System;
using System.Collections.Generic;
using System.Text;
using ZombieHordeDefenseSystem.Application.Dtos;

namespace ZombieHordeDefenseSystem.Application.Services
{
    public interface IDefenseStrategyService
    {
        Task<DefenseStrategyResultDto> GetOptimalDefenseStrategyAsync(DefenseStrategyRequestDto request);

        Task<IEnumerable<HistoryStrategyDto>> GetSimulationHistoryAsync();
    }
}
