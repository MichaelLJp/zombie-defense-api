using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieHordeDefenseSystem.Application.Dtos
{
    public class ZombieEliminadoDto
    {
        public string Tipo { get; set; } = string.Empty;
        public int NivelAmenaza { get; set; }
        public int Puntaje { get; set; }
        public int BalasUsadas { get; set; }
        public int TiempoUsado { get; set; }    
    }
}
