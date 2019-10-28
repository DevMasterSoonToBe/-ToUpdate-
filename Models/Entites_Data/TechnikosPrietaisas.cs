using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class TechnikosPrietaisas
    {
        public TechnikosPrietaisas()
        {
            TechnikosPrietaisoPatarimas = new HashSet<TechnikosPrietaisoPatarimas>();
            TechnikosPrietaisoPrasymas = new HashSet<TechnikosPrietaisoPrasymas>();
      TechnikosPrietaisoRezervacija = new HashSet<TechnikosPrietaisoRezervacija>();
    }

        public decimal TechnikosPrietaisoId { get; set; }
        public decimal? FormosId { get; set; }
        public string PrietaisoPavadinimas { get; set; }
        public int? LaisvasKiekis { get; set; }

        public LaisviResursai Formos { get; set; }
        public ICollection<TechnikosPrietaisoPatarimas> TechnikosPrietaisoPatarimas { get; set; }
        public ICollection<TechnikosPrietaisoPrasymas> TechnikosPrietaisoPrasymas { get; set; }
    public ICollection<TechnikosPrietaisoRezervacija> TechnikosPrietaisoRezervacija { get; set; }
  }
}
