using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Infirmier
    {
        public int InfirmierId { get; set; }
        public string NomComplet { get; set; }
        public Specialite Specialite { get; set; }

        // Navigation Property
        public ICollection<Bilan> Bilans { get; set; }
    }
}
