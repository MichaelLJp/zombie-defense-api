using System;
using System.Collections.Generic;
using System.Text;
using ZombieHordeDefenseSystem.Domain.Entities;

namespace ZombieHordeDefenseSystem.Application.Ports
{
    public interface IZombieRepository
    {
        /// <summary>
        /// Asynchronously retrieves a list of all zombies available in the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see
        /// cref="ZombieType"/> objects representing all zombies. The list will be empty if no zombies are found.</returns>
        Task<List<ZombieType>> GetAllZombiesAsync();
    }
}
