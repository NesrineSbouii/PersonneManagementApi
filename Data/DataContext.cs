using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using PersonManagementApi.Entities;

namespace PersonManagementApi.Data
{
	public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public virtual DbSet<Personne> Personnes { get; set; }
        public virtual DbSet<Emploi> Emplois { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personne>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Prenom)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

                entity.Property(e => e.DateNaissance)
                    .IsRequired();

                entity.HasMany(p => p.Emplois)
                      .WithOne(e => e.Personne)
                      .HasForeignKey(e => e.PersonneId);
            });

            modelBuilder.Entity<Emploi>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NomEntreprise)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PosteOccupe)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateDebut)
                    .IsRequired();

                entity.Property(e => e.DateFin);
            });
        }
    }
}

