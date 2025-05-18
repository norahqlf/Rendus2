using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Analyse
    {
        public int IdAnalyse { get; set; }
        public int DureeResultat { get; set; }
        public decimal ValeurAnalyse { get; set; }
        public decimal ValeurMaxNormale { get; set; }
        public decimal ValeurMinNormale { get; set; }
        public decimal Prix { get; set; }

        [Required]
        public string CodeInfirmier { get; set; }

        [Required]
        public string CodePatient { get; set; }

        [Required]
        public DateTime DatePrelevement { get; set; }

        public Bilan Bilan { get; set; }
    }
}
