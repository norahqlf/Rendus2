using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class InfirmierService : IInfirmierService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InfirmierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public double GetInfirmierPercentageBySpecialty(Specialite specialite)
        {
            // Get all infirmiers in the given specialty
            var totalInfirmiers = _unitOfWork.Repository<Infirmier>().GetMany(i => i.Specialite == specialite).Count();
            var totalInfirmiersCount = _unitOfWork.Repository<Infirmier>().GetAll().Count();

            if (totalInfirmiersCount == 0)
                throw new InvalidOperationException("No infirmiers found.");

            // Calculate the percentage
            return (totalInfirmiers / (double)totalInfirmiersCount) * 100;
        }
    }
}

