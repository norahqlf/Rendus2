using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Bilan
    {
        public int Id { get; set; }
        public DateTime DatePrelevement { get; set; }
        public string EmailMedecin { get; set; }
        public bool Paye { get; set; }
        public int CodeInfirmier { get; set; }
        public Infirmier Infirmier { get; set; }
        public string CodePatient { get; set; }
        public Patient Patient { get; set; }
        public ICollection<Analyse> Analyses { get; set; }
    }
}
