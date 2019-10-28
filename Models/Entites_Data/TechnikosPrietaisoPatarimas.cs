using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class TechnikosPrietaisoPatarimas
    {
        public decimal TechnikosPrietaisoId { get; set; }
        public decimal KlientoUzsakymoId { get; set; }
        public decimal InstrukcijosId { get; set; }
    [StringLength(1000, ErrorMessage = "Prietaiso komentaras negali turėti daugiau 1000 simbolių")]
    public string PrietaisoKomentaras { get; set; }
    [Required(ErrorMessage = "Prietaiso kiekis privalomas!")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Prietaiso kiekis turi būti sveikas skaičius bei negali būti neigiamas!")]
    public int PrietaisoKiekis { get; set; }

        public GamybosInstrukcija GamybosInstrukcija { get; set; }
        public TechnikosPrietaisas TechnikosPrietaiso { get; set; }
    }
}
