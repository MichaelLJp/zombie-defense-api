using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZombieHordeDefenseSystem.Domain.Entities
{
    public class ZombieType
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public int TiempoDisparo { get; private set; }

        public int BalasNecesarias { get; private set; }

        public int Puntaje { get; private set; }

        public int NivelAmenaza { get; private set; }

        public ZombieType(int id,string nombre, int tiempoDisparo, int balasNecesarias, int puntaje, int nivelAmenaza)
        {
            Id = id;
            Nombre = nombre;
            TiempoDisparo = tiempoDisparo;
            BalasNecesarias = balasNecesarias;
            Puntaje = puntaje;
            NivelAmenaza = nivelAmenaza;
        }
    }
}
