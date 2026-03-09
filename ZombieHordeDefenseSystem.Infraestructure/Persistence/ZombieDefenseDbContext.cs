using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZombieDefense.Infrastructure.Persistence;

public partial class ZombieDefenseDbContext : DbContext
{
    public ZombieDefenseDbContext()
    {
    }

    public ZombieDefenseDbContext(DbContextOptions<ZombieDefenseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Eliminados> Eliminados { get; set; }

    public virtual DbSet<Simulaciones> Simulaciones { get; set; }

    public virtual DbSet<ZombieTypes> ZombieTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:zombie-horde-defense.database.windows.net,1433;Initial Catalog=ZombieDefense;Persist Security Info=False;User ID=AdminZombie;Password=Zombie1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Eliminados>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Eliminad__3214EC07CDABB030");

            entity.Property(e => e.Timestamp).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Simulation).WithMany(p => p.Eliminados).HasConstraintName("FK__Eliminado__Simul__09A971A2");

            entity.HasOne(d => d.Zombie).WithMany(p => p.Eliminados).HasConstraintName("FK__Eliminado__Zombi__08B54D69");
        });

        modelBuilder.Entity<Simulaciones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Simulaci__3214EC07B8CFA5C8");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<ZombieTypes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ZombieTy__3214EC078212422B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
