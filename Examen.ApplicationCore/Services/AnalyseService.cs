using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class AnalyseService : IAnalyseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Bilan> GetAbnormalAnalysesByPatient(int patientId)
        {
            var bilans = _unitOfWork.Repository<Bilan>()
                .GetMany(b => b.PatientId == patientId && b.DatePrelevement.Year == DateTime.Now.Year)
                .ToList();

            var abnormalBilans = new List<Bilan>();

            foreach (var bilan in bilans)
            {
                foreach (var analyse in bilan.Analyses)
                {
                    // Check if the analyse value is abnormal (below minimum or above maximum)
                    if (analyse.ValeurAnalyse < analyse.ValeurMinNormale || analyse.ValeurAnalyse > analyse.ValeurMaxNormale)
                    {
                        abnormalBilans.Add(bilan);
                        break;
                    }
                }
            }

            return abnormalBilans;
        }
    }

}
