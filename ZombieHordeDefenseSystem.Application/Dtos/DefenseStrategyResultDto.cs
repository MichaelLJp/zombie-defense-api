using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieHordeDefenseSystem.Application.Dtos
{
    public class DefenseStrategyResultDto
    {
        public int PuntajeTotal { get; set; }
        public int BalasUsadas { get; set; }
        public int TiempoUsado { get; set; }
        public List<ZombieEliminadoDto> ZombiesEliminados { get; set; } = new();

    }
}
