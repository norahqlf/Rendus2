using Examen.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Infrastructure.Configurations
{
    public class BilanConfiguration : IEntityTypeConfiguration<Bilan>
    {
        public void Configure(EntityTypeBuilder<Bilan> builder)
        {
            // Définir la clé primaire composée
            builder.HasKey(b => new { b.InfirmierId, b.PatientId, b.DatePrelevement });

            // Définir la relation entre Bilan et Infirmier
            builder.HasOne(b => b.Infirmier)
                   .WithMany(i => i.Bilans)
                   .HasForeignKey(b => b.InfirmierId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Définir la relation entre Bilan et Patient
            builder.HasOne(b => b.Patient)
                   .WithMany(p => p.Bilans)
                   .HasForeignKey(b => b.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
