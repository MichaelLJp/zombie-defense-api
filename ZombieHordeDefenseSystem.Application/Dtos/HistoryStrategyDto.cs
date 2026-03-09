using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieHordeDefenseSystem.Application.Dtos
{
    public class HistoryStrategyDto
    {
        public int id { get; set; }
        public int PuntajeLogrado { get; set; }
        public int BalasDisponibles { get; set; }
        public int TiempoDisponible { get; set; }
        public List<ZombieDefeatedDto> Eliminados { get; set; }
    }
}
