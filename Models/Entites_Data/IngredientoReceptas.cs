using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class IngredientoReceptas
    {
        public decimal KlientoUzsakymoId { get; set; }
        public decimal InstrukcijosId { get; set; }
        [Required]
        public decimal IngredientoId { get; set; }
        [Required(ErrorMessage = "Ingrediento kiekis privalomas!")]
    [RegularExpression("^[0-9]+(\\.[0-9]{1,2})?$", ErrorMessage = "Ingrediento kiekis negali būti neigiamas bei turi būti pateiktas natūraliu skaičiumi arba dešimtųjų tikslumu!")]
    public double IngredientoKiekis { get; set; }
    [StringLength(50, ErrorMessage = "Virimo laikas negali turėti daugiau 50 simbolių")]
    public string VirimoLaikas { get; set; }
    [StringLength(50, ErrorMessage = "Fermentacijos laikas negali turėti daugiau 50 simbolių")]
    public string FermentacijosLaikas { get; set; }

        public GamybosInstrukcija GamybosInstrukcija { get; set; }
        public Ingredientas Ingrediento { get; set; }
    }
}
