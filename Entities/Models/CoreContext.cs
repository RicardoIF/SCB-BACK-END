using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.Models
{
    public partial class CoreContext : DbContext
    {
        public CoreContext()
        {
        }

        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cargo> Cargos { get; set; } = null!;
        public virtual DbSet<CargoDepartamento> CargoDepartamentos { get; set; } = null!;
        public virtual DbSet<Colaborador> Colaboradors { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Dependencia> Dependencia { get; set; } = null!;
        public virtual DbSet<Genero> Generos { get; set; } = null!;
        public virtual DbSet<GrupoOcupacional> GrupoOcupacionals { get; set; } = null!;
        public virtual DbSet<SupervisorEmpleado> SupervisorEmpleados { get; set; } = null!;
        public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; } = null!;

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            modelBuilder.Entity<SupervisorEmpleado>(entity =>
            {
                entity.ToTable("SupervisorEmpleado");

                entity.Property(e => e.SupervisorEmpleadoId).ValueGeneratedNever();

                entity.Property(e => e.FechaModificiacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.HasOne(d => d.Hijo)
                    .WithMany(p => p.SupervisorEmpleadoHijos)
                    .HasForeignKey(d => d.HijoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supervisorempleado_hijoid_foreign");

                entity.HasOne(d => d.Padre)
                    .WithMany(p => p.SupervisorEmpleadoPadres)
                    .HasForeignKey(d => d.PadreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supervisorempleado_padreid_foreign");
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.ToTable("TipoDocumento");

                entity.Property(e => e.TipoDocumentoId).ValueGeneratedNever();

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
