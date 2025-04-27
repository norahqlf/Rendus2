﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Laboratoire
    {
        public int LaboratoireId { get; set; }
        public string Intitule { get; set; }
        public string Localisation { get; set; }

        // Navigation Property
        public ICollection<Analyse> Analyses { get; set; }
    }
}