using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcCongresoTIC.Models;

namespace MvcCongresoTIC.Data
{
    public class MvcCongresoTICContext : DbContext
    {
        public MvcCongresoTICContext (DbContextOptions<MvcCongresoTICContext> options)
            : base(options)
        {
        }

        public DbSet<MvcCongresoTIC.Models.Participante> Participante { get; set; } = default!;
        public DbSet<MvcCongresoTIC.Models.Conferencia> Conferencia { get; set; } = default!;
        public DbSet<MvcCongresoTIC.Models.RegistroConferencia> RegistrosConferencias { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegistroConferencia>()
                .HasKey(rc => new { rc.ConferenciaId, rc.ParticipanteId });

            modelBuilder.Entity<RegistroConferencia>()
                .HasOne(rc => rc.Conferencia)
                .WithMany(c => c.Asistentes)
                .HasForeignKey(rc => rc.ConferenciaId);

            modelBuilder.Entity<RegistroConferencia>()
                .HasOne(rc => rc.Participante)
                .WithMany(p => p.RegistrosConferencias)
                .HasForeignKey(rc => rc.ParticipanteId);

            // Otros ajustes y configuraciones...

            base.OnModelCreating(modelBuilder);
        }
    }
}
