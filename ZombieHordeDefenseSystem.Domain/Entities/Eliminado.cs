using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZombieHordeDefenseSystem.Domain.Entities
{
    public class Eliminado
    {
        public virtual ZombieType Zombie { get; set; }
        public int PuntosObtenidos { get; set; }
        public DateTime? Timestamp { get; set; }

        public Eliminado(ZombieType zombie)
        {
                Zombie = zombie;
                PuntosObtenidos = zombie.Puntaje;
                Timestamp = DateTime.Now;
        }

    }
}
