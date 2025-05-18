using Examen.ApplicationCore.Domain;
using Examen.Infrastructure;
using System;
using System.Linq;

namespace Examen.ApplicationCore.Services
{
    public class InfirmierService
    {
        private readonly MedicalAnalysisContext _context;

        public InfirmierService(MedicalAnalysisContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public double GetPercentageBySpecialite(Specialite specialite)
        {
            // Get total number of nurses
            int totalInfirmiers = _context.Infirmiers.Count();
            if (totalInfirmiers == 0)
            {
                return 0.0; // Return 0% if there are no nurses
            }

            // Get number of nurses with the specified specialty
            int infirmiersWithSpecialite = _context.Infirmiers
                .Count(i => i.Specialite == specialite.ToString());

            // Calculate percentage
            double percentage = (double)infirmiersWithSpecialite / totalInfirmiers * 100;

            return Math.Round(percentage, 2); // Round to 2 decimal places
        }
    }
}