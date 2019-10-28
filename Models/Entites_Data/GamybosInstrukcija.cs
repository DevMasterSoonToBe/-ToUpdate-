using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class GamybosInstrukcija
    {
        public GamybosInstrukcija()
        {
            IngredientoReceptas = new HashSet<IngredientoReceptas>();
            TechnikosPrietaisoPatarimas = new HashSet<TechnikosPrietaisoPatarimas>();
        }

        public decimal KlientoUzsakymoId { get; set; }
        public decimal InstrukcijosId { get; set; }
    [Required(ErrorMessage = "Gamybos instrukcijos būsena privaloma!")]
    public string Busena { get; set; }
          [Required]
          public string RecepturaParuosta { get; set; }
          [Required]
          public string TechnikosPatarimaiParuosti { get; set; }
    [Required(ErrorMessage = "Gamybos terminas privalomas!")]
    [DataType(DataType.DateTime, ErrorMessage = "Įveskite tinkamą datą!")]
    public DateTime GamybosTerminas { get; set; }

        public string BrandinimoLaikas { get; set; }

        public KlientoUzsakymoAplankas KlientoUzsakymo { get; set; }
        public ICollection<IngredientoReceptas> IngredientoReceptas { get; set; }
        public ICollection<TechnikosPrietaisoPatarimas> TechnikosPrietaisoPatarimas { get; set; }
    }
}
