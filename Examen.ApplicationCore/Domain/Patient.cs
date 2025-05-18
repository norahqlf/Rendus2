using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Patient
    {
        [Key]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Le CodePatient doit avoir exactement 5 caractères.")]
        public string CodePatient { get; set; }

        public string EmailPatient { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Informations supplémentaires")]
        public string Informations { get; set; }

        public string NomComplet { get; set; }
        public string NumeroTel { get; set; }
        public ICollection<Bilan> Bilans { get; set; }
    }
}
