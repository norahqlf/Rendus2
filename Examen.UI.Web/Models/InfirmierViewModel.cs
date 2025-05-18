// Examen.Web/Models/InfirmierViewModel.cs
using Examen.ApplicationCore.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examen.Web.Models
{
    public class InfirmierViewModel
    {
        [Required]
        [StringLength(10)]
        public string CodeInfirmier { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Specialite { get; set; }

        [Required]
        [Display(Name = "Laboratoire")]
        public int LaboratoireId { get; set; }

        public IEnumerable<Laboratoire> Laboratoires { get; set; }
        public IEnumerable<string> Specialites { get; set; }
    }
}