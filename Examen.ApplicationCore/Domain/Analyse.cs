using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Analyse
    {
        public int AnalyseId { get; set; }
        public int DureeResultat { get; set; } // durée en heures
        public double PrixAnalyse { get; set; }
        public string TypeAnalyse { get; set; }
        public float ValeurAnalyse { get; set; }
        public float ValeurMaxNormale { get; set; }
        public float ValeurMinNormale { get; set; }

        // Foreign Key
        public int LaboratoireId { get; set; }

        // Navigation Property
        public Laboratoire Laboratoire { get; set; }
        public ICollection<Bilan> Bilans { get; set; }
    }
}
