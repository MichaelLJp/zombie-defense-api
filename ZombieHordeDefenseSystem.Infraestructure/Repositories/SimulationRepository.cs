using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZombieDefense.Infrastructure.Persistence;
using ZombieHordeDefenseSystem.Application.Ports;
using ZombieHordeDefenseSystem.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZombieHordeDefenseSystem.Infraestructure.Repositories
{
    public class SimulationRepository : ISimulationRepository
    {
        private readonly ZombieDefenseDbContext _dbContext;

        public SimulationRepository(ZombieDefenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Simulacion simulacion)
        {

            var simulacionEntity = new Simulaciones
            {
                Fecha = DateTime.UtcNow,
                TiempoDisponible = simulacion.TiempoDisponible,
                BalasDisponibles = simulacion.BalasDisponibles,
                PuntajeLogrado = simulacion.PuntajeLogrado,
                Eliminados = simulacion.Eliminados.Select(e => new Eliminados
                {
                    ZombieId = e.Zombie.Id,
                    PuntosObtenidos = e.Zombie.Puntaje,
                    Timestamp = DateTime.UtcNow
                }).ToList()
            };
            await _dbContext.Simulaciones.AddAsync(simulacionEntity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<Simulacion>> GetAllAsync()
        {
            var dbSimulaciones = await _dbContext.Simulaciones.Include(s => s.Eliminados)
                .ThenInclude(e => e.Zombie)
                .OrderByDescending(s => s.BalasDisponibles)
                .ThenByDescending(s => s.TiempoDisponible)
                .AsNoTracking()
                .ToListAsync();
            var historialDominio = new List<Simulacion>();
            foreach (var simulacion in dbSimulaciones)
            {
                var simulacionDominio = new Simulacion(
                    simulacion.Id,
                    simulacion.TiempoDisponible ?? 0,
                    simulacion.BalasDisponibles ?? 0,
                    simulacion.PuntajeLogrado ?? 0
                    );

                foreach (var eliminado in simulacion.Eliminados)
                {

                    if (eliminado.Zombie != null)
                    {
                        var zombieDominio = new ZombieType(
                            eliminado.Zombie.Id,
                            eliminado.Zombie.Tipo,
                            eliminado.Zombie.TiempoDisparo,
                            eliminado.Zombie.BalasNecesarias,
                            eliminado.Zombie.Puntaje,
                            eliminado.Zombie.NivelAmenaza
                        );
                        simulacionDominio.CargarZombieExistente(zombieDominio);
                    }

                }
                historialDominio.Add(simulacionDominio);
            }
            return historialDominio;
        }
    }
}
