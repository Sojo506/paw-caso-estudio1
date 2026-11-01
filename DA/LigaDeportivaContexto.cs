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



    }
}
