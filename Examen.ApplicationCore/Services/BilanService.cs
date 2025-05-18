using Examen.ApplicationCore.Domain;
using Examen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Examen.ApplicationCore.Services
{
    public class BilanService
    {
        private readonly MedicalAnalysisContext _context;

        public BilanService(MedicalAnalysisContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public decimal CalculateBilanTotalAmount(int codeInfirmier, string codePatient, DateTime datePrelevement)
        {
            // Retrieve the Bilan with its Analyses
            var bilan = _context.Bilans
                .Include(b => b.Analyses)
                .FirstOrDefault(b => b.CodeInfirmier == codeInfirmier &&
                                     b.CodePatient == codePatient &&
                                     b.DatePrelevement == datePrelevement);

            if (bilan == null)
            {
                throw new Exception("Bilan not found.");
            }

            if (bilan.Analyses == null || !bilan.Analyses.Any())
            {
                return 0.0m; // Return 0 if no analyses
            }

            // Count prior Bilans for the patient (excluding the current one)
            int priorBilansCount = _context.Bilans
                .Count(b => b.CodePatient == codePatient &&
                            b.DatePrelevement < datePrelevement);

            // Calculate total amount from Analyses
            decimal totalAmount = bilan.Analyses.Sum(a => a.Prix);

            // Apply 10% discount if patient has more than 5 prior Bilans
            if (priorBilansCount > 5)
            {
                totalAmount *= 0.9m; // 10% discount
            }

            return totalAmount;
        }

        public DateTime GetBilanReadyDate(int codeInfirmier, string codePatient, DateTime datePrelevement)
        {
            // Retrieve the Bilan with its Analyses
            var bilan = _context.Bilans
                .Include(b => b.Analyses)
                .FirstOrDefault(b => b.CodeInfirmier == codeInfirmier &&
                                     b.CodePatient == codePatient &&
                                     b.DatePrelevement == datePrelevement);

            if (bilan == null)
            {
                throw new Exception("Bilan not found.");
            }

            if (bilan.Analyses == null || !bilan.Analyses.Any())
            {
                throw new Exception("No analyses found for the specified Bilan.");
            }

            // Calculate the ready date for each analysis and find the latest
            var latestReadyDate = bilan.Analyses
                .Select(a => a.DatePrelevement.AddDays(a.DureeResultat))
                .Max();

            return latestReadyDate;
        }
    }
}