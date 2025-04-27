using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Bilan
    {
        public int BilanId { get; set; }
        public DateTime DatePrelevement { get; set; }
        public string EmailMedecin { get; set; }
        public bool Paye { get; set; }

        // Foreign Keys
        public int PatientId { get; set; }
        public int InfirmierId { get; set; }

        // Navigation Properties
        public Patient Patient { get; set; }
        public Infirmier Infirmier { get; set; }
        public ICollection<Analyse> Analyses { get; set; }  // Changed to ICollection for multiple analyses
    }

}
