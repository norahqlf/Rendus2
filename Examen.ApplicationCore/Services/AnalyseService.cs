using Examen.ApplicationCore.Domain;
using Examen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Examen.ApplicationCore.Services
{
    public class AnalyseService
    {
        private readonly MedicalAnalysisContext _context;

        public AnalyseService(MedicalAnalysisContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<BilanAbnormalAnalysesDto> GetAbnormalAnalysesByPatient(string codePatient)
        {
            if (string.IsNullOrWhiteSpace(codePatient))
            {
                throw new ArgumentException("CodePatient cannot be null or empty.", nameof(codePatient));
            }

            int currentYear = DateTime.Now.Year;

            var abnormalAnalyses = _context.Analyses
                .Include(a => a.Bilan)
                .Where(a => a.CodePatient == codePatient &&
                            a.DatePrelevement.Year == currentYear &&
                            (a.ValeurAnalyse < a.ValeurMinNormale || a.ValeurAnalyse > a.ValeurMaxNormale))
                .ToList();

            var groupedByBilan = abnormalAnalyses
                .GroupBy(a => a.Bilan)
                .Select(group => new BilanAbnormalAnalysesDto
                {
                    BilanId = new { group.Key.CodeInfirmier, group.Key.CodePatient, group.Key.DatePrelevement },
                    DatePrelevement = group.Key.DatePrelevement,
                    EmailMedecin = group.Key.EmailMedecin,
                    Paye = group.Key.Paye,
                    AbnormalAnalyses = group.Select(a => new AnalyseDto
                    {
                        IdAnalyse = a.IdAnalyse,
                        ValeurAnalyse = a.ValeurAnalyse,
                        ValeurMinNormale = a.ValeurMinNormale,
                        ValeurMaxNormale = a.ValeurMaxNormale,
                        DatePrelevement = a.DatePrelevement,
                        Prix = a.Prix
                    }).ToList()
                })
                .ToList();

            return groupedByBilan;
        }
    }

    public class BilanAbnormalAnalysesDto
    {
        public object BilanId { get; set; } // Composite key
        public DateTime DatePrelevement { get; set; }
        public string EmailMedecin { get; set; }
        public bool Paye { get; set; }
        public List<AnalyseDto> AbnormalAnalyses { get; set; }
    }

    public class AnalyseDto
    {
        public int IdAnalyse { get; set; }
        public decimal ValeurAnalyse { get; set; }
        public decimal ValeurMinNormale { get; set; }
        public decimal ValeurMaxNormale { get; set; }
        public DateTime DatePrelevement { get; set; }
        public decimal Prix { get; set; }
    }
}