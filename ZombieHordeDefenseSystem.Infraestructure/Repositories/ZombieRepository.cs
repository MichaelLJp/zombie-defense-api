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
    public class ZombieRepository : IZombieRepository
    {
        public readonly ZombieDefenseDbContext _dbContext;
        
        public ZombieRepository(ZombieDefenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ZombieType>> GetAllZombiesAsync()
        {
            var dbZombies = await _dbContext.ZombieTypes.AsNoTracking().ToListAsync();
            return dbZombies.Select(z => new ZombieType(
                z.Id, 
                z.Tipo, 
                z.TiempoDisparo, 
                z.BalasNecesarias, 
                z.Puntaje, 
                z.NivelAmenaza)).ToList();
        }

    }
}
