using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Infirmier
    {
        public string CodeInfirmier { get; set; }
        public string Nom { get; set; }
        public string Specialite { get; set; } // Added

        public ICollection<Bilan> Bilans { get; set; }
    }

}
