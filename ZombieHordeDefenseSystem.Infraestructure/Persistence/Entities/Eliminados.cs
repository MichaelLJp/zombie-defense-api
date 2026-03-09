using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZombieDefense.Infrastructure.Persistence;

public partial class Eliminados
{
    [Key]
    public int Id { get; set; }

    public int? ZombieId { get; set; }

    public int? SimulationId { get; set; }

    public int? PuntosObtenidos { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Timestamp { get; set; }

    [ForeignKey("SimulationId")]
    [InverseProperty("Eliminados")]
    public virtual Simulaciones? Simulation { get; set; }

    [ForeignKey("ZombieId")]
    [InverseProperty("Eliminados")]
    public virtual ZombieTypes? Zombie { get; set; }
}
