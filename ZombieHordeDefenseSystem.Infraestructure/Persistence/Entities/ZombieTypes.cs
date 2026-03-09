using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZombieDefense.Infrastructure.Persistence;

public partial class ZombieTypes
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Tipo { get; set; } = null!;

    public int TiempoDisparo { get; set; }

    public int BalasNecesarias { get; set; }

    public int Puntaje { get; set; }

    public int NivelAmenaza { get; set; }

    [InverseProperty("Zombie")]
    public virtual ICollection<Eliminados> Eliminados { get; set; } = new List<Eliminados>();
}
