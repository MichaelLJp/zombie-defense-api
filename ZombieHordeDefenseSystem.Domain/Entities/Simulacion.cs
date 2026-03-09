using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZombieHordeDefenseSystem.Domain.Entities
{
    public class Simulacion
    {
        public int Id { get; private set; }

        public DateTime Fecha { get; private set; }

        public int TiempoDisponible { get; private set; }

        public int BalasDisponibles { get; private set; }

        public int PuntajeLogrado { get; private set; }

        public readonly List<Eliminado> _eliminados = new();

        public IReadOnlyCollection<Eliminado> Eliminados => _eliminados.AsReadOnly();

        public Simulacion(int tiempoDisponible, int balasDisponibles)
        {
            Fecha = DateTime.Now;
            TiempoDisponible = tiempoDisponible;
            BalasDisponibles = balasDisponibles;
            PuntajeLogrado = 0;
        }
        public Simulacion(int id, int tiempo, int balas, int puntaje)
        {
            Id = id;
            TiempoDisponible = tiempo;
            BalasDisponibles = balas;
            PuntajeLogrado = puntaje;
        }

        public void RegistrarZombieEliminado(ZombieType zombie)
        {
            var eliminado = new Eliminado(zombie);
            _eliminados.Add(eliminado);
            PuntajeLogrado += eliminado.PuntosObtenidos;
        }
        public void CargarZombieExistente (ZombieType zombie)
        {
            var eliminado = new Eliminado(zombie);
            _eliminados.Add(eliminado);
        }

    }
}
