using System;
using System.Collections.Generic;
using System.Text;
using ZombieHordeDefenseSystem.Domain.Entities;

namespace ZombieHordeDefenseSystem.Application.Ports
{
    public interface ISimulationRepository
    {
        /// <summary>
        /// Asynchronously adds a new Simulacion entity to the data store.
        /// </summary>
        /// <param name="simulacion">The Simulacion instance to add. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
        Task AddAsync(Simulacion simulacion);

        Task<IEnumerable<Simulacion>> GetAllAsync();
    }
}
