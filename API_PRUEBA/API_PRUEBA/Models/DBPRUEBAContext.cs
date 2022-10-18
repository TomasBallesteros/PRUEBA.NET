using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_PRUEBA.Models
{
    public partial class DBPRUEBAContext : DbContext
    {
        public DBPRUEBAContext()
        {
        }

        public DBPRUEBAContext(DbContextOptions<DBPRUEBAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.PeId)
                    .HasName("PK__PERSONAS__4828874B99CD455E");

                entity.ToTable("PERSONAS");

                entity.Property(e => e.PeId).HasColumnName("PE_ID");

                entity.Property(e => e.PeApellidos)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PE_APELLIDOS");

                entity.Property(e => e.PeCodigoPersona)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PE_CODIGO_PERSONA");

                entity.Property(e => e.PeEmail)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PE_EMAIL");

                entity.Property(e => e.PeFechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("PE_FECHA_CREACION");

                entity.Property(e => e.PeIdentificacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PE_IDENTIFICACION");

                entity.Property(e => e.PeNombreCompleto)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("PE_NOMBRE_COMPLETO");

                entity.Property(e => e.PeNombres)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PE_NOMBRES");

                entity.Property(e => e.PeTipoIdentificacion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PE_TIPO_IDENTIFICACION");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIOS");

                entity.Property(e => e.UsPassword)
                    .HasMaxLength(512)
                    .IsUnicode(false)
                    .HasColumnName("US_PASSWORD");

                entity.Property(e => e.UsUsuario)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("US_USUARIO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
