using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZombieDefense.Infrastructure.Persistence;

public partial class Simulaciones
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha { get; set; }

    public int? TiempoDisponible { get; set; }

    public int? BalasDisponibles { get; set; }

    public int? PuntajeLogrado { get; set; }

    [InverseProperty("Simulation")]
    public virtual ICollection<Eliminados> Eliminados { get; set; } = new List<Eliminados>();
}
