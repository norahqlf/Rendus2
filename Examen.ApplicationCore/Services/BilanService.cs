using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class BilanService : IBilanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BilanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Method 1: Get the total amount of a Bilan, applying a 10% discount if the patient has more than 5 prélèvements
        public decimal GetTotalAmount(int bilanId)
        {
            // Retrieve the Bilan entity
            var bilan = _unitOfWork.Repository<Bilan>().Get(b => b.BilanId == bilanId);
            if (bilan == null) throw new ArgumentException("Bilan not found");

            // Get the Patient associated with the Bilan
            var patient = _unitOfWork.Repository<Patient>().Get(p => p.CodePatient == bilan.PatientId);
            if (patient == null) throw new ArgumentException("Patient not found");

            // Count how many prélèvements (analyses) the patient has had
            var prelevementsCount = _unitOfWork.Repository<Bilan>()
                .GetMany(b => b.PatientId == patient.CodePatient)
                .Count();

            // Retrieve the associated Analyse for the current Bilan
            var analyse = _unitOfWork.Repository<Analyse>().Get(a => a.AnalyseId == bilan.AnalyseId);
            if (analyse == null) throw new ArgumentException("Analyse not found");

            // Calculate the total amount of the Bilan (Sum of analysis prices)
            decimal totalAmount = analyse.PrixAnalyse;

            // Apply 10% discount if the patient has more than 5 prélèvements
            if (prelevementsCount > 5)
            {
                totalAmount -= totalAmount * 0.10m; // 10% discount
            }

            return totalAmount;
        }

        // Method 4: Get the date when a Bilan will be ready (when all its analyses are completed)
        public DateTime GetReadyDateForBilan(int bilanId)
        {
            // Retrieve the Bilan entity
            var bilan = _unitOfWork.Repository<Bilan>().Get(b => b.BilanId == bilanId);
            if (bilan == null) throw new ArgumentException("Bilan not found");

            // Retrieve the associated Analyse for the current Bilan
            var analyse = _unitOfWork.Repository<Analyse>().Get(a => a.AnalyseId == bilan.AnalyseId);
            if (analyse == null) throw new ArgumentException("Analyse not found");

            // Estimate the ready date by adding the duration of the analysis to the prélèvement date
            return bilan.DatePrelevement.AddHours(analyse.DureeResultat);
        }
    }
}

