using System;
using System.Collections.Generic;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class LaisviResursai
    {
        public LaisviResursai()
        {
            Ingredientas = new HashSet<Ingredientas>();
            TechnikosPrietaisas = new HashSet<TechnikosPrietaisas>();
      LaisviResursaiUzsakymui = new HashSet<ResursaiUzsakymui>();
    }

        public decimal FormosId { get; set; }
        public string LaisvuResursuKodas { get; set; }
    public string ResursoPavadinimas { get; set; }


    public ICollection<Ingredientas> Ingredientas { get; set; }
    public ICollection<ResursaiUzsakymui> LaisviResursaiUzsakymui { get; set; }
    public ICollection<TechnikosPrietaisas> TechnikosPrietaisas { get; set; }
    }
}
