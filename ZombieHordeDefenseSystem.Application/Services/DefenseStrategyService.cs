using System;
using System.Collections.Generic;
using System.Text;
using ZombieHordeDefenseSystem.Application.Dtos;
using ZombieHordeDefenseSystem.Application.Ports;
using ZombieHordeDefenseSystem.Domain.Entities;

namespace ZombieHordeDefenseSystem.Application.Services
{
    public class DefenseStrategyService : IDefenseStrategyService
    {
        private readonly IZombieRepository _zombieRepository;
        public readonly ISimulationRepository _simulationRepository;

        public DefenseStrategyService(IZombieRepository zombieRepository, ISimulationRepository simulationRepository)
        {
            _zombieRepository = zombieRepository;
            _simulationRepository = simulationRepository;
        }

        public async Task<DefenseStrategyResultDto> GetOptimalDefenseStrategyAsync(DefenseStrategyRequestDto request)
        {
            var zombiesDisponibles = await _zombieRepository.GetAllZombiesAsync();


            var maxBullets = request.Bullets;
            var maxtime = request.SecondsAvailable;

            int[,] dp = new int[maxBullets + 1, maxtime + 1];

            ZombieType?[,] zombieChoice = new ZombieType?[maxBullets + 1, maxtime + 1];

            for (int b = 0; b <= maxBullets; b++)
            {
                for (int t = 0; t <= maxtime; t++)
                {
                    foreach (var zombie in zombiesDisponibles)
                    {
                        if (zombie.BalasNecesarias <= b && zombie.TiempoDisparo <= t)
                        {

                            int scoreWithZombie = dp[b - zombie.BalasNecesarias, t - zombie.TiempoDisparo] + zombie.Puntaje;
                            if (scoreWithZombie > dp[b, t])
                            {
                                dp[b, t] = scoreWithZombie;
                                zombieChoice[b, t] = zombie;
                            }
                        }
                    }
                }
            }

            var simulacion = new Simulacion(maxtime, maxBullets);

            int remainingBullets = maxBullets;
            int remainingTime = maxtime;

            while (remainingBullets > 0 && remainingTime > 0 && zombieChoice[remainingBullets, remainingTime] != null) {
                var zombieElegido = zombieChoice[remainingBullets, remainingTime];
                simulacion.RegistrarZombieEliminado(zombieElegido);

                remainingBullets -= zombieElegido.BalasNecesarias;
                remainingTime -= zombieElegido.TiempoDisparo;
            }

            await _simulationRepository.AddAsync(simulacion);
            var result = new DefenseStrategyResultDto
            {
                PuntajeTotal = simulacion.PuntajeLogrado,
                BalasUsadas = maxBullets - remainingBullets,
                TiempoUsado = maxtime - remainingTime,
                ZombiesEliminados = simulacion.Eliminados.Select(e => new ZombieEliminadoDto
                {
                    Tipo = e.Zombie.Nombre,
                    NivelAmenaza = e.Zombie.NivelAmenaza,
                    Puntaje = e.PuntosObtenidos,
                    BalasUsadas = e.Zombie.BalasNecesarias,
                    TiempoUsado = e.Zombie.TiempoDisparo
                }).OrderByDescending(x => x.NivelAmenaza).ToList()
            };

            return result;
        }

        public async Task<IEnumerable<HistoryStrategyDto>> GetSimulationHistoryAsync()
        {
            var simulaciones = await _simulationRepository.GetAllAsync();
            var historyResult = simulaciones.Select(s => new HistoryStrategyDto
            {
                id = s.Id,
                PuntajeLogrado = s.PuntajeLogrado,
                BalasDisponibles = s.BalasDisponibles,
                TiempoDisponible = s.TiempoDisponible,

                Eliminados = s.Eliminados
                .GroupBy(e => e.Zombie.Nombre)
                .Select(g => new ZombieDefeatedDto
                {
                    Nombre = g.Key,
                    Cantidad = g.Count()
                }).ToList()
                }).ToList();
            return historyResult;


        }
    }
}
