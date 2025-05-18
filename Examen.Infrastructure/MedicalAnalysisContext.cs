// Examen.Infrastructure/MedicalAnalysisContext.cs
using Examen.ApplicationCore.Domain;
using Examen.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace Examen.Infrastructure
{
    public class MedicalAnalysisContext : DbContext
    {
        public DbSet<Laboratoire> Laboratoires { get; set; }
        public DbSet<Infirmier> Infirmiers { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Analyse> Analyses { get; set; }
        public DbSet<Bilan> Bilans { get; set; }

        public MedicalAnalysisContext(DbContextOptions<MedicalAnalysisContext> options) : base(options) { }

        // Examen.Infrastructure/MedicalAnalysisContext.cs
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Existing configurations...
            modelBuilder.Entity<Infirmier>()
                .HasOne(i => i.Laboratoire)
                .WithMany()
                .HasForeignKey(i => i.LaboratoireId);

            // Update seed data for Infirmier
            modelBuilder.Entity<Infirmier>().HasData(
                new Infirmier { CodeInfirmier = "INF01", Nom = "Ahmed Ben Salah", Specialite = Specialite.Hematologie.ToString(), LaboratoireId = 1 }
            );
        }
    }
}