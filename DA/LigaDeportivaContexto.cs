using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AB.Models.Equipos;

namespace DA
{
    public class LigaDeportivaContexto : DbContext
    {
        public LigaDeportivaContexto(DbContextOptions<LigaDeportivaContexto> options) : base(options)
        {
        }

        public DbSet<Equipo> Equipos => Set<Equipo>();

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
        }
    }
}
