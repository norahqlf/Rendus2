using Examen.ApplicationCore.Domain;
using Examen.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Laboratoire> Laboratoires { get; set; }
        public DbSet<Analyse> Analyses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Bilan> Bilans { get; set; }
        public DbSet<Infirmier> Infirmiers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API without configuration class
            modelBuilder.Entity<Laboratoire>(entity =>
            {
                entity.Property(l => l.Localisation)
                      .HasColumnName("AdresseLabo")
                      .HasMaxLength(50);
            });

            // Fluent API with configuration class
            modelBuilder.ApplyConfiguration(new BilanConfiguration());
        }

        // 👉 ADD THIS:
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LaboRomdhaniNourElhouda;Trusted_Connection=True;");
            }
        }
    }
}
