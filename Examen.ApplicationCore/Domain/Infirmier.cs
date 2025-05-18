// Examen.ApplicationCore/Domain/Infirmier.cs
using System.Collections.Generic;

namespace Examen.ApplicationCore.Domain
{
    public class Infirmier
    {
        public string CodeInfirmier { get; set; }
        public string Nom { get; set; }
        public string Specialite { get; set; }
        public int LaboratoireId { get; set; } // Added
        public Laboratoire Laboratoire { get; set; } // Added
        public ICollection<Bilan> Bilans { get; set; }
    }
}