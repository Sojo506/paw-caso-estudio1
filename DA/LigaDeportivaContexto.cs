using Microsoft.EntityFrameworkCore;
using AB.Models.Equipos;
using AB.Models.Jugadores;
using AB.Models.Partidos;

namespace DA
{
    public class LigaDeportivaContexto : DbContext
    {
        public LigaDeportivaContexto(DbContextOptions<LigaDeportivaContexto> options) : base(options)
        {
        }

        public DbSet<Equipo> Equipos => Set<Equipo>();
        public DbSet<Jugador> Jugadores => Set<Jugador>();
        public DbSet<Partido> Partidos => Set<Partido>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(e => e.Nombre)
                    .IsUnique();

                entity.Property(e => e.Ciudad)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Jugador>(entity =>
            {
                entity.Property(j => j.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(j => j.Apellido)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(j => new { j.EquipoId, j.NumeroCamiseta })
                    .IsUnique();

                entity.HasOne(j => j.Equipo)
                    .WithMany(e => e.Jugadores)
                    .HasForeignKey(j => j.EquipoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Partido>(entity =>
            {
                entity.Property(p => p.Cancha)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(p => p.EquipoLocal)         
                    .WithMany()
                    .HasForeignKey(p => p.EquipoLocalId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.EquipoVisitante)
                    .WithMany()
                    .HasForeignKey(p => p.EquipoVisitanteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
